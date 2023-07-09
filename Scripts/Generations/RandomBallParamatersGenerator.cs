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
            var x = Randomizer.Range(0.1f, 10f);
            var y = Randomizer.Range(-5f, -3.5f);
            var v0 = Randomizer.Range(900, 2500);
            var size = Randomizer.Range(0.5f, 2);
            var g = Randomizer.Range(0.1f, 3f);
            var m = Randomizer.Range(0.2f, 2f);

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
