using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Deer : Herbivore
	{
		public Deer(int _x, int _y, World world) : base(_x, _y, world)
		{
			LimitSatur = 25;
			LimitAge = 100;
			Saturation = 50;
			LimitSatiety = 20;
			FoodList.Add(typeof(Grass));
			FoodList.Add(typeof(Bush));
			FoodList.Add(typeof(Tree));
			Speed = 1;
			SetColor("80490b");
			SetSize(8);
		}
	}
}
