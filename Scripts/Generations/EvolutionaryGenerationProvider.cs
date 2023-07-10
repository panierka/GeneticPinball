using GeneticPinball.Scripts.Agents;
using GeneticPinball.Scripts.Utility;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace GeneticPinball.Scripts.Generations;

public partial class EvolutionaryGenerationProvider : BallGenerationProviderNode
{
    [Export]
    private int size;

    [Export]
    public float MutationProbability { get; set; }

    [Export]
    public int EliteClonesAmount { get; set; }

    private List<BallParameters>? currentGeneration = null;

    public override List<BallParameters> GetGeneration(List<int> scores)
    {
        if (currentGeneration is null)
        {
            currentGeneration = Enumerable
                .Repeat(0, size)
                .Select(_ => RandomBallParamatersGenerator.Generate())
                .ToList();

            return currentGeneration;
        }

        var clones = currentGeneration
            .Zip(scores)
            .OrderByDescending(x => x.Second)
            .Take(EliteClonesAmount)
            .Select(x => x.First);

        var crossed = Cross(currentGeneration, scores, size - EliteClonesAmount);
        var mutated = Mutate(crossed);
        var nextGeneration = clones.Concat(mutated);

        currentGeneration = nextGeneration.ToList();

        return currentGeneration;
    }

    private List<BallParameters> Mutate(List<BallParameters> parameters)
    {
        return new List<BallParameters>(parameters)
            .Select(x =>
            {
                if (!Randomizer.Chance(MutationProbability))
                {
                    return x;
                }

                return x with
                {
                    Angle = Mathf.Clamp(
                        x.Angle + Randomizer.Normal(), 
                        RandomBallParamatersGenerator.MIN_ANGLE, 
                        RandomBallParamatersGenerator.MAX_ANGLE
                    ),

                    InitialVelocity = Mathf.Clamp(
                        x.InitialVelocity + Randomizer.Normal(), 
                        RandomBallParamatersGenerator.MIN_INITIAL_VELOCITY, 
                        RandomBallParamatersGenerator.MAX_INITIAL_VELOCITY
                    ),
                    
                    Mass = Mathf.Clamp(
                        x.Mass + Randomizer.Normal(), 
                        RandomBallParamatersGenerator.MIN_MASS, 
                        RandomBallParamatersGenerator.MAX_MASS
                    ),
                    
                    GravityScale = Mathf.Clamp(
                        x.GravityScale + Randomizer.Normal(), 
                        RandomBallParamatersGenerator.MIN_GRAVITY, 
                        RandomBallParamatersGenerator.MAX_GRAVITY
                    ),
                    
                    SizeScale = Mathf.Clamp(
                        x.SizeScale + Randomizer.Normal(), 
                        RandomBallParamatersGenerator.MIN_SIZE, 
                        RandomBallParamatersGenerator.MAX_SIZE
                    ),
                };
            })
            .ToList();
    }

    private static List<BallParameters> Cross(List<BallParameters> parameters, List<int> scores, int amount)
    {
        var rankedAgents = parameters
            .Zip(scores)
            .Select(x => new BallData { Parameters = x.First, Score = x.Second });
        
        var newGeneration = new List<BallParameters>();

        for(int i = 0; i < amount; i++)
        {
            var pair = Roulette(rankedAgents, 2)
                .Select(x => x.Parameters);

            var parent1 = pair.ElementAt(0);
            var parent2 = pair.ElementAt(1);

            var newAgent = new BallParameters()
            {
                Angle = Mathf.Clamp(parent1.Angle + Randomizer.Normal() * (parent2.Angle - parent1.Angle), RandomBallParamatersGenerator.MIN_ANGLE, RandomBallParamatersGenerator.MAX_ANGLE),
                InitialVelocity = Mathf.Clamp(parent1.InitialVelocity + Randomizer.Normal() * (parent2.InitialVelocity - parent1.InitialVelocity), RandomBallParamatersGenerator.MIN_INITIAL_VELOCITY, RandomBallParamatersGenerator.MAX_INITIAL_VELOCITY),
                Mass = Mathf.Clamp(parent1.Mass + Randomizer.Normal() * (parent2.Mass - parent1.Mass), RandomBallParamatersGenerator.MIN_MASS, RandomBallParamatersGenerator.MAX_MASS),
                GravityScale = Mathf.Clamp(parent1.GravityScale + Randomizer.Normal() * (parent2.GravityScale - parent1.GravityScale), RandomBallParamatersGenerator.MIN_GRAVITY, RandomBallParamatersGenerator.MAX_GRAVITY),
                SizeScale = Mathf.Clamp(parent1.SizeScale + Randomizer.Normal() * (parent2.SizeScale - parent1.SizeScale), RandomBallParamatersGenerator.MIN_SIZE, RandomBallParamatersGenerator.MAX_SIZE),
            };

            newGeneration.Add(newAgent);
        }

        return newGeneration;
    }

    private static IEnumerable<BallData> Roulette(IEnumerable<BallData> pool, int amount)
    {
        return pool
            .OrderByDescending(x => x.Score * Randomizer.Range(0, 1))
            .Take(2);
    }

    private readonly struct BallData
    {
        public BallParameters Parameters { get; init; }

        public int Score { get; init; }
    }
}
