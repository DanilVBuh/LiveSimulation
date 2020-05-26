using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Grass : Plant
	{
		public Grass(double _x, double _y, World world) : base(_x, _y, world)
		{
			this.Saturation = 20;
			this.Count = 1;
			SetColor("16b526");
			SetSize(3);
		}
	}
}
