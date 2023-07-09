using Godot;
using System;

namespace GeneticPinball.Scripts.Agents;

public partial class Ball : Node2D
{
	[Export]
	private RigidBody2D rigidBody;

    public void Initialize(BallParameters parameters)
	{
        var direction = parameters.Direction.Normalized();
        var initialVelocity = direction * parameters.InitialVelocity;

        Launch(initialVelocity);
	}

	private void Launch(Vector2 initialVelocity)
	{
		rigidBody.ApplyImpulse(initialVelocity);
    }
}
