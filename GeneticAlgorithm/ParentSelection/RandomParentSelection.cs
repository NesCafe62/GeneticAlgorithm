using System;
using System.Collections.Generic;

namespace GeneticAlgorithm {

	class RandomParentSelection : IParentSelection {
		
		protected Random Random;

		public RandomParentSelection(Random random)  {
			Random = random;
		}

		public IChromosome ChooseParent(IChromosome parent, IList<IChromosome> chromosomes) {
			int parentIndex = chromosomes.IndexOf(parent);
			int index;
			if (parentIndex == -1) {
				index = Random.Next(0, chromosomes.Count);
			} else {
				index = Random.Next(0, chromosomes.Count - 1);
			
				if (index >= parentIndex) {
					index++;
				}
			}
			return chromosomes[index];
		}

	}

}
