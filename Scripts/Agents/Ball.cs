using Godot;
using System;

namespace GeneticPinball.Scripts.Agents;

public partial class Ball : Node2D
{
	private Vector2 initialVelocity;

	[Export]
	private RigidBody2D rigidBody;

    public void Initialize(BallParameters parameters)
	{
        var direction = parameters.Direction.Normalized();
        initialVelocity = direction * parameters.InitialVelocity;

        Launch();
	}

	private void Launch()
	{
		rigidBody.ApplyImpulse(initialVelocity);
    }
}
