namespace GeneticAlgorithm {
	
	public interface IGeneFactory<T> where T : IGene {
		
		T CreateGene(int index);

	}

}
