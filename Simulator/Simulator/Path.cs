using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public class Path
    {
		public Waypoint Start { get; private set; }

		public Waypoint End { get; private set; }

		public Path(Waypoint start, Waypoint end)
		{
			Start = start;
			End = end;
		}
    }
}
