using System.ServiceProcess;

namespace Crawler
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase ServicesToRun;
            ServicesToRun = new Crawler();
            ServiceBase.Run(ServicesToRun);
        }
    }
}
