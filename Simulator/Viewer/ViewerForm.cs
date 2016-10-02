using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulator;
using Newtonsoft.Json;

namespace Viewer
{
    public partial class ViewerForm : Form
    {
		Match match;
		Timer timer = new Timer();

        public ViewerForm()
        {
            InitializeComponent();

			OptionPath.Text = Directory.GetCurrentDirectory() + "/Option.json";

			timer.Interval = 1000 / (int)PlayFrame.Value;

			timer.Tick += (object sender, EventArgs e) =>
			{
				if (manualCheckBox.Checked || match == null || match.IsEnd())
					return;

				match.Update();

				draw.Invalidate();
			};

			timer.Start();
        }

		private void startButton_Click(object sender, EventArgs e)
		{
			var json = File.ReadAllText(OptionPath.Text);
			var option = JsonConvert.DeserializeObject<MatchOption>(json);

			List<Player> players = new List<Player>();

			var colors = new List<Color> { Color.OrangeRed, Color.Cyan, Color.LightGreen, Color.Yellow, Color.Pink };

			for (int i = 0; i < PlayerNumUpDown.Value; i++)
			{
				players.Add(new AIView(colors[i]));
			}

			match = new Match(option, players);

			match.Init();

			draw.Invalidate();
		}

		private void OptionLoadingButton_Click(object sender, EventArgs e)
		{
			var window = new OpenFileDialog();

			window.Filter = ".json";

			if (window.ShowDialog() == DialogResult.OK)
			{
				OptionPath.Text = window.FileName;
			}
		}

		private void manualCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			nextFrameButton.Visible = manualCheckBox.Checked;
		}

		private void nextFrameButton_Click(object sender, EventArgs e)
		{
			if (match.IsEnd())
				return;

			match.Update();

			draw.Invalidate();
		}

		private void draw_Paint(object sender, PaintEventArgs e)
		{
			if (match == null)
				return;
			foreach (var unitQueue in match.Units.Values)
			{
				foreach (var unit in unitQueue)
				{
					var ai = unit.Owner as AIView;
					Brush brush = new SolidBrush(ai.AIColor);
					float radius = (float)Math.Sqrt(unit.Num) * 3.0f;

					e.Graphics.FillEllipse(brush, unit.Pos.X - radius / 2, unit.Pos.Y - radius / 2, radius, radius);

				}
			}

			StringFormat format = new StringFormat();
			format.LineAlignment = StringAlignment.Center;
			format.Alignment = StringAlignment.Center;

			foreach (var c in match.Castles)
			{
				Brush brush;
				var ai = c.Owner as AIView;

				if (ai == null)
					brush = new SolidBrush(Color.White);
				else
					brush = new SolidBrush(ai.AIColor);

				e.Graphics.FillEllipse(brush, c.Pos.X - c.Radius, c.Pos.Y - c.Radius, c.Radius * 2, c.Radius * 2);
				e.Graphics.DrawEllipse(new Pen(Color.Black), c.Pos.X - c.Radius, c.Pos.Y - c.Radius, c.Radius * 2, c.Radius * 2);
				e.Graphics.DrawString(c.UnitNum.ToString(), SystemFonts.DefaultFont, Brushes.Black, c.Pos.X, c.Pos.Y, format);
			}
		}

		private void PlayFrame_ValueChanged(object sender, EventArgs e)
		{
			timer.Interval = 1000 / (int)PlayFrame.Value;
		}
	}
}
