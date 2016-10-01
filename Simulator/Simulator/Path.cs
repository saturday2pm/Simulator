using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public class Path
    {
		public Waypoint Start { get; private set; }

		public Waypoint End { get; private set; }
		public Point Dir { get; private set; }

		public Path(Waypoint start, Waypoint end)
		{
			Start = start;
			End = end;
			float x = End.Pos.X - Start.Pos.X;
			float y = End.Pos.Y - start.Pos.Y;

			float norm = (float)Math.Sqrt((x*x + y*y));

			Dir.X = x / norm;
			Dir.Y = y / norm;
		}
		
	}
}
