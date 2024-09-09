using _06Publicaciones.config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _06Publicaciones.Models;

namespace _06Publicaciones.Views.Usuarios
{
    public partial class frm_usuarios : Form
    {
        public frm_usuarios()
        {
            InitializeComponent();
        }

        public void CargaUsuarios()
        {
            var listaUsuarios = UsuariosModel.ObtenerTodos();
            lst_Usuarios.DataSource = null;
            lst_Usuarios.DataSource = listaUsuarios;
            lst_Usuarios.DisplayMember = "UsuarioCompleto";
            lst_Usuarios.ValueMember = "ID";
        }


            private bool validarcampos(params TextBox[] cajadetexto)
        {
            foreach (var caja in cajadetexto)
            {
                if (string.IsNullOrWhiteSpace(caja.Text))
                {
                    ErrorHandler.ManejarErrorGeneral(null, "Complete la informacion");
                    return false;
                }
            }
            return true;
        }
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validarcampos(txt_Usuario, txt_Contrasenia, txt_Rol, txt_ID))
                {
                    return;
                }

                if (!int.TryParse(txt_ID.Text, out int id))
                {
                    ErrorHandler.ManejarErrorGeneral(null, "El ID debe ser un número entero.");
                    return;
                }

                var usuario = new UsuariosModel
                {
                    ID = id, // Asignación directa del ID convertido a entero
                    NombreUsuario = txt_Usuario.Text,
                    Password = txt_Contrasenia.Text,
                    Roles = txt_Rol.Text
                };

                var insertado = UsuariosModel.Insertar(usuario);
                if (insertado != null)
                {
                    CargaUsuarios();
                    ErrorHandler.ManejarInsertar();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "");
            }
        }

        private void btn_Limpiar_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "";
            txt_Contrasenia.Text = "";
            txt_Rol.Text = "";
            txt_Usuario.Text = "";
        }

        private void lst_Usuarios_DoubleClick(object sender, EventArgs e)
        {
            if (lst_Usuarios.SelectedItem != null)
            {
                if (int.TryParse(lst_Usuarios.SelectedValue.ToString(), out int id))
                {
                    var usuario = UsuariosModel.ObtenerPorId(id);
                    txt_ID.Text = usuario.ID.ToString();
                    txt_Usuario.Text = usuario.NombreUsuario;
                    txt_Contrasenia.Text = usuario.Password;
                    txt_Rol.Text = usuario.Roles;
                }
                else
                {
                    ErrorHandler.ManejarErrorGeneral(null, "El ID seleccionado no es válido.");
                }
            }
        }

        private void frm_usuarios_Load(object sender, EventArgs e)
        {
            CargaUsuarios();
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (lst_Usuarios.SelectedItem != null)
            {
                if (int.TryParse(lst_Usuarios.SelectedValue.ToString(), out int id))
                {
                    // Confirmar la eliminación
                    var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?",
                                                         "Confirmar Eliminación",
                                                         MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        // Llamar al método de eliminación
                        string resultado = UsuariosModel.Eliminar(id);
                        if (resultado == "Usuario eliminado correctamente.")
                        {
                            // Actualizar la lista de usuarios
                            CargaUsuarios();
                            MessageBox.Show(resultado);
                        }
                        else
                        {
                            ErrorHandler.ManejarErrorGeneral(null, resultado);
                        }
                    }
                }
                else
                {
                    ErrorHandler.ManejarErrorGeneral(null, "El ID seleccionado no es válido.");
                }
            }
            else
            {
                ErrorHandler.ManejarErrorGeneral(null, "No hay ningún usuario seleccionado.");
            }
        }
    }
    }

