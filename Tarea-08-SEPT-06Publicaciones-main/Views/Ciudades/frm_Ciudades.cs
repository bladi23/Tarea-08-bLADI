using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _06Publicaciones.Controllers;
using _06Publicaciones.config;
using System.Runtime.Remoting.Contexts;
using _06Publicaciones.Models;
namespace _06Publicaciones.Views.Ciudades
{
    public partial class frm_Ciudades : Form
    {
        private string id;
        private CiudadesModel CiudadesModel = new CiudadesModel();

        public frm_Ciudades(string id, string detalleCiudad, string detallePais )
        {
            InitializeComponent();
            this.id = id;
            this.CiudadesModel = new CiudadesModel();

            textBox1.Text = detalleCiudad;
            comboBox1.Text = detallePais;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frm_Ciudades_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(this.id);
            PaisesController _paises = new PaisesController();
            comboBox1.DataSource = _paises.todos();
            comboBox1.DisplayMember = "Detalle";
            comboBox1.ValueMember = "IdPais";
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string detalleCiudad = textBox1.Text;
            int idPais = (int)comboBox1.SelectedValue;

            CiudadesModel.GuardarCiudad(id, detalleCiudad, idPais);
            MessageBox.Show("Ciudad guardada correctamente.");
            this.Close();
        }

        private void btn_CancelarCiudad_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

