using GeneticPinball.Scripts.Agents;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace GeneticPinball.Scripts.Visual;

public partial class BallsUiController : Control
{
    [Export]
    private PackedScene packedBallProfileUi;

    [Export]
    private int verticalOffset;

    [Export]
    private int distanceBetweenProfileUis;

    public static BallsUiController Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
    
    public void RegisterBallProfile(Ball ball, BallProfile ballProfile)
    {
        var ballProfileUi = packedBallProfileUi.Instantiate<BallProfileUi>();
        ballProfileUi.Initialize(ballProfile);

        var positionY = verticalOffset + (ballProfile.Id - 1) * distanceBetweenProfileUis;
        ballProfileUi.Position = Vector2.Down * positionY;

        AddChild(ballProfileUi);

        ball.OnBallSimulationFinished += _ =>
        {
            ballProfileUi?.Disable();
        };

        ball.OnScoreChanged += score =>
        {
            ballProfileUi?.UpdateScore(score);
        };

        ballProfileUi.UpdateScore(ball.Score);
    }
    
    public void Clear()
    {
        GetChildren()
            .ToList()
            .ForEach(x => x.QueueFree());
    }
}
