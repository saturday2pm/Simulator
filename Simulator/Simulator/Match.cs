using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public class Match
    {
		public List<Player> Players { get; private set; }
		public List<Castle> Castles { get; private set; }
		public List<Unit> Units { get; private set; }

		public int Frame { get; private set; }

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

			foreach (var player in Players)
			{
				player.Update(this);
			}
		}

		public bool IsEnd()
		{
			return false;
		}
    }
}
