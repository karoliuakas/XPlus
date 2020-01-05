using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace AutoGidas
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text)
                && (textBox1.Text.StartsWith("https://autogidas.lt/skelbimas/") || textBox1.Text.StartsWith("https://www.autogidas.lt/skelbimas/"))
                && textBox1.Text.EndsWith(".html"))
            {
                //string dir = Environment.CurrentDirectory;
                //dir = Directory.GetParent(dir).Parent.FullName;

                //string fileName = String.Format(@"url", Application.StartupPath);
                //File.WriteAllText(fileName, textBox1.Text);

                CarInfo carma = new CarInfo();
                carma.run_cmd(textBox1.Text);
                label1.Text = carma.left;
                label2.Text = carma.right;
                label3.Text = "Vidutinė šios markės automobilių kaina: "+carma.averagePrice+"€";
                label4.Text = "Šio automobilio kaina: "+carma.price +"€";
                label5.Text = carma.average;
                label6.Text = "Peržiūrėta transporto priemonių:"+ carma.amount;
                if (carma.coefficient < 0.9&& carma.coefficient >0)
                {
                    label7.Text = "Patariame nepirkti("+carma.coefficient.ToString()+")";
                    label7.ForeColor = Color.Red;
                }
                else if (carma.coefficient > 0.9 && carma.coefficient < 1.1)
                {
                    label7.Text = "Pirk arba nepirk(" + carma.coefficient.ToString() + ")";
                    label7.ForeColor = Color.GreenYellow;
                }
                else if (carma.coefficient > 1.1)
                {
                    label7.Text = "Patariame pirkti tuojau pat(" + carma.coefficient.ToString() + ")";
                    label7.ForeColor = Color.Green;
                }
                else
                {
                    label7.Text = "Negalime patarti";
                    label7.ForeColor = Color.Blue;
                }
                pictureBox1.ImageLocation = carma.picURL;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
              
                

            }
            else
            {
                MessageBox.Show("Tinkamas adreso formatas: \n" +
                                "https://autogidas.lt/skelbimas/ <-> .html");
            }
        }

    }
}