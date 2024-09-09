using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _06Publicaciones.Views;
using _06Publicaciones.Views.Empleados;
using _06Publicaciones.Views.Financiero;

using _06Publicaciones.Controllers;
namespace _06Publicaciones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            txt_contrasenia.Text = "";
            txt_usuario.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txt_usuario.Text.Trim();
            string contrasenia = txt_contrasenia.Text.Trim();
            try
            {
                UsuariosController _usuariosController = new UsuariosController();
                var usuariomodel = _usuariosController.AutenticarUsuario(usuario, contrasenia);
                if (usuariomodel != null)
                {
                    lbl_mensaje.Text = "Ingreso exitoso";
                    this.Hide();
                    if (usuariomodel.Roles == "Admin")
                    {
                        var _general = new General();
                        this.Hide();
                        _general.Show();
                    }
                    else if (usuariomodel.Roles == "financiero")
                    {
                        var _financiero = new frm_Financiero_Principal();
                        _financiero.Show();

                    }

                    else
                    {
                        lbl_mensaje.Text = "Usted no posee el nivel de acceso necesario";
                    }
                }
                else
                {
                    lbl_mensaje.Text = "El usuario o la contraseña son incorrectos";
                    txt_contrasenia.Text = "";
                    txt_usuario.Text = "";
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


