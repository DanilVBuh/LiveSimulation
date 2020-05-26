using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Fox : Predator
	{
		public Fox(double _x, double _y, World world) : base(_x, _y, world)
		{
			LimitSatur = 30;
			LimitAge = 120;
			Saturation = 30;
			LimitSatiety = 20;
			FoodList.Add(typeof(Mouse));
			FoodList.Add(typeof(Hare));
			Speed = 1.5;
			SetColor("ff4b1a");
			SetSize(6);
		}
	}
}
