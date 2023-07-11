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

	[Signal]
	public delegate void OnLastBallFinishedEventHandler(Godot.Collections.Array<int> scores);

	private Godot.Collections.Array<int> scores = new();
	private List<BallParameters> currentBallDatas = new();

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
        var size = ballDatas.Count();

		scores = new(Enumerable.Repeat(0, size));
        BallsUiController.Instance.Clear();
		currentBallDatas.Clear();

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
				currentBallDatas.Add(x.Data);
            });

		CurrentAmountOfBalls += size;
	}
	
	private Ball SpawnBall(int id, BallParameters parameters)
	{
		var ball = packedBall.Instantiate<Ball>();

		GetTree().CreateTimer(id * 0.12).Timeout += () => AddChild(ball);
        ball.Initialize(id, parameters);

		ball.OnBallSimulationFinished += RegisterBallFinish;

        return ball;
	}

	private void RegisterBallFinish(int id, int ballScore)
	{
		var index = id - 1;

		CurrentAmountOfBalls--;
		scores[index] = ballScore;

		var parameters = currentBallDatas[index];
        ExtremumDisplay.Instance.TryUpdateBest(ballScore, parameters);

        if (CurrentAmountOfBalls == 0)
		{
			EmitSignal(SignalName.OnLastBallFinished, scores);
		}
	}
}
