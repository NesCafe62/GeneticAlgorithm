using System;

namespace GeneticAlgorithm {

	public class GeneticAlgorithmOptions : ICloneable {

        public IChromosomeFactory ChromosomeFactory;

        public IFitness Fitness;

        public ISelection Selection;

        public IParentSelection ParentSelection;

        public ICrossover Crossover;

        public IMutation Mutation;

        public double CrossoverProbability;

        public double MutationProbability;

        public Random Random = new Random();

        public int GenerationSize;

        public Action<Generation> OnAfterIteration;

        public int TerminateUnchangedGenetarionsCount = 50;

        public IChromosomeValidator Validator = new EmptyValidator();

        public object Clone() {
            return MemberwiseClone();
        }

    }

}
