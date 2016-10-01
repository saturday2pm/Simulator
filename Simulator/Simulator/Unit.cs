using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
	public enum BattleResult
	{
		NotBattle, // 아무하고도 안 마주침
		AttackCastle, //성 공격(무조건 뒤짐)
		Win, //상대와 싸워이김(살았음)
		Lose, //상대와 싸워서 졌음(죽었음)
	}

	public class Point
	{
		public float X { get; set; }
		public float Y { get; set; }

		public Point()
		{
			X = 0;
			Y = 0;
		}

		public Point(float x, float y)
		{
			X = x;
			Y = y;
		}

		public void Normalize()
		{
			float norm = (float)Math.Sqrt((X * X + Y * Y));

			X = X / norm;
			Y = Y / norm;
		}
	}

    public class Unit
    {
		//유닛의 현재 좌표
		public Point Pos { get; private set; }
		//해당 유닛의 병력 크기
		public int Num { get; private set; }

		public Player Owner { get; private set; }

		//이 유닛이 있는 경로 - 전투 판단할 때 쓰임
		public Path Road { get; private set; }

		public Unit(float x, float y, Player owner, Waypoint startPoint, Waypoint endPoint)
		{
			Pos.X = x;
			Pos.Y = y;

			Owner = owner;

			Road = new Path(startPoint, endPoint);
		}

		public Unit(Point pos, Player owner, Waypoint startPoint, Waypoint endPoint)
		{
			Pos = pos;
			Owner = owner;

			Road = new Path(startPoint, endPoint);
		}

		public BattleResult Battle(Match match)
		{
			return BattleResult.NotBattle;
		}

		//속도에 맞게 지정된 경로 따라 이동함
		public void Move(Match match)
		{
		}
	}
}
