using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {
    
	public class Generation {

        public IList<IChromosome> Chromosomes { get; protected set; }

        public IChromosome BestChromosome { get; protected set; }

        public Generation(IList<IChromosome> chromosomes) {
            Chromosomes = chromosomes;
        }

        public void EvaluateBestChromosome() {
            BestChromosome = Chromosomes.Aggregate((current, next) => (
				(next.Fitness > current.Fitness) ? next : current
			));
        }

        public void EvaluateFitness(IFitness fitness) {
            foreach (var chromosome in Chromosomes) {
                if (!chromosome.Fitness.HasValue) {
                    chromosome.Fitness = fitness.getFitness(chromosome);
                }
            }
        }

    }

}
