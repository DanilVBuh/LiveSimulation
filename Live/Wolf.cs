using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Wolf : Predator
	{
		public Wolf
			(double _x, double _y, World world) : base(_x, _y, world)
		{
			LimitSatur = 35;
			LimitAge = 120;
			Saturation = 35;
			LimitSatiety = 15;
			FoodList.Add(typeof(Deer));
			FoodList.Add(typeof(Mouse));
			FoodList.Add(typeof(Hare));
			Speed = 1;
			SetColor("181d29");
			SetSize(7);
		}
	}
}
