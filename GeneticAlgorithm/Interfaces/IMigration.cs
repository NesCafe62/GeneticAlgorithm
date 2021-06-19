using System.Collections.Generic;

namespace GeneticAlgorithm {

    public interface IMigration {
		
        IChromosome Migrate(IChromosome chromosome, IList<IChromosome> targetChromosomes);

    }

}
