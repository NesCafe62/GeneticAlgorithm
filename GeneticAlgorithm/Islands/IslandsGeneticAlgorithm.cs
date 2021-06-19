using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {

	public class IslandsGeneticAlgorithm {
		
		public readonly int IslandsCount;

		public Action<Generation, int, int> OnAfterIteration;

		public IChromosome BestChromosome { get; protected set; }

		public int BestSolutionIslandId { get; protected set; }

		public int GenerationsCount { get; protected set; }

		public bool IsFinished { get; protected set; }


		private IMigration migration;

		private IFitness fitness;

		private List<GeneticAlgorithm> islands;

		private readonly int migrationCount;

		private readonly int migrationInterval;

		public IslandsGeneticAlgorithm(
			GeneticAlgorithmOptions options, IMigration migration,
			int islandsCount, int migrationCount, int migrationInterval
		) {
			options.OnAfterIteration = null;

			this.migration = migration;
			fitness = options.Fitness;
			IslandsCount = islandsCount;
			this.migrationCount = migrationCount;
			this.migrationInterval = migrationInterval;

			islands = new List<GeneticAlgorithm>(islandsCount);
			for (int i = 0; i < islandsCount; i++) {
				var island = new GeneticAlgorithm(options);
				int islandId = i;
				island.OnAfterIteration += (generation) => {
					OnAfterIteration.Invoke(generation, island.Population.Generations.Count - 1, islandId);
				};
				islands.Add(island);
			}
		}

		public void Run() {
			if (IsFinished) {
				throw new Exception("Genetic algorithm is finished");
			}

			bool isFirstIteration = true;
			do {
				if (!isFirstIteration) {
					Migrate();
				} else {
					isFirstIteration = false;
				}

				for (int islandId = 0; islandId < IslandsCount; islandId++) {
					var island = islands[islandId];
					bool isIterationFinished = !island.RunIterations(migrationInterval);
					if (
						BestChromosome == null ||
						island.BestChromosome.Fitness.Value > BestChromosome.Fitness.Value
					) {
						BestChromosome = island.BestChromosome;
					}
					
					if (isIterationFinished) {
						IsFinished = true;
						BestSolutionIslandId = islandId;
						GenerationsCount = island.Population.Generations.Count;
						break;
					}
				}
			} while (!IsFinished);
		}

		protected void Migrate() {
			for (int toIslandId = 0; toIslandId < IslandsCount; toIslandId++) {
				int fromIslandId = (toIslandId + 1) % IslandsCount;
				var toIsland = islands[toIslandId].Population.CurrentGeneration;
				var fromIsland = islands[fromIslandId].Population.CurrentGeneration;
				MigrateGeneration(fromIsland, toIsland);
			}
		}

		protected void MigrateGeneration(Generation from, Generation to) {
			var migrateChromosomes = from.Chromosomes
				.OrderByDescending(c => c.Fitness.Value)
				.Take(migrationCount);

			foreach (var chromosome in migrateChromosomes) {
				var migrateChromosome = migration.Migrate(chromosome, to.Chromosomes);
				migrateChromosome.Fitness = fitness.СalculateFitness(migrateChromosome);
				to.Chromosomes.Add(migrateChromosome);
			}
			
		}

		public Population GetIslandPopulation(int islandId) {
			return islands[islandId].Population;
		}

	}

}
