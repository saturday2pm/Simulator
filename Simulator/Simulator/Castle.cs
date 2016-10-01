using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
	public class Castle : Waypoint
	{
		public Player Owner { get; private set; } = null;

		public int Level { get; private set; } = 1;

		List<CastleUpgradeInfo> UpgradeInfo;
		float unitSpeed;
		float unitAttackRange;

		float unitNum;

		public int UnitNum
		{
			get { return (int)unitNum; }
		}

		public float Radius
		{
			get { return UpgradeInfo[Level - 1].Radius; }
		}

		public int Cost
		{
			get { return UpgradeInfo[Level - 1].Cost; }
		}

		public int MaxNum
		{
			get { return UpgradeInfo[Level - 1].MaxNum; }
		}

		public bool IsUpgradable
		{
			get
			{
				if (Level == UpgradeInfo.Count)
					return false;

				return UnitNum >= Cost;
			}
		}

		public List<Waypoint> AttackPoint { get; private set;}

		public Castle(int id, float x, float y, List<CastleUpgradeInfo> info, float speed, float range) : base(id)
		{
			Pos.X = x;
			Pos.Y = y;

			UpgradeInfo = info;
			unitSpeed = speed;
			unitAttackRange = range;
		}

		public Castle(int id, Point pos, List<CastleUpgradeInfo> info, float speed, float range) : base(id)
		{
			Pos = pos;

			UpgradeInfo = info;
			unitSpeed = speed;
			unitAttackRange = range;
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
