using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using _06Publicaciones.config;
using System.Data.SqlClient;

namespace _06Publicaciones.Models
{
    class CiudadesModel
    {
        public int IdCiudad { get; set; }
        public string Detalle { get; set; }
        public int idPais { get; set; }

        public DataTable todosconrelacion()
        {
            var cadena = "SELECT Ciudades.IdCiudad, Ciudades.Detalle as Ciudad, Paises.IdPais, Paises.Detalle AS 'Pais' FROM Ciudades INNER JOIN Paises ON Ciudades.idPais = Paises.IdPais";
            using (var cn = Conexion.GetConnection())
            {
                try
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(cadena, cn);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    return tabla;
                }
                catch (SqlException ex)
                {
                    ErrorHandler.ManejarErrorSql(ex, "Error al insertar el autor.");
                }
                catch (Exception ex)
                {
                    ErrorHandler.ManejarErrorGeneral(ex, "Error al insertar el autor.");
                }
                return null;
            }
        }

        public void GuardarCiudad(string id, string detalleCiudad, int idPais)
        {
            using (var cn = Conexion.GetConnection())
            {
                string query;
                if (string.IsNullOrEmpty(id))
                {
                    query = "INSERT INTO Ciudades (Detalle, idPais) VALUES (@Detalle, @idPais)";
                }
                else
                {
                    query = "UPDATE Ciudades SET Detalle = @Detalle, idPais = @idPais WHERE IdCiudad = @IdCiudad";
                }

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Detalle", detalleCiudad);
                    cmd.Parameters.AddWithValue("@idPais", idPais);
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@IdCiudad", id);
                    }

                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ErrorHandler.ManejarErrorSql(ex, "Error al insertar la ciudad");
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler.ManejarErrorGeneral(ex, "Error al insertar la ciudad.");
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
        }
    }
}
