using Godot;
using System;

namespace GeneticPinball.Scripts.Agents;

public partial class Ball : Node2D
{
	private BallParameters parameters;

	[Export]
	private RigidBody2D rigidBody;

    public void Initialize(BallParameters parameters)
	{
		this.parameters = parameters;
		Launch();
	}

	private void Launch()
	{
		var direction = parameters.Direction.Normalized();
        var velocity = direction * parameters.InitialVelocity;

		rigidBody.ApplyImpulse(velocity);

		GD.Print($"V = {velocity}");
    }
}
