using GeneticPinball.Scripts.Agents;
using GeneticPinball.Scripts.Generations;
using Godot;
using System;

namespace GeneticPinball.Scripts.Core;

[GlobalClass]
public partial class SimulationController : Node2D
{
	[Export]
	private BallManager ballManager;

	[Export]
	private BallGenerationProviderNode generationProvider;

	public override void _Ready()
	{
		StartNextIteration();
	}

	public void StartNextIteration()
	{
		var ballDatasGeneration = generationProvider.GetGeneration();
		ballManager.SpawnBalls(ballDatasGeneration);
	}
}
