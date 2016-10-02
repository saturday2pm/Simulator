using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
	public class Castle : Waypoint
	{
		public Player Owner { get; set; } = null;

		public int Level { get; private set; } = 1;

		List<CastleUpgradeInfo> UpgradeInfo;

		float unitNum;
		float unitRunRatio;

		public float UnitIncreaseRatio
		{
			get { return UpgradeInfo[Level - 1].IncreaseRatio; }
		}

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

		public List<Waypoint> EndPoint { get; private set; } = new List<Waypoint>();

		public Castle(int id, int startNum, float x, float y, float runRatio, List<CastleUpgradeInfo> info) : base(id)
		{
			Pos = new Point(x, y);
			unitNum = startNum;

			unitRunRatio = runRatio;

			UpgradeInfo = info;
		}

		public Castle(int id, Point pos, float runRatio, List<CastleUpgradeInfo> info) : base(id)
		{
			Pos = pos;

			unitRunRatio = runRatio;

			UpgradeInfo = info;
		}

		public void Update(Match match)
		{
			if (Owner == null)
			{
				return; // 야만인 땅은 걍 가만히 있음
			}

			unitNum += unitNum * UnitIncreaseRatio;

			int num = (int)(unitNum * unitRunRatio);

			//unit 일부를 다른 지역으로 파견함
			if (num > 0 && num * EndPoint.Count < UnitNum)
			{
				foreach (var end in EndPoint)
				{
					match.CreateUnit(num, Owner, Radius, this, end);
					unitNum -= num;
				}
			}

			if (unitNum > MaxNum)
				unitNum = MaxNum;
		}

		public void Attack(Waypoint point)
		{
			if (point == this)
				return;

			if (!EndPoint.Contains(point))
			{
				EndPoint.Add(point);
			}
		}

		public void CancelAttack(Waypoint point)
		{
			if (point == this)
				return;

			EndPoint.Remove(point);
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

			if (unitNum <= 0.0f)
			{
				unitNum = -unitNum;

				if(Owner != null)
					Owner.RemoveCastle(this);

				Owner = unit.Owner;
				Owner.AddCastle(this);
			}
		}

		public void Upgrade()
		{
			if (!IsUpgradable)
				return;

			unitNum -= Cost;
			Level++;
		}
    }
}
