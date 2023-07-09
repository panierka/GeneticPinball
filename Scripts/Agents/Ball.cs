using Godot;
using System;
using System.Linq;

namespace GeneticPinball.Scripts.Agents;

public partial class Ball : Node2D
{
	[Export]
	private RigidBody2D rigidBody;

	[Signal]
	public delegate void OnBallSimulationFinishedEventHandler(int score);

	private int score;
	private int id;
    
    public int Score 
	{ 
		get => score;
		private set
		{
			score = value;
		}
	}

    public void Initialize(int id, BallParameters parameters)
	{
		this.id = id;

        var direction = parameters.Direction.Normalized();
		var initialVelocity = direction * parameters.InitialVelocity;

		rigidBody
			.GetChildren()
			.Cast<Node2D>()
			.Where(x => x is { })
			.ToList()
			.ForEach(x => x.Scale *= parameters.SizeScale);

		rigidBody.Mass = parameters.Mass;
		rigidBody.GravityScale = parameters.GravityScale;
        
        Launch(initialVelocity);
	}

    private void Launch(Vector2 initialVelocity)
	{
		rigidBody.ApplyImpulse(initialVelocity);
    }

	public void AddScore(int amount)
	{
		Score += amount;
	}

	public void FinishSimulation()
	{
		EmitSignal(SignalName.OnBallSimulationFinished, score);
        QueueFree();
	}
}
