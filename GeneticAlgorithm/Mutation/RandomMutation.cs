using System;

namespace GeneticAlgorithm {

	class RandomMutation : IMutation {
		
		protected IGeneFactory Factory;

		protected Random Random;

		public RandomMutation(IGeneFactory factory, Random random) {
			Factory = factory;
			Random = random;
		}

		public void Mutate(IChromosome chromosome) {
			int index = Random.Next(0, chromosome.Length);
			chromosome.SetGene(index, Factory.CreateGene(index));
		}
		
	}

}
