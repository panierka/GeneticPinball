using GeneticPinball.Scripts.Agents;
using GeneticPinball.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticPinball.Scripts.Generations
{
    public static class RandomBallParamatersGenerator
    {
        public static BallParameters Generate()
        {
            var x = Randomizer.Range(0, 1);
            var y = Randomizer.Range(-1, -0.5f);
            var v0 = Randomizer.Range(600, 1000);
            var size = Randomizer.Range(0.5f, 2);
            var g = Randomizer.Range(0.2f, 2f);
            var m = Randomizer.Range(0.2f, 10f);

            return new()
            {
                Direction = new(x, y),
                InitialVelocity = v0,
                SizeScale = size,
                GravityScale = g,
                Mass = m
            };
        }
    }
}
