using GeneticPinball.Scripts.Agents;
using Godot;
using System;

namespace GeneticPinball.Scripts.Visual;

public partial class BallProfileUi : Control
{
    [Export]
    private Label scoreLabel;

    [Export]
    private ColorRect idColorRect;

    [Export]
    private ColorRect backgroundColorRect;

    [Export]
    private Color disabledBackgroundColor;

    [Export]
    private Color disabledTextColor;

    public void Initialize(BallProfile profile)
    {
        idColorRect.Color = profile.Color;
    }

    public void UpdateScore(int newAmount)
    {
        scoreLabel.Text = $"Score: {newAmount}";
    }

    public void Disable()
    {
        backgroundColorRect.Color = disabledBackgroundColor;
        scoreLabel.SelfModulate = disabledTextColor;
    }
}
