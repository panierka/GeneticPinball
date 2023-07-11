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
		public const float MIN_ANGLE = 30;
		public const float MAX_ANGLE = 150;

		public const float MIN_INITIAL_VELOCITY = 700;
		public const float MAX_INITIAL_VELOCITY = 1500;

		public const float MIN_SIZE = 0.5f;
		public const float MAX_SIZE = 2;

		public const float MIN_GRAVITY = 0.2f;
		public const float MAX_GRAVITY = 3f;

		public const float MIN_MASS = 0.75f;
		public const float MAX_MASS = 2f;

		public static BallParameters Generate()
		{
			var angle = Randomizer.Range(MIN_ANGLE, MAX_ANGLE);
			var v0 = Randomizer.Range(MIN_INITIAL_VELOCITY , MAX_INITIAL_VELOCITY);
			var size = Randomizer.Range(MIN_SIZE, MAX_SIZE);
			var g = Randomizer.Range(MIN_GRAVITY, MAX_GRAVITY);
			var m = Randomizer.Range(MIN_MASS, MAX_MASS);

			return new()
			{
				Angle = angle,
				InitialVelocity = v0,
				SizeScale = size,
				GravityScale = g,
				Mass = m,
			};
		}
	}
}
