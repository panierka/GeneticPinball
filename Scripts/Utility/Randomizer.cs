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

        public static bool Chance(float successPercentage)
        {
            return generator.Randf() <= successPercentage;
        }

        public static float Normal(float mean = 0, float deviation = 1)
        {
            return generator.Randfn(mean, deviation);
        }
    }
}
