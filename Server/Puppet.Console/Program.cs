using System;
using Puppet.Http;

namespace Puppet.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var inputLine = System.Console.ReadLine()?.Split(' ');
                WorkerManager.Instance.SetCommand(inputLine);
            }
        }
    }
}