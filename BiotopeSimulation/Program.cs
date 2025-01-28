using Serilog;
using System.Reflection;
using BiotopeSimulation.Services;
using static System.Configuration.ConfigurationManager;

namespace BiotopeSimulation;

public static class Program
{
    private static void Main()
    {
        var fileName = AppSettings["InputFileName1"];
        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyPath);
        if (assemblyDirectory != null)
        {
            if (fileName != null)
            {
                var filePath = Path.Combine(assemblyDirectory, "SampleInputs", fileName);

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .MinimumLevel.Debug()
                    .CreateLogger();

                try
                {
                    FightService.Simulate(new FileService().ParseTextFileToObjects(filePath));
                }
                catch (Exception ex)
                {
                    Log.Error(ex.StackTrace);
                    throw;
                }
            }
        }

        Console.Read();
    }
}



