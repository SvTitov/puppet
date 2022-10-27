using System;
using Puppet.Http;

namespace Puppet.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new WorkerManager();
            
            while (true)
            {
                var inputLine = System.Console.ReadLine()?.Split(' ');
                worker.SetCommand(inputLine);
            }
        }
    }
}