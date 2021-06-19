using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {

	public class GeneticAlgorithm {

		public Population Population { get; protected set; }

		public Action<Generation> OnAfterIteration;

		public IChromosome BestChromosome { get; protected set; }

		public bool IsFinished { get; protected set; }


		protected GeneticAlgorithmOptions options;

		private double lastFitness;

		private bool initialized;

		private int unchangedGenerationsCount;

		public GeneticAlgorithm(GeneticAlgorithmOptions options) {
			Population = new Population(options.GenerationSize, options.ChromosomeFactory);
			this.options = options;
			OnAfterIteration = options.OnAfterIteration;
		}

		protected void Init() {
			if (initialized) {
				return;
			}
			Population.CreateInitialGeneration(options.Validator);
			Population.CurrentGeneration.EvaluateFitness(options.Fitness);
			Population.CurrentGeneration.EvaluateBestChromosome();
			initialized = true;
		}

		public void Run() {
			if (IsFinished) {
				throw new Exception("Genetic algorithm is finished");
			}

			Init();
			while (!IsFinished) {
				IterateGeneration();
			};
		}

		public bool RunIterations(int iterationsCount) {
			if (IsFinished) {
				throw new Exception("Genetic algorithm is finished");
			}

			Init();
			int i = 0;
			while (!IsFinished && i < iterationsCount) {
				IterateGeneration();
				i++;
			};
			return !IsFinished;
		}

		private void UpdateGenerationCount() {
			double bestFitness = BestChromosome.Fitness.Value;
			if (lastFitness == bestFitness) {
				unchangedGenerationsCount++;
			} else {
				unchangedGenerationsCount = 1;
			}
			lastFitness = bestFitness;
		}

		private bool IterateGeneration() {
			var parents = Population.CurrentGeneration.Chromosomes;
			var children = Cross(parents);
			Mutate(children);

			var filteredChildren = children.Where(c => options.Validator.Validate(c));
			EvaluateFitness(filteredChildren);
			
			var newGeneration = options.Selection.SelectChromosomes(Population.GenerationSize, parents.Concat(filteredChildren).ToList());
			Population.CreateNewGeneration(newGeneration);
			BestChromosome = Population.CurrentGeneration.BestChromosome;
			UpdateGenerationCount();
			
			OnAfterIteration.Invoke(Population.CurrentGeneration);
			UpdateIsFinished();
			return IsFinished;
		}

		protected void EvaluateFitness(IEnumerable<IChromosome> chromosomes) {
			foreach (var chromosome in chromosomes) {
				if (!chromosome.Fitness.HasValue) {
					chromosome.Fitness = options.Fitness.СalculateFitness(chromosome);
				}
			}
		}

		protected void UpdateIsFinished() {
			IsFinished = (unchangedGenerationsCount >= options.TerminateUnchangedGenetarionsCount);
		}

		protected IList<IChromosome> Cross(IList<IChromosome> parents) {
			var offspring = new List<IChromosome>();
			foreach (var parent in parents) {
				if (options.Random.NextDouble() <= options.CrossoverProbability) {
					var parent2 = options.ParentSelection.ChooseParent(parent, parents);
					var children = options.Crossover.Crossover(parent, parent2);
					offspring.AddRange(children);
				}
			}
			return offspring;
		}

		protected void Mutate(IList<IChromosome> chromosomes) {
			foreach (var chromosome in chromosomes) {
				if (options.Random.NextDouble() <= options.MutationProbability) {
					options.Mutation.Mutate(chromosome);
				}
			}
		}

	}
}
