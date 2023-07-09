using GeneticPinball.Scripts.Utility;
using Godot;
using System;
using System.Linq;

namespace GeneticPinball.Scripts.Agents;

public partial class BallManager : Node2D
{
    [Export]
    private PackedScene packedBall;

    public override void _Ready()
    {
        Repeat.Action(() =>
        {
            var x = Randomizer.Range(0, 1);
            var y = Randomizer.Range(-1, -0.5f);
            var v0 = Randomizer.Range(600, 800);

            var parameters = new BallParameters()
            {
                Direction = new(x, y),
                InitialVelocity = v0
            };

            SpawnBall(parameters);

        }, 20);
    }
    
    private void SpawnBall(BallParameters parameters)
    {
        var ball = packedBall.Instantiate<Ball>();

        AddChild(ball);
        ball.Initialize(parameters);
    }
}
