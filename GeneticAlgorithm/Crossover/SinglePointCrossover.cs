using System;
using System.Collections.Generic;

namespace GeneticAlgorithm {
    
	class SinglePointCrossover : ICrossover {

        protected IChromosomeFactory Factory;

        protected Random Random;

        public SinglePointCrossover(IChromosomeFactory factory, Random random) {
            Factory = factory;
            Random = random;
        }

        public IList<IChromosome> Crossover(IChromosome firstParent, IChromosome secondParent) {
            int index = Random.Next(1, firstParent.Length-1);
            var firstChild = Factory.CreateNew();
            var secondChild = Factory.CreateNew();

            for (int i = 0; i < firstParent.Length; i++) {
                if (i == index) {
                    var temp = firstParent;
                    firstParent = secondParent;
                    secondParent = temp;
                }

                firstChild.SetGene(i, firstParent.GetGene(i));
                secondChild.SetGene(i, secondParent.GetGene(i));
            }
            return new List<IChromosome>() { firstChild, secondChild };
        }

    }

}
