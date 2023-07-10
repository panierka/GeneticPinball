using GeneticPinball.Scripts.Agents;
using GeneticPinball.Scripts.Generations;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
		StartNextIteration(null);

		ballManager.OnLastBallFinished += StartNextIteration;
	}

	public void StartNextIteration(IEnumerable<int> scores)
	{
		var ballDatasGeneration = generationProvider.GetGeneration(scores?.ToList());
		ballManager.SpawnBalls(ballDatasGeneration);
	}
}
