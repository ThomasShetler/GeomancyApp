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
                label1.Tag = "2";
            }
            else
            {
                label1.Text = "  ◆";
                label1.Tag = "1";
            }
            UpdateFigureDisplay();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (label3.Text == "  ◆")
            {
                label3.Text = "◆ ◆";
                label3.Tag = "2";
            }
            else
            {
                label3.Text = "  ◆";
                label3.Tag = "1";
            }
            UpdateFigureDisplay();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (label4.Text == "  ◆")
            {
                label4.Text = "◆ ◆";
                label4.Tag = "2";
            }
            else
            {
                label4.Text = "  ◆";
                label4.Tag = "1";
            }
            UpdateFigureDisplay();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (label5.Text == "  ◆")
            {
                label5.Text = "◆ ◆";
                label5.Tag = "2";
            }
            else
            {
                label5.Text = "  ◆";
                label5.Tag = "1";
            }
            UpdateFigureDisplay();
        }

        private void UpdateFigureDisplay()
        {
            try
            {
                // Get the current elemental pattern from the labels, handling string Tags
                int fireValue = Convert.ToInt32(label1.Tag?.ToString() ?? "2");
                int airValue = Convert.ToInt32(label3.Tag?.ToString() ?? "2");
                int waterValue = Convert.ToInt32(label4.Tag?.ToString() ?? "2");
                int earthValue = Convert.ToInt32(label5.Tag?.ToString() ?? "2");

                // Use the new method to get figure data by elemental pattern
                FigureData fig = FigureData.GetFigureByElementalPattern(fireValue, airValue, waterValue, earthValue);
                
                if (fig != null)
                {
                    UpdateText(fig);
                }
                else
                {
                    // Fallback to legacy method if needed
                    string figureName = GetFigureNameByPattern(fireValue, airValue, waterValue, earthValue);
                    fig = FigureData.GetFigureByName(figureName);
                    if (fig != null)
                    {
                        UpdateText(fig);
                    }
                    else
                    {
                        // If still no figure found, clear the display
                        ClearDisplay();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating figure display: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearDisplay()
        {
            FigName.Text = "No Figure Found";
            OtherNames.Text = "";
            InnerElement.Text = "";
            OutterElement.Text = "";
            Quality.Text = "";
            Planet.Text = "";
            Sign.Text = "";
            Keyword.Text = "";
            DivinaryMeaning.Text = "";
            Imagery.Text = "";
            StrongHouse.Text = "";
            WeakHouse.Text = "";
            
            if (FireElement != null) FireElement.Text = "";
            if (AirElement != null) AirElement.Text = "";
            if (WaterElement != null) WaterElement.Text = "";
            if (EarthElement != null) EarthElement.Text = "";
            
            if (Anatomy != null) Anatomy.Text = "";
            if (BodyType != null) BodyType.Text = "";
            if (CharacterType != null) CharacterType.Text = "";
            if (Colors != null) Colors.Text = "";
            if (Commentary != null) Commentary.Text = "";
        }

        public void UpdateText(FigureData fig)
        {
            if (fig == null) 
            {
                ClearDisplay();
                return;
            }

            try
            {
                FigName.Text = fig.Name ?? "";
                OtherNames.Text = fig.OtherNames ?? "";
                InnerElement.Text = fig.InnerEl ?? "";
                OutterElement.Text = fig.OuterEl ?? "";
                Quality.Text = fig.Quality ?? "";
                Planet.Text = fig.Planet ?? "";
                Sign.Text = fig.Sign ?? "";
                Keyword.Text = fig.Keyword ?? "";
                DivinaryMeaning.Text = fig.DivinatoryMeaning ?? "";
                Imagery.Text = fig.Imagery ?? "";
                StrongHouse.Text = fig.StrongHouse ?? "";
                WeakHouse.Text = fig.WeakHouse ?? "";
                
                if (FireElement != null) FireElement.Text = fig.FireElement ?? "";
                if (AirElement != null) AirElement.Text = fig.AirElement ?? "";
                if (WaterElement != null) WaterElement.Text = fig.WaterElement ?? "";
                if (EarthElement != null) EarthElement.Text = fig.EarthElement ?? "";
                
                if (Anatomy != null) Anatomy.Text = fig.Anatomy ?? "";
                if (BodyType != null) BodyType.Text = fig.BodyType ?? "";
                if (CharacterType != null) CharacterType.Text = fig.CharacterType ?? "";
                if (Colors != null) Colors.Text = fig.Colors ?? "";
                if (Commentary != null) Commentary.Text = fig.Commentary ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating text: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Legacy method for backward compatibility
        public string GetFigureNameByPattern(int h1, int h2, int h3, int h4)
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
                return "Tristitia";
                return "Tristitia";
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
            // Initialize the form with default values
            try
            {
                // Set initial Tag values as strings
                label1.Tag = "2";
                label3.Tag = "2";
                label4.Tag = "2";
                label5.Tag = "2";
                
                // Set initial text
                label1.Text = "◆ ◆";
                label3.Text = "◆ ◆";
                label4.Text = "◆ ◆";
                label5.Text = "◆ ◆";
                
                // Update the display with the initial pattern (2-2-2-2 = Populus)
                UpdateFigureDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handlers for the new labels (color-coded elemental labels)
        private void label17_Click(object sender, EventArgs e)
        {
            // Fire Element label clicked - could be used for additional functionality
        }

        private void label18_Click(object sender, EventArgs e)
        {
            // Air Element label clicked - could be used for additional functionality
        }

        private void label19_Click(object sender, EventArgs e)
        {
            // Water Element label clicked - could be used for additional functionality
        }

        private void label20_Click(object sender, EventArgs e)
        {
            // Earth Element label clicked - could be used for additional functionality
        }

        // Event handlers for text boxes (if needed for validation or real-time updates)
        private void Quality_TextChanged(object sender, EventArgs e)
        {
            // Quality text changed - could be used for validation
        }

        private void Keyword_TextChanged(object sender, EventArgs e)
        {
            // Keyword text changed - could be used for validation
        }

        private void Planet_TextChanged(object sender, EventArgs e)
        {
            // Planet text changed - could be used for validation
        }

        private void Sign_TextChanged(object sender, EventArgs e)
        {
            // Sign text changed - could be used for validation
        }

        private void StrongHouse_TextChanged(object sender, EventArgs e)
        {
            // Strong House text changed - could be used for validation
        }

        private void WeakHouse_TextChanged(object sender, EventArgs e)
        {
            // Weak House text changed - could be used for validation
        }

        private void InnerElement_TextChanged(object sender, EventArgs e)
        {
            // Inner Element text changed - could be used for validation
        }

        private void FireElement_TextChanged(object sender, EventArgs e)
        {
            // Fire Element text changed - could be used for validation
        }

        private void AirElement_TextChanged(object sender, EventArgs e)
        {
            // Air Element text changed - could be used for validation
        }

        private void WaterElement_TextChanged(object sender, EventArgs e)
        {
            // Water Element text changed - could be used for validation
        }

        private void EarthElement_TextChanged(object sender, EventArgs e)
        {
            // Earth Element text changed - could be used for validation
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Panel paint event - could be used for custom drawing
        }
    }
}
