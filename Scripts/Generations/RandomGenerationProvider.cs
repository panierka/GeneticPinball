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

		public override List<BallParameters> GetGeneration(List<float> _)
		{
			return Enumerable
				.Repeat(0, size)
				.Select(_ => RandomBallParamatersGenerator.Generate())
				.ToList();
		}
	}
}
