using GeneticPinball.Scripts.Agents;
using Godot;
using System;

namespace GeneticPinball.Scripts.Board;

public partial class Bumper : Node2D
{
	[Export]
	private float bumpingPower;

	[Export]
	private int addedScore;

	public void Bump(Node2D node)
	{
		if (node.Owner is Ball ball)
		{
			ball.AddScore(addedScore);
		}

		if (node is RigidBody2D rb)
		{
			var vectorBetween = rb.GlobalPosition - GlobalPosition;
			var direction = vectorBetween.Normalized();
			var force = direction * bumpingPower;

			rb.ApplyImpulse(force);
        }
	}
}
