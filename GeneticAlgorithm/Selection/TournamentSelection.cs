using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {

    class TournamentSelection : ISelection {
		
        protected Random Random;

        protected int RoundSize;

        public TournamentSelection(Random random, int roundSize) {
            Random = random;
            RoundSize = roundSize;
        }

        public IList<IChromosome> SelectChromosomes(int count, IList<IChromosome> chromosomes) {
            var newChromosomes = new List<IChromosome>(count);

            while (newChromosomes.Count < count) {
                var randomIndexes = Utils.GetUniqueRandom(Random, 0, chromosomes.Count, RoundSize);
                var winner = chromosomes.Where((c, i) => randomIndexes.Contains(i))
                    .Aggregate((current, next) => (
						(next.Fitness.Value > current.Fitness.Value) ? next : current
					));
				
                newChromosomes.Add(winner.Clone());
            }
            return newChromosomes;
        }

    }

}
