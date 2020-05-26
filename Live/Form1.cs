using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Live
{
	public partial class Form1 : Form
	{
		Graphics g;
		Timer timer = new Timer();
		World world;
		int mx;
		int my;
		string mode = "satiety";
		Rectangle tile;
		ColorConverter cc = new ColorConverter();
		public Form1()
		{
			world = new World(80, 40, 16);
			//g = this.CreateGraphics();
			timer.Enabled = true;
			timer.Interval = 4;  /* 100 millisec */
			timer.Tick += new EventHandler(TimerCallback);

			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
		protected override void OnPaint(PaintEventArgs e)
		{
			g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			for (int i = 0; i < world.Height; i++)
			{
				for (int j = 0; j < world.Width; j++)
				{
					tile = new Rectangle(j * world.TileSize, i * world.TileSize, world.TileSize, world.TileSize);
					Color color = (Color)cc.ConvertFromString("#" + "3bc464");
					SolidBrush b = new SolidBrush(color);
					g.FillRectangle(Brushes.Green, tile);
					//g.DrawRectangle(new Pen(Color.Black), tile);
				}
			}
			foreach (Plant p in this.world.Plants)
			{
				Color color = (Color)cc.ConvertFromString("#" + p.Color);
				SolidBrush b = new SolidBrush(color);
				g.FillEllipse(b, (float)p.x * world.TileSize + world.TileSize * (float)(1 - p.Size / 10) / 2, (float)p.y * world.TileSize + world.TileSize * (float)(1 - p.Size / 10) / 2, world.TileSize * p.Size / 10, world.TileSize * p.Size / 10);
			}
			foreach (Farm p in this.world.Farms)
			{
				Color color = (Color)cc.ConvertFromString("#" + p.Color);
				SolidBrush b = new SolidBrush(color);
				g.FillEllipse(b, (float)p.x * world.TileSize + world.TileSize * (float)(1 - p.Size / 10) / 2, (float)p.y * world.TileSize + world.TileSize * (float)(1 - p.Size / 10) / 2, world.TileSize * p.Size / 10, world.TileSize * p.Size / 10);
			}
			foreach (Animal a in this.world.Animals)
			{
				Color color = (Color)cc.ConvertFromString("#" + a.Color);
				SolidBrush b = new SolidBrush(color);
				g.FillEllipse(b, (float)a.x * world.TileSize + world.TileSize * (float)(1 - a.Size / 10) / 2, (float)a.y * world.TileSize + world.TileSize * (float)(1 - a.Size / 10) / 2, world.TileSize * a.Size / 10, world.TileSize * a.Size / 10);
				/*if (a.GetType().Equals(typeof(Person)))
					g.FillEllipse(Brushes.LightSalmon, (float)a.x * world.TileSize + world.TileSize / 5, (float)a.y * world.TileSize + world.TileSize / 5, world.TileSize * 3 / 5, world.TileSize * 3 / 5);
				if (a.GetType().Equals(typeof(Predator)))
					g.FillEllipse(Brushes.Brown, (float)a.x * world.TileSize + world.TileSize / 5, (float)a.y * world.TileSize + world.TileSize / 5, world.TileSize * 3 / 5, world.TileSize * 3 / 5);
				if (a.GetType().Equals(typeof(Herbivore)))
					g.FillEllipse(Brushes.Gray, (float)a.x * world.TileSize + world.TileSize / 5, (float)a.y * world.TileSize + world.TileSize / 5, world.TileSize * 3 / 5, world.TileSize * 3 / 5);*/
				if (a.IsClicked)
				{
					g.DrawString(a.Satiety.ToString(), new Font("Arial", (world.TileSize + 2) / 3), Brushes.DarkBlue, (float)a.x * world.TileSize + world.TileSize / 7, (float)a.y * world.TileSize + world.TileSize / 5);
					//g.DrawString(a.Satiety.ToString(), this.Font, Brushes.DarkBlue, a.x * world.TileSize + world.TileSize / 5, a.y * world.TileSize + world.TileSize / 5);
				}
			}
			foreach (House p in this.world.Houses)
			{
				Color color = (Color)cc.ConvertFromString("#" + p.Color);
				SolidBrush b = new SolidBrush(color);
				g.FillEllipse(b, (float)p.x * world.TileSize + world.TileSize * (float)(1 - p.Size / 10) / 2, (float)p.y * world.TileSize + world.TileSize * (float)(1 - p.Size / 10) / 2, world.TileSize * p.Size / 10, world.TileSize * p.Size / 10);
			}
			base.OnPaint(e);
		}

		private void TimerCallback(object sender, EventArgs e)
		{
			this.world.UpdateWorld();
			this.Invalidate();
			return;
		}
		private void Control1_MouseClick(Object sender, MouseEventArgs e)
		{
			mx = e.X / world.TileSize;
			my = e.Y / world.TileSize;
			if (mode == "satiety")
				foreach (Animal a in world.Animals)
				{
					if (a.x == mx && a.y == my)
					{
						a.IsClicked = !a.IsClicked;
					}
				}
			/*if (mode == "person")
			{
				world.Animals.Add(new Person(mx, my, this.world));
			}
			if (mode == "predator")
			{
				world.Animals.Add(new Predator(mx, my, this.world));
			}
			if (mode == "herbivore")
			{
				world.Animals.Add(new Herbivore(mx, my, this.world));
			}
			if (mode == "plant")
			{
				world.Plants.Add(new Plant(mx, my, this.world));
			}*/
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			/*if (e.Control)
			{*/
			string kc = e.KeyCode.ToString();
			if (kc == "Z") { mode = "satiety"; }
			if (kc == "X") { mode = "person"; }
			if (kc == "C") { mode = "predator"; }
			if (kc == "V") { mode = "herbivore"; }
			if (kc == "B") { mode = "plant"; }
			if (kc == "Q") { timer.Interval = Math.Min(timer.Interval * 5 / 4, 10000); }
			if (kc == "A") { timer.Interval = Math.Max(timer.Interval * 4 / 5, 4); }
			/*}*/
		}
	}
}
