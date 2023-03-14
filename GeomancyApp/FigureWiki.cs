using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeomancyApp
{
    public partial class FigureWiki : Form
    {
        public FigureWiki()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

            if (label1.Text == "  ◆")
            {
                label1.Text = "◆ ◆";
                label1.Tag = 2;
            }
            else
            {
                label1.Text = "  ◆";
                label1.Tag = 1;
            }
            FigureData figureData = new FigureData();

            FigureData FigFill = figureData.ReturnFigureData(FigureName(Convert.ToInt32(label1.Tag), Convert.ToInt32(label3.Tag), Convert.ToInt32(label4.Tag), Convert.ToInt32(label5.Tag)));
            UpdateText(FigFill);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (label3.Text == "  ◆")
            {
                label3.Text = "◆ ◆";
                label3.Tag = 2;
            }
            else
            {
                label3.Text = "  ◆";
                label3.Tag = 1;
            }
            FigureData figureData = new FigureData();

            FigureData FigFill = figureData.ReturnFigureData(FigureName(Convert.ToInt32(label1.Tag), Convert.ToInt32(label3.Tag), Convert.ToInt32(label4.Tag), Convert.ToInt32(label5.Tag)));
            UpdateText(FigFill);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (label4.Text == "  ◆")
            {
                label4.Text = "◆ ◆";
                label4.Tag = 2;
            }
            else
            {
                label4.Text = "  ◆";
                label4.Tag = 1;
            }
            FigureData figureData = new FigureData();

            FigureData FigFill = figureData.ReturnFigureData(FigureName(Convert.ToInt32(label1.Tag), Convert.ToInt32(label3.Tag), Convert.ToInt32(label4.Tag), Convert.ToInt32(label5.Tag)));
            UpdateText(FigFill);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (label5.Text == "  ◆")
            {
                label5.Text = "◆ ◆";
                label5.Tag = 2;
            }
            else
            {
                label5.Text = "  ◆";
                label5.Tag = 1;
            }
            FigureData figureData = new FigureData();

            FigureData FigFill = figureData.ReturnFigureData(FigureName(Convert.ToInt32(label1.Tag), Convert.ToInt32(label3.Tag), Convert.ToInt32(label4.Tag), Convert.ToInt32(label5.Tag)));
            UpdateText(FigFill);
        }
        public void UpdateText(FigureData fig)
        {
            FigName.Text = fig.Name;
            OtherNames.Text = fig.OtherNames;
            InnerElement.Text = fig.InnerEl;
            OutterElement.Text = fig.OutterEl;
            Quality.Text = fig.Quailty;
            Planet.Text = fig.Planet;
            Sign.Text = fig.Sign;
            Keyword.Text = fig.Keyword;
            DivinaryMeaning.Text = fig.DivinaryMeaning;
            Imagery.Text = fig.Imagery;
            
        }
        public string FigureName(int h1, int h2, int h3, int h4)
        {

            if (h1 == 1 && h2 == 1 && h3 == 1 && h4 == 1)
            {
                return "Via";
            }
            else if (h1 == 2 && h2 == 2 && h3 == 2 && h4 == 2)
            {
                return "Populus";
            }
            else if (h1 == 1 && h2 == 2 && h3 == 2 && h4 == 2)
            {
                return "Laetitia";
            }
            else if (h1 == 1 && h2 == 1 && h3 == 2 && h4 == 2)
            {
                return "Fortuna Minor";
            }
            else if (h1 == 1 && h2 == 1 && h3 == 1 && h4 == 2)
            {
                return "Cauda Draconis";
            }
            else if (h1 == 2 && h2 == 1 && h3 == 1 && h4 == 2)
            {
                return "Conjunctio";
            }
            else if (h1 == 1 && h2 == 1 && h3 == 2 && h4 == 1)
            {
                return "Puer";
            }
            else if (h1 == 2 && h2 == 2 && h3 == 2 && h4 == 1)
            {
                return "Trisititia";
            }
            else if (h1 == 1 && h2 == 2 && h3 == 2 && h4 == 1)
            {
                return "Carcer";
            }
            else if (h1 == 2 && h2 == 2 && h3 == 1 && h4 == 1)
            {
                return "Fortuna Major";
            }
            else if (h1 == 2 && h2 == 1 && h3 == 1 && h4 == 1)
            {
                return "Caput Draconis";
            }
            else if (h1 == 1 && h2 == 2 && h3 == 1 && h4 == 1)
            {
                return "Puella";
            }
            else if (h1 == 2 && h2 == 1 && h3 == 2 && h4 == 1)
            {
                return "Acquisitio";
            }
            else if (h1 == 2 && h2 == 2 && h3 == 1 && h4 == 2)
            {
                return "Albus";
            }
            else if (h1 == 1 && h2 == 2 && h3 == 1 && h4 == 2)
            {
                return "Amissio";
            }
            else
            {
                return "Rubeus";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FigureWiki_Load(object sender, EventArgs e)
        {

        }
    }
}
