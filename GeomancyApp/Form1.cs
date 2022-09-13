﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeomancyApp
{
   

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        HouseChart houseChart = new HouseChart();
       
        figure House1 = new figure();
        private void label1_Click(object sender, EventArgs e)
        {
            
            if(label1.Text == "  ◆")
            {
                label1.Text = "◆ ◆";
                label1.Tag = 2;
                House1.headLine = 2;
            }
            else
            {
                label1.Text = "  ◆";
                label1.Tag = 1;
                House1.headLine = 1;
            }
            
        }


        
        private void label5_Click(object sender, EventArgs e)
        {
            {
            
                if (label5.Text == "  ◆")
                {
                    label5.Text = "◆ ◆";
                    label5.Tag = 2;
                    House1.footLine = 2;
                }
                else
                {
                    label5.Text = "  ◆";
                    label5.Tag = 1;
                    House1.footLine = 1;
                }
             
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            {
                

                if (label3.Text == "  ◆")
                {
                    label3.Text = "◆ ◆";
                    label3.Tag = 2;
                    House1.neckLine = 2;
                }
                else
                {
                    label3.Text = "  ◆";
                    label3.Tag = 1;
                    House1.neckLine = 1;
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            {

                if (label4.Text == "  ◆")
                {
                    label4.Text = "◆ ◆";
                    label4.Tag = 2;
                    House1.bodyLine = 2;
                }
                else
                {
                    label4.Text = "  ◆";
                    label4.Tag = 1;
                    House1.bodyLine = 1;
                }
            }

        }
        
        private void firstHouse(int l1, int l2, int l3, int l4)
        {
            House1.headLine = l1;
            House1.neckLine = l2;
            House1.bodyLine = l3;
            House1.footLine = l4;
            House1.HouseNumber = 1;
            House1.figureName = FigureName(l1, l2, l3, l4);
           
        }
        figure House2 = new figure();
        private void SecondHouse(int l1, int l2, int l3, int l4)
        {
            House2.headLine = l1;
            House2.neckLine = l2;
            House2.bodyLine = l3;
            House2.footLine = l4;
            House2.HouseNumber = 2;
            House2.figureName = FigureName(l1, l2, l3, l4);

        }
        figure House3 = new figure();
        private void ThirdHouse(int l1, int l2, int l3, int l4)
        {
            House3.headLine = l1;
            House3.neckLine = l2;
            House3.bodyLine = l3;
            House3.footLine = l4;
            House3.HouseNumber = 3;
            House3.figureName = FigureName(l1, l2, l3, l4);

        }
        figure House4 = new figure();
        private void ForthHouse(int l1, int l2, int l3, int l4)
        {
            House4.headLine = l1;
            House4.neckLine = l2;
            House4.bodyLine = l3;
            House4.footLine = l4;
            House4.HouseNumber = 4;
            House4.figureName = FigureName(l1, l2, l3, l4);

        }

        private void label10_Click(object sender, EventArgs e)
        {
            {

                if (label10.Text == "  ◆")
                {
                    label10.Text = "◆ ◆";
                    label10.Tag = 2;
                }
                else
                {
                    label10.Text = "  ◆";
                    label10.Tag = 1;
                }
            }

        }

        private void label8_Click(object sender, EventArgs e)
        {
            {

                if (label8.Text == "  ◆")
                { 
                    label8.Text = "◆ ◆";
                    label8.Tag = 2;
                }
                else
                {
                    label8.Text = "  ◆";
                    label8.Tag = 1;
                }
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {
            {

                if (label7.Text == "  ◆")
                {
                    label7.Text = "◆ ◆";
                    label7.Tag = 2;
                }
                else
                {
                    label7.Text = "  ◆";
                    label7.Tag = 1;
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            {

                if (label6.Text == "  ◆")
                {
                    label6.Text = "◆ ◆";
                    label6.Tag = 2;
                }
                else
                {
                    label6.Text = "  ◆";
                    label6.Tag = 1;
                }
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            {

                if (label15.Text == "  ◆")
                {
                    label15.Text = "◆ ◆";
                    label15.Tag = 2;
                }
                else
                {
                    label15.Text = "  ◆";
                    label15.Tag = 1;
                }
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            {

                if (label13.Text == "  ◆")
                {
                    label13.Text = "◆ ◆";
                    label13.Tag = 2;
                }
                else
                {
                    label13.Text = "  ◆";
                    label13.Tag = 1;
                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            {

                if (label12.Text == "  ◆")
                {
                    label12.Text = "◆ ◆";
                    label12.Tag = 2;
                }
                else
                {
                    label12.Text = "  ◆";
                    label12.Tag = 1;
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            {

                if (label11.Text == "  ◆")
                {
                    label11.Text = "◆ ◆";
                    label11.Tag = 2;
                }
                else
                {
                    label11.Text = "  ◆";
                    label11.Tag = 1;
                }
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            {

                if (label20.Text == "  ◆")
                {
                    label20.Text = "◆ ◆";
                    label20.Tag = 2;
                }
                else
                {
                    label20.Text = "  ◆";
                    label20.Tag = 1;
                }
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            {

                if (label18.Text == "  ◆")
                {
                    label18.Text = "◆ ◆";
                    label18.Tag = 2;
                }
                else
                {
                    label18.Text = "  ◆";
                    label18.Tag = 1;
                }
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            {

                if (label17.Text == "  ◆")
                {
                    label17.Text = "◆ ◆";
                    label17.Tag = 2;
                }
                else
                {
                    label17.Text = "  ◆";
                    label17.Tag = 1;
                }
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            {

                if (label16.Text == "  ◆")
                {
                    label16.Text = "◆ ◆";
                    label16.Tag = 2;
                }
                else
                {
                    label16.Text = "  ◆";
                    label16.Tag = 1;
                }
               
            }
        }
        figure House5 = new figure();
        public void FifthHouseCal(int h1, int h2, int h3, int h4)
        {
            House5.headLine = h1;
            House5.neckLine = h2;
            House5.bodyLine = h3;
            House5.footLine = h4;
            House5.HouseNumber = 5;
            House5.figureName = FigureName(h1, h2, h3, h4);

            if (h1 == 1)
            {
                label40.Text = "  ◆";
                label40.Tag = 1;
            }
            else
            {
                label40.Text = "◆ ◆";
                label40.Tag = 2;
                
            }
            if (h2 == 1)
            {
                label38.Text = "  ◆";
                label38.Tag = 1;
            }
            else
            {
                label38.Text = "◆ ◆";
                label38.Tag = 2;
            }
            if (h3 == 1)
            {
                label37.Text = "  ◆";
                label37.Tag = 1;

            }
            else
            {
                label37.Text = "◆ ◆";
                label37.Tag = 2;
            }
            if (h4 == 1)
            {
                label36.Text = "  ◆";
                label36.Tag = 1;

            }
            else
            {
                label36.Text = "◆ ◆";
                label36.Tag = 2;
            }
        }
        figure House6 = new figure();
        public void SixthHouseCal(int h1, int h2, int h3, int h4)
        {
            House6.headLine = h1;
            House6.neckLine = h2;
            House6.bodyLine = h3;
            House6.footLine = h4;
            House6.HouseNumber = 6;
            House6.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label35.Text = "  ◆";
                label35.Tag = 1;
            }
            else
            {
                label35.Text = "◆ ◆";
                label35.Tag = 2;

            }
            if (h2 == 1)
            {
                label33.Text = "  ◆";
                label33.Tag = 1;
            }
            else
            {
                label33.Text = "◆ ◆";
                label33.Tag = 2;
            }
            if (h3 == 1)
            {
                label32.Text = "  ◆";
                label32.Tag = 1;

            }
            else
            {
                label32.Text = "◆ ◆";
                label32.Tag = 2;
            }
            if (h4 == 1)
            {
                label31.Text = "  ◆";
                label31.Tag = 1;

            }
            else
            {
                label31.Text = "◆ ◆";
                label31.Tag = 2;
            }
        }
        figure House7 = new figure();
        public void SeventhHouseCal(int h1, int h2, int h3, int h4)
        {
            House7.headLine = h1;
            House7.neckLine = h2;
            House7.bodyLine = h3;
            House7.footLine = h4;
            House7.HouseNumber = 7;
            House7.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label30.Text = "  ◆";
                label30.Tag = 1;
            }
            else
            {
                label30.Text = "◆ ◆";
                label30.Tag = 2;

            }
            if (h2 == 1)
            {
                label28.Text = "  ◆";
                label28.Tag = 1;
            }
            else
            {
                label28.Text = "◆ ◆";
                label28.Tag = 2;
            }
            if (h3 == 1)
            {
                label27.Text = "  ◆";
                label27.Tag = 1;

            }
            else
            {
                label27.Text = "◆ ◆";
                label27.Tag = 2;
            }
            if (h4 == 1)
            {
                label26.Text = "  ◆";
                label26.Tag = 1;

            }
            else
            {
                label26.Text = "◆ ◆";
                label26.Tag = 2;
            }
        }
        figure House8 = new figure();
        public void EighthHouseCal(int h1, int h2, int h3, int h4)
        {
            House8.headLine = h1;
            House8.neckLine = h2;
            House8.bodyLine = h3;
            House8.footLine = h4;
            House8.HouseNumber = 8;
            House8.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label25.Text = "  ◆";
                label25.Tag = 1;
            }
            else
            {
                label25.Text = "◆ ◆";
                label25.Tag = 2;

            }
            if (h2 == 1)
            {
                label23.Text = "  ◆";
                label23.Tag = 1;
            }
            else
            {
                label23.Text = "◆ ◆";
                label23.Tag = 2;
            }
            if (h3 == 1)
            {
                label22.Text = "  ◆";
                label22.Tag = 1;

            }
            else
            {
                label22.Text = "◆ ◆";
                label22.Tag = 2;
            }
            if (h4 == 1)
            {
                label21.Text = "  ◆";
                label21.Tag = 1;

            }
            else
            {
                label21.Text = "◆ ◆";
                label21.Tag = 2;
            }
        }
        figure House9 = new figure();
        public void NinthHouseCal(int h1, int h2, int h3, int h4)
        {
            House9.headLine = h1;
            House9.neckLine = h2;
            House9.bodyLine = h3;
            House9.footLine = h4;
            House9.HouseNumber = 9;
            House9.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label50.Text = "  ◆";
                label50.Tag = 1;
            }
            else
            {
                label50.Text = "◆ ◆";
                label50.Tag = 2;

            }
            if (h2 == 1)
            {
                label48.Text = "  ◆";
                label48.Tag = 1;
            }
            else
            {
                label48.Text = "◆ ◆";
                label48.Tag = 2;
            }
            if (h3 == 1)
            {
                label47.Text = "  ◆";
                label47.Tag = 1;

            }
            else
            {
                label47.Text = "◆ ◆";
                label47.Tag = 2;
            }
            if (h4 == 1)
            {
                label46.Text = "  ◆";
                label46.Tag = 1;

            }
            else
            {
                label46.Text = "◆ ◆";
                label46.Tag = 2;
            }
        }
        figure House10 = new figure(); 
        public void TenthHouseCal(int h1, int h2, int h3, int h4)
        {
            House10.headLine = h1;
            House10.neckLine = h2;
            House10.bodyLine = h3;
            House10.footLine = h4;
            House10.HouseNumber = 10;
            House10.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label65.Text = "  ◆";
                label65.Tag = 1;
            }
            else
            {
                label65.Text = "◆ ◆";
                label65.Tag = 2;

            }
            if (h2 == 1)
            {
                label63.Text = "  ◆";
                label63.Tag = 1;
            }
            else
            {
                label63.Text = "◆ ◆";
                label63.Tag = 2;
            }
            if (h3 == 1)
            {
                label62.Text = "  ◆";
                label62.Tag = 1;

            }
            else
            {
                label62.Text = "◆ ◆";
                label62.Tag = 2;
            }
            if (h4 == 1)
            {
                label61.Text = "  ◆";
                label61.Tag = 1;

            }
            else
            {
                label61.Text = "◆ ◆";
                label61.Tag = 2;
            }
        }
        figure House11 = new figure();
        public void eleventhHouseCal(int h1, int h2, int h3, int h4)

        {
            House11.headLine = h1;
            House11.neckLine = h2;
            House11.bodyLine = h3;
            House11.footLine = h4;
            House11.HouseNumber = 11;
            House11.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label55.Text = "  ◆";
                label55.Tag = 1;
            }
            else
            {
                label55.Text = "◆ ◆";
                label55.Tag = 2;

            }
            if (h2 == 1)
            {
                label53.Text = "  ◆";
                label53.Tag = 1;
            }
            else
            {
                label53.Text = "◆ ◆";
                label53.Tag = 2;
            }
            if (h3 == 1)
            {
                label52.Text = "  ◆";
                label52.Tag = 1;

            }
            else
            {
                label52.Text = "◆ ◆";
                label52.Tag = 2;
            }
            if (h4 == 1)
            {
                label51.Text = "  ◆";
                label51.Tag = 1;

            }
            else
            {
                label51.Text = "◆ ◆";
                label51.Tag = 2;
            }
        }
        figure House12 = new figure();
        public void twelvthHouseCal(int h1, int h2, int h3, int h4)
        {
            House12.headLine = h1;
            House12.neckLine = h2;
            House12.bodyLine = h3;
            House12.footLine = h4;
            House12.HouseNumber = 12;
            House12.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label45.Text = "  ◆";
                label45.Tag = 1;
            }
            else
            {
                label45.Text = "◆ ◆";
                label45.Tag = 2;

            }
            if (h2 == 1)
            {
                label43.Text = "  ◆";
                label43.Tag = 1;
            }
            else
            {
                label43.Text = "◆ ◆";
                label43.Tag = 2;
            }
            if (h3 == 1)
            {
                label42.Text = "  ◆";
                label42.Tag = 1;

            }
            else
            {
                label42.Text = "◆ ◆";
                label42.Tag = 2;
            }
            if (h4 == 1)
            {
                label41.Text = "  ◆";
                label41.Tag = 1;

            }
            else
            {
                label41.Text = "◆ ◆";
                label41.Tag = 2;
            }
        }
        figure RightWit = new figure();
        public void RightWitCal(int h1, int h2, int h3, int h4)
        {
            RightWit.headLine = h1;
            RightWit.neckLine = h2;
            RightWit.bodyLine = h3;
            RightWit.footLine = h4;
            RightWit.HouseNumber = 13;
            RightWit.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label60.Text = "  ◆";
                label60.Tag = 1;
            }
            else
            {
                label60.Text = "◆ ◆";
                label60.Tag = 2;

            }
            if (h2 == 1)
            {
                label58.Text = "  ◆";
                label58.Tag = 1;
            }
            else
            {
                label58.Text = "◆ ◆";
                label58.Tag = 2;
            }
            if (h3 == 1)
            {
                label57.Text = "  ◆";
                label57.Tag = 1;

            }
            else
            {
                label57.Text = "◆ ◆";
                label57.Tag = 2;
            }
            if (h4 == 1)
            {
                label56.Text = "  ◆";
                label56.Tag = 1;

            }
            else
            {
                label56.Text = "◆ ◆";
                label56.Tag = 2;
            }
        }
        figure LeftWit = new figure();
        public void LeftWitCal(int h1, int h2, int h3, int h4)
        {
            LeftWit.headLine = h1;
            LeftWit.neckLine = h2;
            LeftWit.bodyLine = h3;
            LeftWit.footLine = h4;
            LeftWit.HouseNumber = 14;
            LeftWit.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
            {
                label70.Text = "  ◆";
                label70.Tag = 1;
            }
            else
            {
                label70.Text = "◆ ◆";
                label70.Tag = 2;

            }
            if (h2 == 1)
            {
                label68.Text = "  ◆";
                label68.Tag = 1;
            }
            else
            {
                label68.Text = "◆ ◆";
                label68.Tag = 2;
            }
            if (h3 == 1)
            {
                label67.Text = "  ◆";
                label67.Tag = 1;

            }
            else
            {
                label67.Text = "◆ ◆";
                label67.Tag = 2;
            }
            if (h4 == 1)
            {
                label66.Text = "  ◆";
                label66.Tag = 1;

            }
            else
            {
                label66.Text = "◆ ◆";
                label66.Tag = 2;
            }
        }
        figure judge = new figure();
        public void JudgeCal(int h1, int h2, int h3, int h4)
        {
            judge.headLine = h1;
            judge.neckLine = h2;
            judge.bodyLine = h3;
            judge.footLine = h4;
            judge.HouseNumber = 15;
            judge.figureName = FigureName(h1, h2, h3, h4);
            if (h1 == 1)
                if (h1 == 1)
            {
                label75.Text = "  ◆";
                label75.Tag = 1;
            }
            else
            {
                label75.Text = "◆ ◆";
                label75.Tag = 2;

            }
            if (h2 == 1)
            {
                label73.Text = "  ◆";
                label73.Tag = 1;
            }
            else
            {
                label73.Text = "◆ ◆";
                label73.Tag = 2;
            }
            if (h3 == 1)
            {
                label72.Text = "  ◆";
                label72.Tag = 1;

            }
            else
            {
                label72.Text = "◆ ◆";
                label72.Tag = 2;
            }
            if (h4 == 1)
            {
                label71.Text = "  ◆";
                label71.Tag = 1;

            }
            else
            {
                label71.Text = "◆ ◆";
                label71.Tag = 2;
            }
        }
        figure fallout = new figure();
        public void FallOutCal(int h1, int h2, int h3, int h4)
        {
            fallout.headLine = h1;
            fallout.neckLine = h2;
            fallout.bodyLine = h3;
            fallout.footLine = h4;
            fallout.figureName = FigureName(h1, h2, h3, h4);
            fallout.HouseNumber = 16;

            if (h1 == 1)
            {
                label80.Text = "  ◆";
                label80.Tag = 1;
            }
            else
            {
                label80.Text = "◆ ◆";
                label80.Tag = 2;

            }
            if (h2 == 1)
            {
                label78.Text = "  ◆";
                label78.Tag = 1;
            }
            else
            {
                label78.Text = "◆ ◆";
                label78.Tag = 2;
            }
            if (h3 == 1)
            {
                label77.Text = "  ◆";
                label77.Tag = 1;

            }
            else
            {
                label77.Text = "◆ ◆";
                label77.Tag = 2;
            }
            if (h4 == 1)
            {
                label76.Text = "  ◆";
                label76.Tag = 1;

            }
            else
            {
                label76.Text = "◆ ◆";
                label76.Tag = 2;
            }
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
        public string SharingString (HouseChart houses)
        {
           List<figure> figureList = new List<figure>();
            string returnString = "";
            figureList.Add(houses.FirstHouse);
            figureList.Add(houses.SecondHouse);
            figureList.Add(houses.ThirdHouse);
            figureList.Add(houses.ForthHouse);
            figureList.Add(houses.FifthHouse);
            figureList.Add(houses.SixthHouse);
            figureList.Add(houses.SeventhHouse);
            figureList.Add(houses.EightHouse);
            figureList.Add(houses.NinthHouse);
            figureList.Add(houses.TenthHouse);
            figureList.Add(houses.eleventhHouse);
            figureList.Add(houses.twelvethHouse);
            figureList.Add(houses.RightWitness);
            figureList.Add(houses.LeftWittness);
            figureList.Add(houses.Judge);
            figureList.Add(houses.fallout);
            foreach(figure item in figureList)
            {
                returnString += "-";
               returnString += item.headLine.ToString();
                returnString += item.neckLine.ToString();
                returnString += item.bodyLine.ToString();
                returnString += item.footLine.ToString();

            }
            return returnString;

        }
        public HouseChart HouseChartFromString (string input)
        {
            HouseChart houseChart = new HouseChart();

            string[] ListOfStrings = input.Split('-');
            int indexer = 1;
            foreach( var item in ListOfStrings)
            {
                char a = item[0];
                char b = item[1];
                char c = item[2];
                char d = item[3];
                 figure fig = new figure();
                fig.headLine = Convert.ToInt32(a.ToString());
                fig.neckLine = Convert.ToInt32(b.ToString());
                fig.bodyLine = Convert.ToInt32(c.ToString());
                fig.footLine = Convert.ToInt32(d.ToString());
                fig.figureName = FigureName(Convert.ToInt32(a.ToString()), Convert.ToInt32(b.ToString()), Convert.ToInt32(c.ToString()), Convert.ToInt32(d.ToString()));
                fig.HouseNumber = indexer;
                if (indexer == 1)
                {
                    houseChart.FirstHouse = fig;
                }
                else if (indexer == 2)
                {
                    houseChart.SecondHouse = fig;
                }
                else if (indexer == 3)
                {
                    houseChart.ThirdHouse = fig;
                }
                else if (indexer == 4)
                {
                    houseChart.ForthHouse = fig;
                }
                else if (indexer == 5)
                {
                    houseChart.FifthHouse = fig;
                }
                else if (indexer == 6)
                {
                    houseChart.SixthHouse = fig;
                }
                else if (indexer == 7)
                {
                    houseChart.SeventhHouse = fig;
                }
                else if (indexer == 8)
                {
                    houseChart.EightHouse = fig;
                }
                else if (indexer == 9)
                {
                    houseChart.NinthHouse = fig;
                }
                else if (indexer == 10)
                {
                    houseChart.TenthHouse = fig;
                }
                else if (indexer == 11)
                {
                    houseChart.eleventhHouse = fig;

                }
                else if (indexer == 12)
                {
                    houseChart.twelvethHouse = fig;

                }
                else if (indexer == 13)
                {
                    houseChart.RightWitness = fig;

                }
                else if (indexer == 14)
                {
                    houseChart.LeftWittness = fig;

                }
                else if (indexer == 15)
                {
                    houseChart.Judge = fig;

                }
                else
                {
                    houseChart.fallout = fig;
                }
                indexer++;
            }

            return houseChart;
        }
        public int CalNieces(int h1, int h2)
        {
            if ((h1 + h2) % 2 == 0)
            {
                return 2;
}
            else
            {
                return 1;
           }
        }
        private void button1_Click(object sender, EventArgs e)
            {
            
                firstHouse(Convert.ToInt32(label1.Tag), Convert.ToInt32(label3.Tag), Convert.ToInt32(label4.Tag), Convert.ToInt32(label5.Tag));
                SecondHouse(Convert.ToInt32(label10.Tag), Convert.ToInt32(label8.Tag), Convert.ToInt32(label7.Tag), Convert.ToInt32(label6.Tag));
                ThirdHouse(Convert.ToInt32(label15.Tag), Convert.ToInt32(label13.Tag), Convert.ToInt32(label12.Tag), Convert.ToInt32(label11.Tag));
                ForthHouse(Convert.ToInt32(label20.Tag), Convert.ToInt32(label18.Tag), Convert.ToInt32(label17.Tag), Convert.ToInt32(label16.Tag));
                FifthHouseCal(Convert.ToInt32(label1.Tag), Convert.ToInt32(label10.Tag), Convert.ToInt32(label15.Tag), Convert.ToInt32(label20.Tag));
                SixthHouseCal(Convert.ToInt32(label3.Tag), Convert.ToInt32(label8.Tag), Convert.ToInt32(label13.Tag), Convert.ToInt32(label18.Tag));
                SeventhHouseCal(Convert.ToInt32(label4.Tag), Convert.ToInt32(label7.Tag), Convert.ToInt32(label12.Tag), Convert.ToInt32(label17.Tag));
                EighthHouseCal(Convert.ToInt32(label5.Tag), Convert.ToInt32(label6.Tag), Convert.ToInt32(label11.Tag), Convert.ToInt32(label16.Tag));
                NinthHouseCal(CalNieces(Convert.ToInt32(label1.Tag), Convert.ToInt32(label10.Tag)), CalNieces(Convert.ToInt32(label3.Tag), Convert.ToInt32(label8.Tag)), CalNieces(Convert.ToInt32(label4.Tag), Convert.ToInt32(label7.Tag)), CalNieces(Convert.ToInt32(label5.Tag), Convert.ToInt32(label6.Tag)));
                TenthHouseCal(CalNieces(Convert.ToInt32(label15.Tag), Convert.ToInt32(label20.Tag)), CalNieces(Convert.ToInt32(label13.Tag), Convert.ToInt32(label18.Tag)), CalNieces(Convert.ToInt32(label12.Tag), Convert.ToInt32(label17.Tag)), CalNieces(Convert.ToInt32(label11.Tag), Convert.ToInt32(label16.Tag)));
                eleventhHouseCal(CalNieces(Convert.ToInt32(label40.Tag), Convert.ToInt32(label35.Tag)), CalNieces(Convert.ToInt32(label38.Tag), Convert.ToInt32(label33.Tag)), CalNieces(Convert.ToInt32(label37.Tag), Convert.ToInt32(label32.Tag)), CalNieces(Convert.ToInt32(label36.Tag), Convert.ToInt32(label31.Tag)));
                twelvthHouseCal(CalNieces(Convert.ToInt32(label30.Tag), Convert.ToInt32(label25.Tag)), CalNieces(Convert.ToInt32(label28.Tag), Convert.ToInt32(label23.Tag)), CalNieces(Convert.ToInt32(label27.Tag), Convert.ToInt32(label22.Tag)), CalNieces(Convert.ToInt32(label26.Tag), Convert.ToInt32(label21.Tag)));
                RightWitCal(CalNieces(Convert.ToInt32(label50.Tag), Convert.ToInt32(label65.Tag)), CalNieces(Convert.ToInt32(label48.Tag), Convert.ToInt32(label63.Tag)), CalNieces(Convert.ToInt32(label47.Tag), Convert.ToInt32(label62.Tag)), CalNieces(Convert.ToInt32(label46.Tag), Convert.ToInt32(label61.Tag)));
                LeftWitCal(CalNieces(Convert.ToInt32(label55.Tag), Convert.ToInt32(label45.Tag)), CalNieces(Convert.ToInt32(label53.Tag), Convert.ToInt32(label43.Tag)), CalNieces(Convert.ToInt32(label52.Tag), Convert.ToInt32(label42.Tag)), CalNieces(Convert.ToInt32(label51.Tag), Convert.ToInt32(label41.Tag)));
                JudgeCal(CalNieces(Convert.ToInt32(label60.Tag), Convert.ToInt32(label70.Tag)), CalNieces(Convert.ToInt32(label58.Tag), Convert.ToInt32(label68.Tag)), CalNieces(Convert.ToInt32(label57.Tag), Convert.ToInt32(label67.Tag)), CalNieces(Convert.ToInt32(label66.Tag), Convert.ToInt32(label56.Tag)));
                FallOutCal(CalNieces(Convert.ToInt32(label75.Tag), Convert.ToInt32(label1.Tag)), CalNieces(Convert.ToInt32(label3.Tag), Convert.ToInt32(label73.Tag)), CalNieces(Convert.ToInt32(label4.Tag), Convert.ToInt32(label72.Tag)), CalNieces(Convert.ToInt32(label5.Tag), Convert.ToInt32(label71.Tag)));

            HouseChart houseChart = new HouseChart();
            houseChart.FirstHouse = House1;
            houseChart.SecondHouse = House2;
            houseChart.ThirdHouse = House3;
            houseChart.ForthHouse = House4;
            houseChart.FifthHouse = House5;
            houseChart.SixthHouse = House6;
            houseChart.SeventhHouse = House7;
            houseChart.EightHouse = House8;
            houseChart.NinthHouse = House9;
            houseChart.TenthHouse = House10;
            houseChart.eleventhHouse = House11;
            houseChart.twelvethHouse = House12;
            houseChart.RightWitness = RightWit;
            houseChart.LeftWittness = LeftWit;
            houseChart.Judge = judge;
            houseChart.fallout = fallout;

            textBox1.Text = houseChart.FirstHouse.figureName;
            textBox2.Text = houseChart.SecondHouse.figureName;
            textBox3.Text = houseChart.ThirdHouse.figureName;
            textBox4.Text = houseChart.ForthHouse.figureName;
            textBox5.Text = houseChart.FifthHouse.figureName;
            textBox6.Text = houseChart.SixthHouse.figureName;
            textBox7.Text = houseChart.SeventhHouse.figureName;
            textBox8.Text = houseChart.EightHouse.figureName;
            textBox9.Text = houseChart.NinthHouse.figureName;
            textBox10.Text = houseChart.TenthHouse.figureName;
            textBox11.Text = houseChart.eleventhHouse.figureName;
            textBox12.Text = houseChart.twelvethHouse.figureName;
            textBox13.Text = SharingString(houseChart);


        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            HouseChartFromString(textBox15.Text);


        }
    }
}
