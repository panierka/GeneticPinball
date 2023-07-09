using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticPinball.Scripts.Utility
{
    public static class Randomizer
    {
        private static RandomNumberGenerator generator;

        static Randomizer()
        {
            generator = new();
            generator.Randomize();
        }

        public static float Range(float lower, float upper)
        {
            return generator.RandfRange(lower, upper);
        }
    }
}
