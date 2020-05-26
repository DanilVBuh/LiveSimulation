using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Hare : Herbivore
	{
		public Hare(int _x, int _y, World world) : base(_x, _y, world)
		{
			LimitSatur = 20;
			LimitAge = 80;
			Saturation = 35;
			LimitSatiety = 30;
			FoodList.Add(typeof(Grass));
			FoodList.Add(typeof(Bush));
			Speed = 1.5;
			SetColor("0fab8c");
			SetSize(5);
		}
	}
}
