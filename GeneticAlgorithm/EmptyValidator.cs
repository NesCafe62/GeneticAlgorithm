namespace GeneticAlgorithm {

    class EmptyValidator : IChromosomeValidator {

        public bool Validate(IChromosome chromosome) {
            return true;
        }

    }

}
