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
		public const float MIN_INITIAL_ANGLE = 10;
		public const float MAX_INITIAL_ANGLE = 80;
		public static BallParameters Generate()
		{
			var angle = Randomizer.Range(MIN_INITIAL_ANGLE, MAX_INITIAL_ANGLE);
			var x = (float)Math.Cos(angle*Math.PI/180);
			var y = (float)-Math.Sin(angle*Math.PI/180);
			var v0 = Randomizer.Range(700, 1500);
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
