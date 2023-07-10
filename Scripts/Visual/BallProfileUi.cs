using GeneticPinball.Scripts.Agents;
using Godot;
using System;

namespace GeneticPinball.Scripts.Visual;

public partial class BallProfileUi : Control
{
    [Export]
    private Label scoreLabel;

    [Export]
    private ColorRect colorRect;

    public void Initialize(BallProfile profile)
    {
        colorRect.Color = profile.Color;
    }

    public void UpdateScore(int newAmount)
    {
        scoreLabel.Text = $"Score: {newAmount}";
    }
}
