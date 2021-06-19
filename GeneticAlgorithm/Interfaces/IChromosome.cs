namespace GeneticAlgorithm {

	public interface IChromosome {
		
		double? Fitness { get; set; }

		int Length { get;  }

	   
		IChromosome Clone();

		void SetGene(int index, IGene gene);

		IGene GetGene(int index);

	}

}
