using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class Person : Animal
	{
		public House House { get; set; }
		public Farm Farm { get; set; }
		private bool FarmOrHome { get; set; } = false;
		private bool ThoughtAboutFarm { get; set; } = false;
		public Person(double _x, double _y, World world) : base(_x, _y, world)
		{
			LimitAge = 1000;
			LimitSatur = 70;
			SetColor("dea27c");
			SetSize(7);
		}
		public override void UpdateAnimal()
		{
			Satiety--;
			Satur++;
			Age++;
			if (Age > LimitAge)
				IsDead = true;
			else
			{
				if (Satiety < LimitSatiety)
				{
					FindFood();
					if (Satiety < 1)
					{
						//World.Animals.Remove(this);
						IsDead = true;
					}
				}
				else if (IsReadyFor())
				{
					if (Goal == null)
					{
						if (House != null)
							House = null;
						CreateFamily();
					}
					else if (Goal.IsReadyFor())
					{
						StepToGoal();
					}
					else
						Step();
				}
				else if (House == null && Goal != null)
				{
					FindPlaysForHouse();
				}
				else
				{
					Step();
				}
			}
		}
		public void Step()
		{
			if (House == null)
			{
				RandomStep();
			}
			else
			{
				if (Goal != null && House != null && ThoughtAboutFarm)
				{
					if (World.random.Next(100) < 5)
						FarmOrHome = !FarmOrHome;
					if (FarmOrHome)
						StepToHome(House);
					else
						ThinkAboutFarm();
				}
				else
					StepToHome(House);
			}
		}
		public void StepToHome(Unit place)
		{
			if (place.x < x)
				x--;
			else
			{
				if (place.x > x)
					x++;
			}
			if (place.y < y)
				y--;
			else
			{
				if (place.y > y)
					y++;
			}
		}
		public void CreateFamily()
		{
			double dist = Math.Sqrt(Math.Pow(World.Height, 2) + Math.Pow(World.Width, 2)) + 1;
			if (World.AmountOfAnimals(this.GetType()) != 0)
			{
				foreach (Animal p in World.Animals)
				{
					if (p.GetType().Equals(this.GetType()) && p.DifferentGender(this) && !p.HavePair() && p.IsReadyFor())
					{
						double d = Math.Sqrt(Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2));
						if (d < dist)
						{
							dist = d;
							Goal = p;
						}
					}
				}
				if (Goal != null)
				{
					Goal.Goal = this;
				}
				else
				{
					RandomStep();
				}
			}
			else
			{
				RandomStep();
			}

		}
		public void FindPlaysForHouse()
		{
			House nearest = null;
			double dist = Math.Sqrt(Math.Pow(World.Height, 2) + Math.Pow(World.Width, 2)) + 1;
			if (World.Houses.Count != 0)
			{
				foreach (House p in World.Houses)
				{
					double d = Math.Sqrt(Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2));
					if (d < dist)
					{
						dist = d;
						nearest = p;
					}

				}
				if (dist < 50)
				{
					StepToPlace(nearest);
				}
				else
				{
					BuildHouse();
				}
			}
			else
			{
				BuildHouse();
			}
		}
		public void BuildHouse()
		{
			World.Houses.Add(new House(x, y, World, this, (Person)Goal));
		}
		public void StepToPlace(Unit place)
		{
			if (place.x + 1 < x)
				x--;
			else
			{
				if (place.x - 1 > x)
					x++;
			}
			if (place.y + 1 < y)
				y--;
			else
			{
				if (place.y - 1 > y)
					y++;
			}
			//if ((place.x == x && (place.y - y == 1 || place.y - y == -1)) || (place.y == y && (place.x - x == 1 || place.x - x == -1)))
			if ((place.x - x == 1 || place.x - x == -1) && (place.y - y == 1 || place.y - y == -1))
				BuildHouse();
		}
		public override void CreateCreature()
		{
			if (House == null)
			{
				FindPlaysForHouse();
			}
			else
			{
				if (World.random.Next(100) < 20)
				{
					Animal other = (Animal)this.MemberwiseClone();
					other.Age = 0;
					other.Goal = null;
					other.Satiety = 70;
					other.Satur = -30;
					other.Gender = SetGender();
					this.World.Animals.Add(other);
				}
				Goal.Satur = -20;
				Goal.Satiety = 70;
				Satur = -20;
				Satiety = 70;
			}
		}
		public override void FindFood()
		{
			Plant nearestFood = null;
			Animal nearestAnimal = null;
			double distPlant = Math.Sqrt(Math.Pow(World.Height, 2) + Math.Pow(World.Width, 2)) + 1;
			double distAnimal = Math.Sqrt(Math.Pow(World.Height, 2) + Math.Pow(World.Width, 2)) + 1;
			if (World.Plants.Count != 0)
			{
				foreach (Plant p in World.Plants)
				{
					double d = Math.Sqrt(Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2));
					if (d < distPlant)
					{
						distPlant = d;
						nearestFood = p;
					}
				}
				if (World.Animals.Count - World.AmountOfAnimals(typeof(Person)) - World.AmountOfAnimals(typeof(Predator)) != 0)
				{
					foreach (Animal p in World.Animals)
					{
						if (p != this && !p.GetType().Equals(typeof(Person)) && !p.GetType().Equals(typeof(Predator)))
						{
							double d = Math.Sqrt(Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2));
							if (d < distAnimal)
							{
								distAnimal = d;
								nearestAnimal = p;
							}
						}
					}
				}
				if (distPlant < Speed)
				{
					EatFood(nearestFood);
					RandomStep();
				}
				else if (distAnimal < Speed)
				{
					EatFood(nearestAnimal);
					RandomStep();
				}
				else if (distAnimal < distPlant)
				{
					if (distAnimal > 6 && Goal != null && House != null)
					{
						ThoughtAboutFarm = true;
						ThinkAboutFarm();
					}
					else
						StepToFood(nearestAnimal);
				}
				else
				{
					if (distPlant > 6 && Goal != null && House != null)
					{
						ThoughtAboutFarm = true;
						ThinkAboutFarm();
					}
					else
						StepToFood(nearestFood);
				}
			}
			else
				RandomStep();
		}

		public void ThinkAboutFarm()
		{
			if (Farm == null)
				FindPlaceForFarm();
			else
			{
				if (IsOnFarm())
				{
					if (Satiety < LimitSatiety)
						EatFromFarm();
					else
						Work();
				}
				else
				{
					StepToFarm();
				}
			}

		}

		public void FindPlaceForFarm()
		{
			if (House.x + 1 < x)
				x--;
			else
			{
				if (House.x + 1 > x)
					x++;
			}
			if (House.y < y)
				y--;
			else
			{
				if (House.y > y)
					y++;
			}
			if (House.x - x == -1 && House.y == y)
				BuildFarm();
		}

		public void BuildFarm()
		{
			World.Farms.Add(new Farm(x, y, World, this, (Person)Goal));
		}

		public bool IsOnFarm()
		{
			if (x == Farm.x && y == Farm.y)
			{
				return true;
			}
			return false;
		}

		public void StepToFarm()
		{
			if (Farm.x < x)
				x--;
			else
			{
				if (Farm.x > x)
					x++;
			}
			if (Farm.y < y)
				y--;
			else
			{
				if (Farm.y > y)
					y++;
			}
		}

		public void EatFromFarm()
		{
			if (Farm.Saturation >= 10)
			{
				Satiety += 10;
				Farm.Saturation -= 10;
			}
			else
			{
				Work();
			}

		}

		public void Work()
		{
			Farm.Working();
		}
	}
}