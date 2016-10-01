using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
	public abstract class Player
	{
		public List<Castle> OwnCastles { get; private set; } = new List<Castle>();

		public abstract void Init(Match match);
		public abstract void Update(Match match);
    }
}
