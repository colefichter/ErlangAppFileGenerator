using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ErlangAppFileGenerator
{
    class Program
    {
        public static readonly string EXPECTED_FOLDER = @"src\";
        public static readonly string OUTPUT_FOLDER = @"ebin\";
        public static readonly string EXTENSION = @".app.src";
        public static readonly string OUTPUT_EXTENSION = @".app";
        public static readonly string PLACEHOLDER = @"\[modules\]";
        public static readonly string SEARCH_PATTERN = @"*.erl";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Usage();
            }
            else
            {
                try
                {
                    Run(args[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void Usage()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(" USAGE:");
            Console.WriteLine("");
            Console.WriteLine("     >ErlangAppFileGenerator.exe my_app_name");
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------");
        }

        static void Run(string appName)
        {
            string appFileTemplate = File.ReadAllText(Path.Combine(EXPECTED_FOLDER, appName + EXTENSION));
            string moduleList = Modules();

            Regex re = new Regex(PLACEHOLDER, RegexOptions.IgnoreCase);
            string output = re.Replace(appFileTemplate, moduleList);

            File.WriteAllText(Path.Combine(OUTPUT_FOLDER, appName + OUTPUT_EXTENSION), output);
        }

        static string Modules()
        {
            DirectoryInfo di = new DirectoryInfo(EXPECTED_FOLDER);
            
            List<string> modules = new List<string>();
            foreach (FileInfo fi in di.GetFiles(SEARCH_PATTERN))
            {
                modules.Add(fi.Name.Replace(fi.Extension, string.Empty));
            }

            return string.Join(", ", modules);
        }
    }
}
