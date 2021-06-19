using System;

namespace GeneticAlgorithm {

    class RandomMutation<T> : IMutation where T : IGene  {
		
        protected IGeneFactory<T> Factory;

        protected Random Random;

        public RandomMutation(IGeneFactory<T> factory, Random random) {
            Factory = factory;
            Random = random;
        }

        public void Mutate(IChromosome chromosome) {
            int index = Random.Next(0, chromosome.Length);
            chromosome.SetGene(index, Factory.CreateGene(index));
        }
		
    }

}
