using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64ToFileService
{
    public class Logs
    {
        public static void LogErrores(string Error)
        {
            var path = @"C:\ConvertFile";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var pathfield = @"C:\\ConvertFile\\Error " + DateTime.Now.ToString("dd-MMMM-yyyyy") + ".txt";
            StreamWriter sw = new StreamWriter(pathfield, true, ASCIIEncoding.ASCII);
            sw.WriteLine("Error a las: " + DateTime.Now.ToString("hh:mm:ss"));
            sw.WriteLine("Razón: " + Error);
            sw.Close();
        }

        public static void NoesBase64(long id)
        {
            var path = @"C:\ConvertFile";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var pathfield = @"C:\\ConvertFile\\NoBase64.txt";
            StreamWriter sw = new StreamWriter(pathfield, true, ASCIIEncoding.ASCII);          
            sw.WriteLine("El registro con el id: " + id.ToString() +" no es una base64");
            sw.Close();
        }
    }
}
