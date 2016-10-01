using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
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

		//이 유닛이 출발한 지점 - endPoint와 함께 전투 판단할 때 쓰임
		public Waypoint StartPoint { get; private set; }

		//이 유닛의 목적 공격 지점
		public Waypoint EndPoint { get; private set; }

		public Unit(float x, float y, Player owner, Waypoint startPoint, Waypoint endPoint)
		{
			Pos.X = x;
			Pos.Y = y;

			Owner = owner;

			StartPoint = startPoint;
			EndPoint = endPoint;
		}

		public Unit(Point pos, Player owner, Waypoint startPoint, Waypoint endPoint)
		{
			Pos = pos;
			Owner = owner;

			StartPoint = startPoint;
			EndPoint = endPoint;
		}

		public void Update(Match match)
		{
		}
	}
}
