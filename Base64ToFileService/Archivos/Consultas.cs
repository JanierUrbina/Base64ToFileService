using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base64ToFileService.Modelo;
using Npgsql;

namespace Base64ToFileService.Archivos
{
    public static class Consultas
    {
        public static void Obtener(string cadenaconexion)
        {
			try
			{
                string query = "SELECT * FROM imgs;";
                using (NpgsqlConnection connection = new NpgsqlConnection(cadenaconexion))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using(NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read()) 
                            {                                 
                                var obj = new Imagenes()
                                {
                                    Id = Convert.ToInt64(reader["id"]),
                                    archivo = reader.GetString(reader.GetOrdinal("archivo")),
                                    nombrearchivo = reader.GetString(reader.GetOrdinal("nombrearchivo"))
                                };                               
                                string ruta = Files.ConvertToFile(obj);
                                if(ruta!="") //Si se reemplazó correctamente
                                {
                                    CambiarPorRuta(ruta, obj.Id, cadenaconexion);
                                }
                            }
                        }
                    }
                }
			}
			catch (Exception ex)
			{
                Logs.LogErrores(ex.Message);
			}
        }

        private static void CambiarPorRuta(string ruta, long id, string cadenaconexion)
        {
           
                string query = @"
                             UPDATE imgs
                             SET archivo = @ruta
                             where id = @id;";

                using (NpgsqlConnection connection = new NpgsqlConnection(cadenaconexion))
                {
                    
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@ruta", ruta);
                        try
                        {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            Logs.LogErrores(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
          
        }
    }
}
