using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {

	class RouletteWheelSelection : ISelection {
		
		protected Random Random;

		public RouletteWheelSelection(Random random) {
			Random = random;
		}

		protected IList<double> CalculateWheel(IList<IChromosome> chromosomes) {
			var wheel = new List<double>(chromosomes.Count);

			double sumFitness = chromosomes.Sum(c => c.Fitness.Value);
			double cumulativeProbability = 0;

			for (int i = 0; i < chromosomes.Count; i++) {
				cumulativeProbability += chromosomes[i].Fitness.Value;
				wheel.Add(cumulativeProbability / sumFitness);
			}

			return wheel;
		}

		public IList<IChromosome> SelectChromosomes(int count, IList<IChromosome> chromosomes) {
			var wheel = CalculateWheel(chromosomes);

			var newChromosomes = new List<IChromosome>(count);

			for (int i = 0; i < count; i++) {
				double pointer = Random.NextDouble();
				var chromosome = wheel.Select((value, index) => new { Value = value, Index = index })
					.First(r => r.Value >= pointer);
				newChromosomes.Add(chromosomes[chromosome.Index].Clone());
			}

			return newChromosomes;
		}

	}

}
