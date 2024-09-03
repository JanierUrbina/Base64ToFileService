using Base64ToFileService.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64ToFileService.Archivos
{
    public static class Files
    {
        public static string ConvertToFile(Imagenes imagen)
        {
			try
			{
                if(EsBase64(imagen.archivo))
                {
                    byte[] filebytes = Convert.FromBase64String(imagen.archivo);
                    string ruta = "";
                    string filePath = @"C:\\Archivo";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);//Si no existe, crea la ruta
                    }
                    var file = new FormFileFromMemory(filebytes, imagen.nombrearchivo);

                    ruta = Path.Combine(filePath, file.FileName);

                    // Usa el bloque `using` para asegurar que el archivo se cierra correctamente
                    using (var fileStream = new FileStream(ruta, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }

                    return ruta;
                }
                else
                {
                    Logs.NoesBase64(imagen.Id);
                    return "";                   
                }
				
            }
            catch (Exception ex)
			{
                Logs.LogErrores(ex.Message);
                throw;
			}
        }

        private static bool EsBase64(string cadena)
        {
            try
            {
                // Si no existe una excepcion, es una cadena base64
                byte[] data = Convert.FromBase64String(cadena);
                return (cadena.Replace(" ", "").Length % 4 == 0);
            }
            catch
            {
                // Si hay excepción, significa que no pudo ser decodificada, por tanto no es una cadena de base64
                return false;
            }
        }
    }
}
