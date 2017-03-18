using System;
using System.IO;

namespace Crawler_WinFrom
{

    class WriteLog
    {

        private const string path = @"D:\";

        public static string Path => path;
        /// <summary>
        /// Write log to the path  D:\\Crawler-Log\\
        /// </summary>
        /// <param name="log">the log message</param>
        public static void Write(string log)
        {
            FileStream stream = null;
            StreamWriter pen = null;
            try
            {

                stream = new FileStream($"{Path}{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}log.txt", FileMode.Append);
                 pen= new StreamWriter(stream);
                pen.WriteLine(log);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                
            }
            pen.Close();
            stream.Close();
        }
    }
}
