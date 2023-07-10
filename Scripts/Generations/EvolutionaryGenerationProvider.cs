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

    private static List<BallParameters> Mutate(List<BallParameters> parameters)
    {
        return new List<BallParameters>(parameters)
            .Select(x =>
            {
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
        throw new NotImplementedException();
    }
}
