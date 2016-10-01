using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulator
{
    public class Match
    {
		public List<Player> Players { get; private set; }
		public List<Castle> Castles { get; private set; }

		int waypointId = 0;

		//unit을 그 unit이 속한 path로 구분해서 기억함. unit은 먼저 출발한 놈이 항상 먼저 도착 - Queue 사용.
		public Dictionary<Path, Queue<Unit>> Units;

		public int Frame { get; private set; }

		public int width { get; private set; }
		public int Height { get; private set; }

		public float UnitIncreaseRatio { get; private set; }
		public float UnitSpeed { get; private set; }

		public void Init()
		{
			//맵 생성

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
				var head = unitQueue.Peek();

				//해당 path의 제일 앞에 있는 유닛이 전투를 벌였는지 확인 - 그 결과에 따라 다르게 행동
				switch (head.Battle(this))
				{
					case BattleResult.NotBattle: //아무도 안 싸운 경우 모든 유닛 한 칸씩 전진
						foreach (var unit in unitQueue)
							unit.Move(this);

						break;
					case BattleResult.AttackCastle: //성 공격시 head 제거, 나머지 한칸씩 전진
						unitQueue.Dequeue();
						foreach (var unit in unitQueue)
							unit.Move(this);
						break;
					case BattleResult.Lose: //진 경우 head 제거, 나머지 정지(맨 앞이 죽고 싸움에서 패배했으므로)
						unitQueue.Dequeue();
						break;
					case BattleResult.Win: //이긴 경우 전체 한 칸 전진(상대 맨 앞 처치했으므로)
						foreach (var unit in unitQueue)
							unit.Move(this);
						break;
				}
			}

			//각 성 현재 상황을 업데이트한다
			foreach (var castle in Castles)
			{
				castle.Update(this);
			}

			//플레이어의 액션에 따른 변화를 반영한다
			foreach (var player in Players)
			{
				player.Update(this);
			}

			Frame++;
		}

		public bool IsEnd()
		{
			return false;
		}
    }
}
