using System.Collections.Generic;

namespace GeneticAlgorithm {

	public interface ICrossover {
		
		IList<IChromosome> Crossover(IChromosome firstParent, IChromosome secondParent);

	}

}
