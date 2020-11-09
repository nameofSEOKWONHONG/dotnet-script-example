using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dotnet.Script.Core;
using Dotnet.Script.DependencyModel.Context;
using JWLibrary.Core;
using JWLibrary.Util.CLI;
using JWLibrary.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ConsoleApp1 {
    class Program {
        static async Task Main(string[] args) {
            Enumerable.Range(1, 100).ToList().ForEach(i => {
                args = new[] { "1", "2" };
                var scriptPath = @"H:\workspace\ConsoleApp1\ConsoleApp1\bin\Debug\netcoreapp3.1\script\add.csx";
                using var scriptStream = File.OpenRead(scriptPath);
                var compiler = new ScriptCompiler(LogFactory, true);
                var runner = new ScriptRunner(compiler, LogFactory, ScriptConsole.Default);
                var sourceText = SourceText.From(scriptStream);
                var context = new ScriptContext(sourceText, Directory.GetCurrentDirectory(), args, null, OptimizationLevel.Release, ScriptMode.Eval);

                var result = runner.Execute<RequestDto<int>>(context).GetAwaiter().GetResult();
                Console.WriteLine(result.RequestDto);

                Thread.Sleep(1000);
            });

            Dotnet.Script.DependencyModel.Logging.Logger LogFactory(Type type) {
                return (level, message, exception) =>
                {
                    Console.WriteLine($"{level} {message} {exception}");
                };
            }

            //var path = @"H:\workspace\ConsoleApp1\ConsoleApp1\bin\Debug\netcoreapp3.1\script\add.csx 1 2";
            //await ProcessHandlerAsync.RunAsync("dotnet-script", path, (output) => {
            //    Console.WriteLine(output);
            //},
            //(error) => {
            //    Console.WriteLine(error);
            //});

            //path = @"H:\workspace\ConsoleApp1\ConsoleApp1\bin\Debug\netcoreapp3.1\script\min.csx 1 2";
            //await ProcessHandlerAsync.RunAsync("dotnet-script", path, (output) => {
            //    Console.WriteLine(output);
            //},
            //(error) => {
            //    Console.WriteLine(error);
            //});
        }
    }
}
