# GeneticAlgorithm
Genetic algorithm library wtitten in С#


# Usage

## Define gene, chromosome and fitness function

Fitness function higher values treated as better
```cs
class MyGene : IGene {

    public int Value;
    
    public SolutionGene(int value) {
        Value = value;
    }
    
} 


class MyChromosome : IChromosome {

    public double? Fitness { get; set; }
    
    public int Length { get; }
    
    public int[] Values;

    public Solution(int[] values) {
        Values = values;
        Length = Values.Length;
    }

    public IChromosome Clone() {
        var chromosome = new MyChromosome(Values);
        chromosome.Fitness = Fitness;
        return chromosome;
    }

    public void SetGene(int index, IGene gene) {
        Values[index] = ((MyGene)gene).Value;
    }

    public IGene GetGene(int index) {
        return new MyGene(Values[index]);
    }
    
}

class MyFitness : IFitness {

    public double getFitness(IChromosome chromosome) {
        var myChromosome = (MyChromosome) chromosome;
        int counter = 0;
        // for example count how many neighbout numbers differ by only 1
        for (int i = 0; i < myChromosome.Length - 1; i++) {
             if (Math.Abs(myChromosome.Values[i] - myChromosome.Values[i + 1]) == 1) {
                 counter++;
             }
        }
        return counter;
    }

}
```

## Define chromosome and gene factory

It can be two separate classes or just one single class
```cs
class MyChromosomeFactory : IChromosomeFactory, IGeneFactory<MyGene> {

    private int count;
    
    private int maxValue;
    
    private int minValue;
    
    private Random random;

    public SolutionFactory(int count, int minValue, int maxValue, Random random) {
        this.count = count;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.random = random;
    }
    
    private int GenerateGene(int index) {
        return random.Next(minValue, maxValue);
    }

    public MyGene CreateGene(int index) {
        return new MyGene(GenerateGene(index));
    }

    public IChromosome CreateChromosome() {
        var values = new int[count];
        for (int i = 0; i < count; i++) {
            values[i] = GenerateGene(i);
        }
        return new MyChromosome(values);
    }
    
}
```

## Creating chromosome and gene factories
```cs
var chromosomeFactory = new MyChromosomeFactory(10, -5, 5, new Random());
var geneFactory = chromosomeFactory;
```

## Creating algorithm options
```cs
var options = new GeneticAlgorithmOptions {

    // factory that will be used for creating new chromosomes
    ChromosomeFactory = chromosomeFactory,

    // number of chromosomes in generation
    GenerationSize = 20,

    // chance that selected parent will be crossed to create new chromosomes
    CrossoverProbability = 0.8,

    // chance that chromosomes created by crossover will be mutated
    MutationProbability = 0.1,

    // how long continue to run algorithm if best chromosome fitness is not changing
    // [optional]
    // default value: 50
    TerminateUnchangedGenetarionsCount = 50,

    // fitness function
    Fitness = new MyFitness(),

    // selection operator
    Selection = new EliteSelection(),
    // Selection = new RouletteWheelSelection(new Random()),
    // Selection = new TournamentSelection(new Random(), roundSize: 3),

    // parent selection operator
    ParentSelection = new RandomParentSelection(new Random()),
    // ParentSelection = new InbreedingParentSelection(new Random()),

    // crossover operator
    Crossover = new SinglePointCrossover(chromosomeFactory, new Random()),
    // Crossover = new MultiPointCrossover(chromosomeFactory, new Random(), pointsCount: 3),

    // mutation operator
    Mutation = new RandomMutation<MyGene>(geneFactory, new Random()),
    // Mutation = new SwapMutation<MyGene>(new Random()),
    // Mutation = new ReverseMutation<MyGene>(new Random()),

    // delegate function (event) that will be executed after each algorithm iteration
    // [optional]
    OnAfterIteration = (generation) => {
        // output generation chromosomes for example
    }

};
```

## Creating and running genetic algorithm

```cs
// creting genetic algorithm
ga = new GeneticAlgorithm(options);

// alowed to set after iteration delegate from here also
ga.OnAfterIteration += (generation) => {
    // ...
};

// running algorithm
ga.Run();

// getting result
int generationsCount = ga.GenerationsCount;
var bestChromosome = (MyChromosome) ga.BestChromosome;
```


# Islands genetic algorithm

...


# Selection operators

## EliteSelection()

Takes best chromosomes of generation determined by fintess function


## RouletteWheelSelection(Random random)

Picks chromosomes from a wheel, where probability is proportional to fintess function, until number of chromosomes in new generation match required generation size


## TournamentSelection(Random random, int roundSize)
`roundSize`: Number of chromosomes participating in each tournament round

Takes winner chromosome (by fitness function) to new generation from each tournament round until new generation match required generation size. Participants are picked randomly (so same chromosome can be taken multiple times, though Clone method will guarantee unique chromosome instances in new generation)


# Parent selection operators

## RandomParentSelection(Random random)

...


## InbreedingParentSelection(Random random)

...


# Crossover operators

## SinglePointCrossover(IChromosomeFactory factory, Random random)

`factory`: chromosome factory

...


## MultiPointCrossover(IChromosomeFactory factory, Random random, int pointsCount)

`factory`: chromosome factory
`pointsCount`: number of chromosome split points

...


# Mutation operators

## RandomMutation<T>(IGeneFactory<T> factory, Random random) where T : IGene

`factory`: gene factory

...


## SwapMutation<T>(Random random) where T : IGene

...


## ReverseMutation<T>(Random random) where T : IGene

...


# Defining custom operators

...
