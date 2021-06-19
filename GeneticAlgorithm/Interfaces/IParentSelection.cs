using System.Collections.Generic;

namespace GeneticAlgorithm {

	public interface IParentSelection {
		
		IChromosome ChooseParent(IChromosome parent, IList<IChromosome> chromosomes);

	}

}
