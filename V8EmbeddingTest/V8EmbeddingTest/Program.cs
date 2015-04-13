using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V8EmbeddingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var v8Engine = new V8ScriptEngine();

            string line = null;

            while (line != String.Empty)
            {
                line = Console.ReadLine();
                Console.WriteLine(v8Engine.ExecuteCommand(line));
            }
        }
    }
}
