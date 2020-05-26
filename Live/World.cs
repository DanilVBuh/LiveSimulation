using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	class World
	{
		public ICollection<Plant> Plants = new List<Plant>();
		public ICollection<Animal> Animals = new List<Animal>();
		public ICollection<House> Houses = new List<House>();
		public ICollection<Farm> Farms = new List<Farm>();
		public int Width { get; set; }
		public int Height { get; set; }
		public int TileSize { get; set; }
		public Random random = new Random();
		public World(int width, int height, int tilesize)
		{
			this.Width = width;
			this.Height = height;
			this.TileSize = tilesize;
			GeneratePlants(45);
			GeneratePersons(25);
			GenerateHerbivores(28);
			GeneratePredators(17);
		}
		public void GeneratePlants(int p)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (random.Next(1000) < p)
					{
						NewPlant(j, i);
					}
				}
			}
		}

		public void GeneratePersons(int p)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (random.Next(1000) < p)
					{
						Animals.Add(new Person(j, i, this));
					}
				}
			}
		}

		public void GeneratePredators(int p)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (random.Next(1000) < p)
					{
						NewPredator(j, i);
					}
				}
			}
		}

		public void GenerateHerbivores(int p)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (random.Next(1000) < p)
					{
						NewHerbivore(j, i);
					}
				}
			}
		}

		private bool PlantExist(int x, int y)
		{
			foreach (Plant p in this.Plants)
			{
				if (p.x == x && p.y == y)
					return true;
			}
			return false;
		}

		public void UpdateWorld()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (random.Next(10000) < 7 && !PlantExist(j, i))
					{
						NewPlant(j, i);
					}
				}
			}
			for (int i = Animals.Count - 1; i >= 0; i--)
			{
				Animal a = Animals.ElementAt(i);
				a.UpdateAnimal();
				if (a.IsDead) Animals.Remove(a);
			}
		}
		public int AmountOfAnimals(Type type)
		{
			int amount = 0;
			foreach (Animal a in Animals)
			{
				if (a.GetType().Equals(type))
					amount++;
			}
			return amount;
		}
		public void NewPlant(int j, int i)
		{
			int r = random.Next(10);
			if (r < 5)
				Plants.Add(new Grass(j, i, this));
			else if (r < 8)
				Plants.Add(new Bush(j, i, this));
			else
				Plants.Add(new Tree(j, i, this));
		}
		public void NewHerbivore(int j, int i)
		{
			int r = random.Next(10);
			if (r < 4)
				Animals.Add(new Hare(j, i, this));
			else if (r < 6)
				Animals.Add(new Deer(j, i, this));
			else
				Animals.Add(new Mouse(j, i, this));
		}
		public void NewPredator(int j, int i)
		{
			int r = random.Next(10);
			if (r < 3)
				Animals.Add(new Wolf(j, i, this));
			else if (r < 6)
				Animals.Add(new Fox(j, i, this));
			else
				Animals.Add(new Owl(j, i, this));
		}
		public int GetRandom(int r)
		{
			return random.Next(r);
		}
	}
}
