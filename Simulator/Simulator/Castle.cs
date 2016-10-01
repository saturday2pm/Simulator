using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public class Castle : Waypoint
	{
		public Player Owner { get; private set; } = null;

		public int Level { get; private set; } = 1;

		float unitNum;

		public int UnitNum
		{
			get { return (int)unitNum; }
		}

		List<Waypoint> AttackPoint;

		public Castle(int id, float x, float y) : base(id)
		{
			Pos.X = x;
			Pos.Y = y;
		}

		public Castle(int id, Point pos) : base(id)
		{
			Pos = pos;
		}

		public void Update(Match match)
		{
			unitNum += unitNum * match.UnitIncreaseRatio;
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

		public void AddUnit(Unit unit)
		{
			if (unit.Owner != Owner)
				return;

			unitNum += unit.Num;
		}

		public void Attacked(Unit unit)
		{
			if (unit.Owner == Owner)
				return;

			unitNum -= unit.Num;
		}
    }
}
