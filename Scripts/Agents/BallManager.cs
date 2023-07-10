using GeneticPinball.Scripts.Utility;
using GeneticPinball.Scripts.Visual;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticPinball.Scripts.Agents;

[GlobalClass]
public partial class BallManager : Node2D
{
	[Export]
	private PackedScene packedBall;

	[Signal]
	public delegate void OnCurrentAmountOfBallsChangedEventHandler(int amount);

	private int currentAmountOfBalls;
	
	public int CurrentAmountOfBalls 
	{ 
		get => currentAmountOfBalls; 
		set
		{       
			currentAmountOfBalls = Mathf.Max(0, value);
			EmitSignal(
				SignalName.OnCurrentAmountOfBallsChanged, 
				CurrentAmountOfBalls
			);
		} 
	}

	public void SpawnBalls(IEnumerable<BallParameters> ballDatas)
	{
		BallsUiController.Instance.Clear();

		var size = ballDatas.Count();
		ballDatas
			.Zip(Enumerable.Range(1, size))
			.Select(x => new
			{
				Index = x.Second,
				Data = x.First,
			})
			.ToList()
			.ForEach(x =>
			{
				var ball = SpawnBall(x.Index, x.Data);
				var color = ColorProvider.GetColorFromId(x.Index, size);

				ball.Modulate = color;

				var profile = new BallProfile()
				{
					Id = x.Index,
					Parameters = x.Data,
					Color = color
				};

                BallsUiController.Instance.RegisterBallProfile(ball, profile);
            });

		CurrentAmountOfBalls += size;
	}
	
	private Ball SpawnBall(int id, BallParameters parameters)
	{
		var ball = packedBall.Instantiate<Ball>();

		AddChild(ball);
		ball.Initialize(id, parameters);
		ball.OnBallSimulationFinished += RegisterBallFinish;
		return ball;
	}

	private void RegisterBallFinish(int ballScore)
	{
		CurrentAmountOfBalls--;
	}
}
