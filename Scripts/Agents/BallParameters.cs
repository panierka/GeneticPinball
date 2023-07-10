using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GeneticPinball.Scripts.Agents
{
	public readonly struct BallParameters
	{
		public Vector2 Direction
		{
			get
			{
				var angleInRadians = Mathf.DegToRad(Angle);
                var x = Mathf.Cos(angleInRadians);
                var y = Mathf.Sin(angleInRadians);

				return new(x, -y);
            }
		}
        
        public float Angle { get; init; }

		public float InitialVelocity { get; init; }

		public float Mass { get; init; }

		public float GravityScale { get; init; }

		public float SizeScale { get; init; }
	}
}
