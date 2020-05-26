using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	abstract class Plant : Unit
	{
		public int Saturation { get; set; }
		public int Count { get; set; }
		public Plant(double _x, double _y, World world) : base(_x, _y, world)
		{
			this.Saturation = 25;
			this.Count = 1;
		}
		public bool Eat()
		{
			this.Count--;
			if (Count == 0)
				return true;
			return false;
		}
	}
}
