using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Owl : Predator
	{
		public Owl(double _x, double _y, World world) : base(_x, _y, world)
		{
			LimitSatur = 35;
			LimitAge = 120;
			Saturation = 25;
			LimitSatiety = 15;
			FoodList.Add(typeof(Mouse));
			FoodList.Add(typeof(Hare));
			Speed = 2;
			SetColor("d560db");
			SetSize(4);
		}
	}
}
