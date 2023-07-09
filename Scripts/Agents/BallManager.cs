using GeneticPinball.Scripts.Utility;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticPinball.Scripts.Agents;

[GlobalClass]
public partial class BallManager : Node2D
{
    [Export]
    private PackedScene packedBall;

    public void SpawnBalls(IEnumerable<BallParameters> ballDatas)
    {
        ballDatas.ToList().ForEach(SpawnBall);
    }
    
    private void SpawnBall(BallParameters parameters)
    {
        var ball = packedBall.Instantiate<Ball>();

        AddChild(ball);
        ball.Initialize(parameters);
    }
}
