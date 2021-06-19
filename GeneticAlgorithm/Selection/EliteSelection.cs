using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {

    class EliteSelection : ISelection {

        public IList<IChromosome> SelectChromosomes(int count, IList<IChromosome> chromosomes) {
            return chromosomes.OrderByDescending(c => c.Fitness.Value)
                .Take(count)
                .ToList();
        }
		
    }

}
