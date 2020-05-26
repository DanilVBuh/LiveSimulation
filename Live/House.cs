using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class House : Unit
	{
		public ICollection<Person> Livers = new List<Person>();
		
		public House(double _x, double _y, World world, Person p1, Person p2) : base(_x, _y, world)
		{
			Livers.Add(p1);
			Livers.Add(p2);
			p1.House = this;
			p2.House = this;
			SetColor("382007");
			SetSize(10);
		}
	}
}
