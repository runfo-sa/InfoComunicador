using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoComunicador
{
    public partial class Cartel : Form
    {
        string apiCallLocal;
        public Cartel(string apiCall)
        {
            InitializeComponent();
            var TamaniosFuente = new List<Tamanio>
            {
                new Tamanio() {tam=9, nombre="PEQUEÑA"},
                new Tamanio() {tam=15, nombre="MEDIANA"},
                new Tamanio() {tam=20, nombre="GRANDE"}
            };

            listBox1.DataSource= TamaniosFuente;
            listBox1.DisplayMember= "nombre";
            listBox1.ValueMember= "tam";

            listBox1.SetSelected(1, true);

            apiCallLocal = apiCall;

            label1.Text = ComunicacionAPI.Get(apiCall + "cartel/", "Cartel");
        }

        private void Cartel_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void Cartel_Load(object sender, EventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tamanio itemSeleccionado = (Tamanio)listBox1.SelectedItem;
            string respuestaJson = ComunicacionAPI.PostCartel(apiCallLocal + "cartel/nuevo", textBox1.Text, itemSeleccionado.tam);
            label1.Text = ComunicacionAPI.Get(apiCallLocal + "cartel/", "Cartel");
            MessageBox.Show(ProcesarJson.GetCadena(respuestaJson, "Estado"), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public class Tamanio
        {
            public int tam { get; set;}
            public string nombre { get; set;}
        }
    }
}
