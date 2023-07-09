using Godot;
using System;

namespace GeneticPinball.Scripts.Agents;

public partial class Ball : Node2D
{
	[Export]
	private RigidBody2D rigidBody;

	[Signal]
	public delegate void OnBallSimulationFinishedEventHandler(int score);

	private int score;
    
    public int Score 
	{ 
		get => score;
		set
		{
			score = value;
		}
	}

    public void Initialize(BallParameters parameters)
	{
        var direction = parameters.Direction.Normalized();
        var initialVelocity = direction * parameters.InitialVelocity;

		Scale *= parameters.SizeScale;

		rigidBody.Mass = parameters.Mass;
		rigidBody.GravityScale = parameters.GravityScale;
        
        Launch(initialVelocity);
	}

	private void Launch(Vector2 initialVelocity)
	{
		rigidBody.ApplyImpulse(initialVelocity);
    }

	private void AddScore(int amount)
	{
		Score += amount;
	}

	public void FinishSimulation()
	{
		EmitSignal(SignalName.OnBallSimulationFinished, score);
        QueueFree();
	}
}
