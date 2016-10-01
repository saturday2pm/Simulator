using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public class Castle : Waypoint
    {
		public Player Owner { get; private set; } = null;

		public int Level { get; private set; } = 1;

		List<Waypoint> AttackPoint;

		public Castle(float x, float y)
		{
			Pos.X = x;
			Pos.Y = y;
		}

		public Castle(Point pos)
		{
			Pos = pos;
		}

		public void Update(Match match)
		{
		}

		public void Attack(Waypoint point)
		{
			if (!AttackPoint.Contains(point))
			{
				AttackPoint.Add(point);
			}
		}

		public void CancelAttack(Waypoint point)
		{
			AttackPoint.Remove(point);
		}
    }
}
