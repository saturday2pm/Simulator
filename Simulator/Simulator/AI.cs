using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public class AI : Player
    {
		public override void Init(Match match)
		{
		}

		public override void Update(Match match)
		{
			foreach (var castle in OwnCastles)
			{
				CastleUpdate(match, castle);	
			}
		}

		void CastleUpdate(Match match, Castle castle)
		{
			//전략
			//업그레이드 가능한 성은 코스트 *1.2보다 유닛 수가 많으면 업그레이드 한다.
			//자신이 소유한 모든 성에 대해서, 가장 가까운 빈 성 한 곳에 쳐들어간다.
			//빈 성이 없다면, 자신보다 병력이 약한 적 성 중에서 가장 가까운 성에 쳐들어간다.
			//가장 가까운 적 성과의 거리가 일정 이상 떨어져있다면, 가장 가까운 아군 성에 병력을 보낸다.
			//모든 적 성의 병력이 자신보다 높다면, 가만히 있는다.
			//우선순위는 위에서부터 적은 순서대로고, 반드시 한 번에 한 곳의 성만 쳐들어간다.
			//만약 지금 수행하고 있는 일보다 더 높은 우선순위의 경우가 발생한다면 기존 행동을 중단하고 더 높은 우선순위의 행동으로 변경한다.

			if (castle.IsUpgradable && castle.UnitNum > castle.Cost * 1.2)
				castle.Upgrade();

			Castle emptyCastle = null;
			Castle weekCastle = null;
			float maxDistance = 300.0f;

			Castle allyCastle = null;

			foreach (var c in match.Castles)
			{
				if (c == castle)
					continue;

				if (c.Owner == null && (emptyCastle == null || (emptyCastle.Pos.DistSquare(castle.Pos) > c.Pos.DistSquare(castle.Pos))))
					emptyCastle = c;

				if (c.Owner != castle.Owner && (weekCastle == null || weekCastle.Pos.DistSquare(castle.Pos) < maxDistance * maxDistance && (weekCastle == null || (weekCastle.UnitNum > c.UnitNum))))
					weekCastle = c;

				if (c.Owner == castle.Owner && (allyCastle == null || (allyCastle.Pos.DistSquare(castle.Pos) > c.Pos.DistSquare(castle.Pos))))
					allyCastle = c;
			}

			if (emptyCastle != null)
			{
				Attack(castle, emptyCastle);
				return;
			}

			if (weekCastle != null)
			{
				Attack(castle, weekCastle);
				return;
			}

			if (allyCastle != null)
			{
				Attack(castle, allyCastle);
				return;
			}

			if(castle.EndPoint.Count > 0)
				castle.CancelAttack(castle.EndPoint[0]);
		}

		void Attack(Castle castle, Castle end)
		{
			if (castle.EndPoint.Count > 0)
			{
				if (end == castle.EndPoint[0])
					return;

				castle.CancelAttack(castle.EndPoint[0]);
			}

			castle.Attack(end);
		}
	}
}
