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

    [Signal]
    public delegate void OnCurrentAmountOfBallsChangedEventHandler(int amount);

    private int currentAmountOfBalls;
    
    public int CurrentAmountOfBalls 
    { 
        get => currentAmountOfBalls; 
        set
        {       
            currentAmountOfBalls = value;
            EmitSignal(
                SignalName.OnCurrentAmountOfBallsChanged, 
                CurrentAmountOfBalls
            );
        } 
    }

    public void SpawnBalls(IEnumerable<BallParameters> ballDatas)
    {
        ballDatas.ToList().ForEach(SpawnBall);
    }
    
    private void SpawnBall(BallParameters parameters)
    {
        var ball = packedBall.Instantiate<Ball>();

        AddChild(ball);
        ball.Initialize(parameters);
        ball.OnBallSimulationFinished += RegisterBallFinish;

        CurrentAmountOfBalls++;
    }

    private void RegisterBallFinish(int ballScore)
    {
        CurrentAmountOfBalls--;
    }
}
