using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Bush : Plant
	{
		public Bush(double _x, double _y, World world) : base(_x, _y, world)
		{
			this.Saturation = 30;
			this.Count = 2;
			SetColor("114f29");
			SetSize(6);
		}
	}
}
