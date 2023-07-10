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
                    Angle = x.Angle + Randomizer.Normal(),
                    InitialVelocity = x.InitialVelocity + Randomizer.Normal(),
                    Mass = x.Mass + Randomizer.Normal(),
                    GravityScale = x.GravityScale + Randomizer.Normal(),
                    SizeScale = x.SizeScale + Randomizer.Normal(),
                };
            })
            .ToList();
    }

    private static List<BallParameters> Cross(List<BallParameters> parameters, List<int> scores, int amount)
    {
        var rankedAgents = parameters
            .Zip(scores)
            .Select(x => new BallData { Parameters = x.First, Score = x.Second });
        
        var new_generation = new List<BallParameters>();

        for(int i = 0; i < amount; i++)
        {
            var pair = Roulette(rankedAgents, 2)
                .Select(x => x.Parameters);
            
            new_generation.Add(parameters.Select(x => x with
            {
                Angle = x[0].Angle + Randomizer.Normal() * (x[1].Angle - x[0].Angle),
                InitialVelocity = x[0].InitialVelocity + Randomizer.Normal() * (x[1].InitialVelocity - x[0].InitialVelocity),
                Mass = x[0].Mass + Randomizer.Normal() * (x[1].Mass - x[0].Mass),
                GravityScale = x[0].GravityScale + Randomizer.Normal() * (x[1].GravityScale - x[0].GravityScale),
                SizeScale = x[0].SizeScale + Randomizer.Normal() * (x[1].SizeScale - x[0].SizeScale),
            })
            );
        }

        return new_generation;
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
