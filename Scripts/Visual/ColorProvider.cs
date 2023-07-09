using Godot;
using System;

namespace GeneticPinball.Scripts.Visual;

public static class ColorProvider
{
    public static Color GetColorFromId(int id, int ballCount)
    {
        var hue = 360f * (id - 1) / ballCount;
        return HsvToRgb(hue, 1, 1);
    }
    
    private static Color HsvToRgb(float h, float s, float v)
    {
        var c = s * v;
        var x = c * (1 - Mathf.Abs((h / 60f % 2) - 1));
        var hi = Mathf.FloorToInt(h / 60f);

        return hi switch
        {
            0 or 6 => new(c, x, 0),
            1 => new(x, c, 0),
            2 => new(0, c, x),
            3 => new(0, x, c),
            4 => new(x, 0, c),
            5 or -1 => new(c, 0, x),
            _ => throw new NotImplementedException()
        };
    }
}
