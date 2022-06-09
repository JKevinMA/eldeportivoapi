using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Helpers
{
    public class Log
    {
        /// <summary>
        /// Registra eventos de error en directorio Log
        /// </summary>
        /// <param name="obj">Objeto padre (this)</param>
        /// <param name="ex">Excepcion, mostrara mensaje de excepcion</param>
        /// <param name="adicional">texto adicional</param>
        public static void Save(object obj, Exception ex, string adicional)
        {
            try
            {
                string fecha = System.DateTime.Now.ToString("yyyyMMdd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + fecha + ".txt";

                StreamWriter sw = new StreamWriter(path, true);

                StackTrace stacktrace = new StackTrace();
                sw.WriteLine(hora + " - " + obj.GetType().FullName + " / " + stacktrace.GetFrame(1).GetMethod().Name + "(...)");
                sw.WriteLine("-> " + ex);
                sw.WriteLine("");

                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("-", e.Message);
            }
        }

        public static void Save(object obj, string adicional)
        {
            try
            {
                string fecha = System.DateTime.Now.ToString("yyyyMMdd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + fecha + ".txt";

                StreamWriter sw = new StreamWriter(path, true);

                StackTrace stacktrace = new StackTrace();
                sw.WriteLine(hora + " - " + obj.GetType().FullName + " / " + stacktrace.GetFrame(1).GetMethod().Name + "(...)");
                sw.WriteLine("-> " + adicional);
                sw.WriteLine("");

                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("-", e.Message);
            }
        }

        internal static void Save(string v)
        {
            throw new NotImplementedException();
        }
    }
}
