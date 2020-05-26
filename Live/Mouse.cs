using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Mouse : Herbivore
	{
		public Mouse(int _x, int _y, World world) : base(_x, _y, world)
		{
			LimitSatur = 25;
			LimitAge = 100;
			Saturation = 20;
			LimitSatiety = 25;
			FoodList.Add(typeof(Grass));
			Speed = 0.5;
			SetColor("94908b");
			SetSize(2);
		}
	}
}
