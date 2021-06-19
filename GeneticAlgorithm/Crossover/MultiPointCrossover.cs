using System;
using System.Collections.Generic;

namespace GeneticAlgorithm {

    class MultiPointCrossover : ICrossover {

        protected IChromosomeFactory Factory;

        protected Random Random;

        protected int PointsCount;

        public MultiPointCrossover(IChromosomeFactory factory, Random random, int pointsCount) {
            Factory = factory;
            Random = random;
            PointsCount = pointsCount;
        }

        public IList<IChromosome> Crossover(IChromosome firstParent, IChromosome secondParent) {
            var points = Utils.GetUniqueRandom(Random, 0, firstParent.Length, PointsCount);
            var firstChild = Factory.CreateNew();
            var secondChild = Factory.CreateNew();
            int index = 0;
            for (int i = 0; i < firstParent.Length; i++) {
                if (index < points.Length && i == points[index]) {
                    var temp = firstParent;
                    firstParent = secondParent;
                    secondParent = temp;
                    index++;
                }

                firstChild.SetGene(i, firstParent.GetGene(i));
                secondChild.SetGene(i, secondParent.GetGene(i));
            }
            return new List<IChromosome>() { firstChild, secondChild };
        }

    }

}
