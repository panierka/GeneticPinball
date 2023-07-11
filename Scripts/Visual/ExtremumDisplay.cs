using GeneticPinball.Scripts.Agents;
using Godot;
using System;

namespace GeneticPinball.Scripts.Visual;

public partial class ExtremumDisplay : Control
{
    [Export]
    private Label scoreLabel;
    [Export]
    private Label angleLabel;
    [Export]
    private Label velocityLabel;
    [Export]
    private Label gravityLabel;
    [Export]
    private Label massLabel;
    [Export]
    private Label sizeLabel;

    private int currentBestScore;

    public static ExtremumDisplay Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        TryUpdateBest(0, new());
    }

    public void TryUpdateBest(int score, BallParameters parameters)
    {
        if (score < currentBestScore)
        {
            return;
        }

        currentBestScore = score;

        scoreLabel.Text = score.ToString();
        angleLabel.Text = $"{parameters.Angle:n2}\x00B0";
        velocityLabel.Text = $"{parameters.InitialVelocity:n2} px/s";
        gravityLabel.Text = parameters.GravityScale.ToString("n2");
        massLabel.Text = $"{parameters.Mass:n2} kg";
        sizeLabel.Text = parameters.SizeScale.ToString("n2");
    }
}
