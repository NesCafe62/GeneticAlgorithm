using System.Collections.Generic;

namespace GeneticAlgorithm {

	public class IdentityMigration : IMigration {
		
		public IChromosome Migrate(IChromosome chromosome, IList<IChromosome> targetChromosomes) {
			return chromosome.Clone();
		}
		
	}

}
