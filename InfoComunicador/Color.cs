using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoComunicador
{
    public partial class Color : Form
    {
        string apiCallLocal;
        public Color(string apiCall)
        {
            InitializeComponent();

            apiCallLocal = apiCall;

            inicializarCuadroMuestra();
        }

        private void Color_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void Color_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu pantalla = new()
            {
                Size = this.Size,
                StartPosition = FormStartPosition.Manual,
                Location = this.Location
            };
            pantalla.Show();
            this.Hide();
        }

        [GeneratedRegex("rgb\\((\\d+),(\\d+),(\\d+)\\)")]
        private static partial Regex RegexRGB();

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void inicializarCuadroMuestra()
        {
            int r, g, b;
            string cadenaRGB;

            cadenaRGB = ComunicacionAPI.Get(apiCallLocal + "color/", "Color");
            Match match = RegexRGB().Match(cadenaRGB);
            if (match.Success)
            {
                r = int.Parse(match.Groups[1].Value);
                g = int.Parse(match.Groups[2].Value);
                b = int.Parse(match.Groups[3].Value);
            }
            else
            {
                r = g = b = 0;
            }

            pictureBox1.BackColor = System.Drawing.Color.FromArgb(r, g, b);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (sender is not PictureBox llamador)
                throw new InvalidCastException("El objeto llamador no pudo ser interpretado como PictureBox");

            string respuestaJson = ComunicacionAPI.PutColor(apiCallLocal + "color/actualizar", "rgb(" + llamador.BackColor.R +"," + llamador.BackColor.G + "," + llamador.BackColor.B + ")");
            MessageBox.Show(ProcesarJson.GetCadena(respuestaJson, "Estado"), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            inicializarCuadroMuestra();
        }
    }
}
