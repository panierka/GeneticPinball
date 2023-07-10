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

public partial class GeneticGenerationProvider : BallGenerationProviderNode
{
    [Export]
    private int size;

    [Export]
    public float MutationProbability { get; set; }

    [Export]
    public float CrossingProbability { get; set; }

    private List<BallParameters>? currentGeneration = null;

    public override List<BallParameters> GetGeneration(List<float> scores)
    {
        if (currentGeneration is null)
        {
            currentGeneration = Enumerable
                .Repeat(0, size)
                .Select(_ => RandomBallParamatersGenerator.Generate())
                .ToList();

            return currentGeneration;
        }
        
        return currentGeneration;
    }

    private static List<BallParameters> GetMutated(List<BallParameters> parameters)
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

    private static List<BallParameters> GetCrossed(List<BallParameters> parameters)
    {
        throw new NotImplementedException();
    }
}
