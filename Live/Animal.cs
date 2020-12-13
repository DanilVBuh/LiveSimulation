using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	abstract class Animal : Unit
	{
		public string Gender { get; set; }
		public int Satiety { get; set; }
		public int Age { get; set; }
		public int LimitAge { get; set; } = 3000;
		public int LimitSatiety { get; set; } = 30;
		public int Saturation { get; set; }
		public int Satur { get; set; }
		public int LimitSatur { get; set; } = 50;
		public Animal Goal { get; set; } = null;
		public bool IsDead { get; set; } = false;
		public bool IsClicked { get; set; } = false;
		public double Speed { get; set; } = 1;
		public ICollection<Type> FoodList { get; set; } = new List<Type>();
		//Random random = new Random();
		public Animal(double _x, double _y, World world) : base(_x, _y, world)
		{
			this.Age = 0;
			this.Satiety = 60;
			this.Saturation = 35;
			this.Saturation = world.random.Next(20);
			this.Gender = SetGender();
		}

		public virtual void UpdateAnimal()
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
					FindPair();
				}
				else
				{
					RandomStep();
				}
			}
		}

		//copy
		public virtual void FindFood()
		{
		}

		public bool IsReadyFor()
		{
			if (Satur > LimitSatur)
			{
				return true;
			}
			return false;
		}

		public void FindPair()
		{
			if (Goal == null)
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
			else
			{
				StepToGoal();
			}
		}

		public void StepToGoal()
		{
			if (!IsInOne())
			{
				if (Goal.x < x)
					x -= Speed;
				else
				{
					if (Goal.x > x)
						x += Speed;
				}
				if (Goal.y < y)
					y -= Speed;
				else
				{
					if (Goal.y > y)
						y += Speed;
				}
			}
			else
			{
				CreateCreature();
			}
		}

		public virtual void CreateCreature()
		{
			Animal other = (Animal)this.MemberwiseClone();
			other.Age = 0;
			other.Goal = null;
			other.Satiety = 90;
			other.Satur = 0;
			other.Gender = SetGender();
			this.World.Animals.Add(other);
			Goal.Satur = 0;
			Goal.Satiety = Goal.LimitSatiety * 3;
			Goal.Goal = null;
			Satur = 0;
			Satiety = LimitSatiety * 3;
			Goal = null;
		}

		/*		public class NewBaby<T> where T : Animal, new()
				{
					public T GetInstance()
					{
						return new T();
					}
				}*/

		protected void RandomStep()
		{
			this.x += Speed * (World.random.Next(3) - 1);
			this.y += Speed * (World.random.Next(3) - 1);
			if (x < 0) x = 0;
			if (x >= World.Width) x = World.Width - 1;
			if (y < 0) y = 0;
			if (y >= World.Height) y = World.Height - 1;
		}

		protected void StepToFood(Unit food)
		{
			if (food.x < x)
				x -= Speed;
			else
			{
				if (food.x > x)
					x += Speed;
			}
			if (food.y < y)
				y -= Speed;
			else
			{
				if (food.y > y)
					y += Speed;
			}
		}

		public virtual void EatFood(Plant food)
		{
			Satiety += food.Saturation * 10 / (int)Size;
			if (food.Eat())
				World.Plants.Remove(food);
		}
		public virtual void EatFood(Animal food)
		{
			Satiety += food.Saturation;
			World.Animals.Remove(food);
		}

		public bool HavePair()
		{
			if (Goal == null)
				return false;
			return true;
		}

		public bool DifferentGender(Animal animal)
		{
			if (this.Gender == animal.Gender)
				return false;
			return true;
		}

		public bool IsInOne()
		{
			if ((this.x - Goal.x >= this.Speed) || (Goal.x - this.x >= this.Speed) || (this.y - Goal.y >= this.Speed) || (Goal.y - this.y >= this.Speed))
			{
				return false;
			}
			return true;
		}

		public string SetGender()
		{
			if (World.random.Next(2) == 1)
			{
				return "man";
			}
			else
			{
				return "woman";
			}
		}
	}
}
