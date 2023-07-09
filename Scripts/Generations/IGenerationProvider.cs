using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticPinball.Scripts.Generations
{
	public interface IGenerationProvider<T>
	{
		public IEnumerable<T> GetGeneration();
	}
}
