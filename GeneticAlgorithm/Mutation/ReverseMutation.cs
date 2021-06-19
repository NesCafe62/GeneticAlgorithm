using System;
using System.Collections.Generic;

namespace GeneticAlgorithm {

	class ReverseMutation<T> : IMutation where T : IGene {
		
		protected Random Random;

		public ReverseMutation(Random random)  {
			Random = random;
		}

		public void Mutate(IChromosome chromosome) {
			if (chromosome.Length < 2) {
				return;
			}
			
			int fromIndex = Random.Next(0, chromosome.Length);
			int toIndex = Random.Next(0, chromosome.Length - 1);

			if (toIndex >= fromIndex) {
				toIndex++;
			}

			if (fromIndex > toIndex) {
				int temp = fromIndex;
				fromIndex = toIndex;
				toIndex = temp;
			}

			List<IGene> genes = new List<IGene>(toIndex - fromIndex + 1);
			
			for (int i = fromIndex; i <= toIndex; i++) {
				genes.Add(chromosome.GetGene(i));
			}

			for (int i = toIndex; i >= fromIndex; i--) {
				chromosome.SetGene(i, genes[i - fromIndex]);
			}
		}
		
	}

}
