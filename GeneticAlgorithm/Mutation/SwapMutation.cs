using System;

namespace GeneticAlgorithm {

	class SwapMutation : IMutation {
		
		protected Random Random;
		
		public SwapMutation(Random random) {
			Random = random;
		}

		public void Mutate(IChromosome chromosome) {
			if (chromosome.Length < 2) {
				return;
			}

			int firstIndex = Random.Next(0, chromosome.Length);
			int secondIndex = Random.Next(0, chromosome.Length - 1);

			if (secondIndex >= firstIndex) {
				secondIndex++;
			}

			if (firstIndex > secondIndex) {
				int temp = firstIndex;
				firstIndex = secondIndex;
				secondIndex = temp;
			}

			var firstGene = chromosome.GetGene(firstIndex);
			var secondGene = chromosome.GetGene(secondIndex);
			chromosome.SetGene(firstIndex, secondGene);
			chromosome.SetGene(secondIndex, firstGene);
		}

	}

}
