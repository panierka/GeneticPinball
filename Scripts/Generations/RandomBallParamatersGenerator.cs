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
            var x = Randomizer.Range(2.5f, 4);
            var y = Randomizer.Range(-4.5f, -3f);
            var v0 = Randomizer.Range(1000, 1200);
            var size = Randomizer.Range(0.5f, 2);
            var g = Randomizer.Range(0.1f, 1f);
            var m = Randomizer.Range(0.2f, 10f);

            return new()
            {
                Direction = new(x, y),
                InitialVelocity = v0,
                SizeScale = size,
                GravityScale = g,
                Mass = m,
            };
        }
    }
}
