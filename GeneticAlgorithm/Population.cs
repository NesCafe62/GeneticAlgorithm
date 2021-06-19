using System;
using System.Collections.Generic;

namespace GeneticAlgorithm {

	public class Population {

		public int GenerationSize { get; protected set; }

		public IList<Generation> Generations { get; protected set; }

		public Generation CurrentGeneration { get; protected set; }

		protected IChromosomeFactory ChromosomeFactory;

		protected int MaxTries = 1000;

		public Population(int generationSize, IChromosomeFactory factory) {
			GenerationSize = generationSize;
			ChromosomeFactory = factory;
		}

		public void CreateInitialGeneration(IChromosomeValidator validator) {
			Generations = new List<Generation>();
			var chromosomes = new List<IChromosome>(GenerationSize);
			for (int i = 0; i < GenerationSize; i++) {
				chromosomes.Add(CreateValidChromosome(validator));
			}
			CreateNewGeneration(chromosomes);
		}

		protected IChromosome CreateValidChromosome(IChromosomeValidator validator) {
			IChromosome chromosome;
			int i = 0;
			do {
				chromosome = ChromosomeFactory.CreateChromosome();
				i++;
				if (i >= MaxTries) {
					throw new Exception($"Unable to create valid chromosome in {MaxTries} tries");
				}
			} while (!validator.Validate(chromosome));
			return chromosome;
		}

		public void CreateNewGeneration(IList<IChromosome> chromosomes) {
			CurrentGeneration = new Generation(chromosomes);
			CurrentGeneration.EvaluateBestChromosome();
			Generations.Add(CurrentGeneration);
		}

	}

}
