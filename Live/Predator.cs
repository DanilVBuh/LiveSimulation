using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	abstract class Predator : Animal
	{
		public Predator(double _x, double _y, World world) : base(_x, _y, world)
		{
			LimitSatur = 35;
			Speed = 1.5;
		}
		public override void FindFood()
		{
			Animal nearestFood = null;
			double dist = Math.Sqrt(Math.Pow(World.Height, 2) + Math.Pow(World.Width, 2)) + 1;
			double max = dist;
			foreach (Animal p in World.Animals)
			{
				if (FoodList.Contains(p.GetType()))
				{
					double d = Math.Sqrt(Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2));
					if (d < dist)
					{
						dist = d;
						nearestFood = p;
					}
				}
			}
			if (dist > Speed)
			{
				if (dist != max)
					StepToFood(nearestFood);
				else
				{
					RandomStep();
				}
			}
			else
			{
				EatFood(nearestFood);
				RandomStep();
			}
		}

	}
}

