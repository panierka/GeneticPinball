using GeneticPinball.Scripts.Agents;
using Godot;
using System;

namespace GeneticPinball.Scripts.Board;

public partial class Border : Node2D
{
	public void ReactToBall(Node2D body)
	{
		if (body.Owner is Ball ball)
		{
			ball.FinishSimulation();
		}
	}
}
