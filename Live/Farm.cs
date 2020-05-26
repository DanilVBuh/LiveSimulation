using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Farm : Unit
	{
		public ICollection<Person> Livers = new List<Person>();
		public double Saturation { get; set; } = 0;
		public double Speed { get; set; } = 1.5;
		public Farm(double _x, double _y, World world, Person p1, Person p2) : base(_x, _y, world)
		{
			Livers.Add(p1);
			Livers.Add(p2);
			p1.Farm = this;
			p2.Farm = this;
			SetColor("f7ff00");
			SetSize(9);
		}

		public void Working()
		{
			Saturation += Speed;
		}
	}
}
