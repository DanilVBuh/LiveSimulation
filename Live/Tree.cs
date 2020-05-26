using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Tree : Plant
	{
		public Tree(double _x, double _y, World world) : base(_x, _y, world)
		{
			this.Saturation = 15;
			this.Count = 3;
			SetColor("0f6b0f");
			SetSize(8);
		}
	}
}
