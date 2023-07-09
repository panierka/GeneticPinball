using GeneticPinball.Scripts.Agents;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticPinball.Scripts.Generations
{
	[GlobalClass]
	public abstract partial class BallGenerationProviderNode : Node2D, IGenerationProvider<BallParameters>
	{
		public abstract IEnumerable<BallParameters> GetGeneration();
	}
}
