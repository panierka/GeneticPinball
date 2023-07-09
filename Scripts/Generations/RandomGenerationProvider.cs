using GeneticPinball.Scripts.Agents;
using GeneticPinball.Scripts.Utility;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticPinball.Scripts.Generations
{
    public partial class RandomGenerationProvider : BallGenerationProviderNode
    {
        [Export]
        private int size;

        public override IEnumerable<BallParameters> GetGeneration()
        {
            return Enumerable
                .Repeat(0, size)
                .Select(_ => GenerateRandomParameters());
        }

        private static BallParameters GenerateRandomParameters()
        {
            var x = Randomizer.Range(0, 1);
            var y = Randomizer.Range(-1, -0.5f);
            var v0 = Randomizer.Range(600, 800);
            
            return new()
            {
                Direction = new(x, y),
                InitialVelocity = v0
            };
        }
    }
}
