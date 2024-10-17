using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoComunicador
{
    public partial class Menu : Form
    {
        private int originalFormWidth;
        private int originalFormHeight;

        private int originalButton1Width;
        private int originalButton1Height;
        private int originalButton1X;
        private int originalButton1Y;

        private int originalButton2Width;
        private int originalButton2Height;
        private int originalButton2X;
        private int originalButton2Y;

        private string apiCall;
        private string errorMsg;
        public Menu()
        {

            InitializeComponent();

            // Asigna los valores iniciales de las variables
            originalFormWidth = this.Width;
            originalFormHeight = this.Height;

            originalButton1Width = button1.Width;
            originalButton1Height = button1.Height;
            originalButton1X = button1.Location.X;
            originalButton1Y = button1.Location.Y;

            originalButton2Width = button2.Width;
            originalButton2Height = button2.Height;
            originalButton2X = button2.Location.X;
            originalButton2Y = button2.Location.Y;
            try
            {
                apiCall = InfoComunicadorAux.GetLlamada("");
            }catch (Exception ex)
            {
                errorMsg = ex.Message;
                MessageBox.Show(errorMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                apiCall = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(apiCall))
            {
                MessageBox.Show(errorMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cartel pantalla = new(apiCall)
            {
                Size = this.Size,
                StartPosition = FormStartPosition.Manual,
                Location = this.Location
            };

            pantalla.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(apiCall))
            {
                MessageBox.Show(errorMsg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                

            Color pantalla = new(apiCall)
            {
                Size = this.Size,
                StartPosition = FormStartPosition.Manual,
                Location = this.Location
            };

            pantalla.Show();
            this.Hide();
        }

        private void Menu_SizeChanged(object sender, EventArgs e)
        {
            // Calcula las proporciones actuales del tamaño del formulario
            float widthRatio = (float)this.Width / originalFormWidth;
            float heightRatio = (float)this.Height / originalFormHeight;

            // Aplica las proporciones al tamaño y posición original del botón
            int newButton1Width = (int)(originalButton1Width * widthRatio);
            int newButton1Height = (int)(originalButton1Height * heightRatio);
            int newButton1X = (int)(originalButton1X * widthRatio);
            int newButton1Y = (int)(originalButton1Y * heightRatio);

            int newButton2Width = (int)(originalButton2Width * widthRatio);
            int newButton2Height = (int)(originalButton2Height * heightRatio);
            int newButton2X = (int)(originalButton2X * widthRatio);
            int newButton2Y = (int)(originalButton2Y * heightRatio);

            // Actualiza el tamaño y la posición del botón
            button1.Size = new Size(newButton1Width, newButton1Height);
            button1.Location = new Point(newButton1X, newButton1Y);

            button2.Size = new Size(newButton2Width, newButton2Height);
            button2.Location = new Point(newButton2X, newButton2Y);
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
