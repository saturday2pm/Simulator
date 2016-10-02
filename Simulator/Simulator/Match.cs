﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulator
{
    public class Match
    {
		Random r = new Random();
		public List<Player> Players { get; private set; }
		public List<Castle> Castles { get; private set; } = new List<Castle>();

		int waypointId = 0;

		//unit을 그 unit이 속한 path로 구분해서 기억함. unit은 먼저 출발한 놈이 항상 먼저 도착 - Queue 사용.
		public Dictionary<Path, SortedSet<Unit>> Units = new Dictionary<Path, SortedSet<Unit>>();

		public int Frame { get; private set; }

		public MatchOption Option { get; private set; }

		public Match(MatchOption option, List<Player> players)
		{
			Option = option;
			Players = players;
		}

		void MakeWaypoints()
		{
			//일단 지금은 성만 생성
			for (int i = 0; i < Option.CastleNum; i++)
			{
				int x = 0; 
				int y = 0;

				do
				{
					x = r.Next(0, Option.Width);
					y = r.Next(0, Option.Height);
				} while (Castles.Any(c => (c.Pos.X - x) * (c.Pos.X - x) + (c.Pos.Y - y) * (c.Pos.Y - y) < Option.CastleDistance * Option.CastleDistance));

				Castles.Add(new Castle(waypointId, Option.CastleStartUnitNum, x, y, Option.UnitRunRatio, Option.UpgradeInfo));
				waypointId++;
			}
		}

		public void InitPlayerCastle()
		{
			foreach (var p in Players)
			{
				int castleIndex = 0;

				do
				{
					castleIndex = r.Next(0, Castles.Count);
				} while (Players.Any(player => 
							player.OwnCastles.Any(c => // 기존 플레이어의 모든 성에 대해서 거리 조건 만족하는지 확인
								c.Pos.DistSquare(Castles[castleIndex].Pos) < Option.PlayerDistance * Option.PlayerDistance)));

				Castles[castleIndex].Owner = p;
				p.AddCastle(Castles[castleIndex]);
			}
		}

		public void Init()
		{
			//맵 생성
			MakeWaypoints();

			//플레이어한테 성 할당
			InitPlayerCastle();

			//플레이어별 초기화 동작
			foreach (var player in Players)
			{
				player.Init(this);
			}
		}

		public void Update()
		{
			//각 path에 있는 유닛들에 대한 결과를 처리한다
			foreach (var unitQueue in Units.Values)
			{
				if (unitQueue.Count == 0)
					continue;

				var head = unitQueue.Max;

				//해당 path의 제일 앞에 있는 유닛이 전투를 벌였는지 확인 - 그 결과에 따라 다르게 행동
				switch (head.Battle(this))
				{
					case BattleResult.NotBattle: //아무도 안 싸운 경우 모든 유닛 한 칸씩 전진
						foreach (var unit in unitQueue)
							unit.Move(this);

						break;
					case BattleResult.Draw: // 비긴 경우 맨 앞 없앤 후 나머지 전진. 성 공격과 같다.
					case BattleResult.AttackCastle: //성 공격시 head 제거, 나머지 한칸씩 전진
						unitQueue.Remove(unitQueue.Max);
						foreach (var unit in unitQueue)
							unit.Move(this);
						break;
					case BattleResult.Lose: //진 경우 head 제거, 나머지 정지(맨 앞이 죽고 싸움에서 패배했으므로)
						unitQueue.Remove(unitQueue.Max);
						break;
					case BattleResult.Win: //이긴 경우 전체 한 칸 전진(상대 맨 앞 처치했으므로)
						foreach (var unit in unitQueue)
							unit.Move(this);
						break;
					default:
						throw new NotImplementedException();
				}
			}

			//각 성 현재 상황을 업데이트한다
			foreach (var castle in Castles)
			{
				castle.Update(this);
			}

			//가진 성이 0개, 유닛도 없음 -> 멸망!
			Players.RemoveAll(p => p.OwnCastles.Count == 0 && Units.All(pair=>pair.Value.All(u=>u.Owner != p)));

			//플레이어의 액션에 따른 변화를 반영한다
			foreach (var player in Players)
			{
				player.Update(this);
			}

			Frame++;
		}

		public bool IsEnd()
		{
			return Players.Count <= 1;
		}

		public Unit FindEnemy(Unit unit, Path path)
		{
			if (!Units.ContainsKey(path.Reverse))
				return null;

			var queue = Units[path.Reverse];

			if (queue.Count == 0)
				return null;

			var firstEnemy = queue.Max;

			//공격 가능한 적이 있음
			if (unit.IsAttackable(firstEnemy))
			{
				return firstEnemy;
			}

			return null;
		}

		public void CreateUnit(int num, Player owner, float startOffset, Waypoint start, Waypoint end)
		{
			var path = new Path(start, end);
			if (!Units.ContainsKey(path))
				Units.Add(path, new SortedSet<Unit>());

			Units[path].Add(new Unit(num, start.Pos + startOffset * path.Dir, Option.UnitSpeed, Option.UnitAttackRange, owner, start, end));
		}

		//성이 공격 범위 내에 있는지 계산해서 돌려줌
		public bool IsInRange(Unit unit, Castle castle)
		{
			float range = Option.UnitAttackRange + castle.Radius;
			return unit.Pos.DistSquare(castle.Pos) < range * range;
		}
    }
}
