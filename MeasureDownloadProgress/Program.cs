using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeasureDownloadProgress
{
    class Program
    {
        private static readonly Stopwatch sw = Stopwatch.StartNew();

        private static void DownloadStringCallback(Object sender, DownloadStringCompletedEventArgs e)
        {
            // If the request was not canceled and did not throw 
            // an exception, display the resource. 
            if (!e.Cancelled && e.Error == null)
            {
                string textString = (string)e.Result;

                Console.WriteLine("-----------------------DONE-------------------");
                Console.WriteLine("Total length = " + textString.Length);
                Console.WriteLine("-----------------------Press any string to continue-------------------");
            }
        }

        private static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine("{0}\t{1}\t{2}\t{3}",
                sw.ElapsedMilliseconds / 1000.0,
                e.BytesReceived,
                e.ProgressPercentage,
                e.TotalBytesToReceive
                );
        }
        static void Main(string[] args)
        {
            var downloadUrl = args.FirstOrDefault() ?? "http://stackoverflow.com";
            Console.WriteLine("{0}\t{1}\t{2}\t{3}", "Seconds", "Bytes", "%", "Total");
            Console.WriteLine("{0}\t{1}\t{2}\t{3}", 0, 0, 0, "_");
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCallback);
            client.DownloadStringAsync(new Uri(downloadUrl));
            Console.ReadKey();
        }
    }
}
