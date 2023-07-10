using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticPinball.Scripts.Agents;

public readonly struct BallProfile
{
    public BallParameters Parameters { get; init; }

    public int Id { get; init; }

    public Color Color { get; init; }
}

