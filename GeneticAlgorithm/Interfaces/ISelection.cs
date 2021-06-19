using System.Collections.Generic;

namespace GeneticAlgorithm {

	public interface ISelection {
		
		IList<IChromosome> SelectChromosomes(int count, IList<IChromosome> chromosomes);

	}

}
