using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {

	class InbreedingParentSelection : IParentSelection {
		
		protected Random Random;

		public InbreedingParentSelection(Random random) {
			Random = random;
		}
		
		protected IList<double> CalculateWheel(IChromosome parent, IList<IChromosome> chromosomes) {
			var wheel = new List<double>(chromosomes.Count);
			double cumulativeProbability = 0;
			for (int i = 0; i < chromosomes.Count; i++) {
				cumulativeProbability += GetSimilarity(chromosomes[i], parent);
				wheel.Add(cumulativeProbability);
			}
			return wheel;
		}

		public IChromosome ChooseParent(IChromosome parent, IList<IChromosome> chromosomes) {
			var wheel = CalculateWheel(parent, chromosomes);
			double maxValue = wheel.Last();
			double pointer = Random.NextDouble() * maxValue;
			var chromosome = wheel.Select((value, index) => new { Value = value, Index = index })
				.First(r => r.Value >= pointer);
			return chromosomes[chromosome.Index];
		}

		protected double GetSimilarity(IChromosome chromosome1, IChromosome chromosome2) {
			return 1 / (Math.Pow(chromosome1.Fitness.Value - chromosome2.Fitness.Value, 2) + 1);
		}

	}

}
