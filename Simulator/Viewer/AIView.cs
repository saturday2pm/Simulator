using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using System.Drawing;

namespace Viewer
{
	public class AIView : AI
	{
		public Color AIColor;

		public AIView(Color c)
		{
			AIColor = c;
		}
	}
}
