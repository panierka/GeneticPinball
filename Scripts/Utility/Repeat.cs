using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticPinball.Scripts.Utility
{
    public static class Repeat
    {
        public static void Action(Action action, int times)
        {
            for (int _ = 0; _ < times; _++)
            {
                action?.Invoke();
            }
        }
    }
}
