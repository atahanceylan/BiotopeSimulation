using HeroWillSurviveOrNot.Services;
using HeroWillSurviveOrNot.Species;
using Serilog;
using System.Configuration;
using System.Reflection;

namespace HeroWillSurviveOrNot;

public static class Program
{
    private static void Main(string[] args)
    {
        string fileName = ConfigurationManager.AppSettings["InputFileName1"];
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
        string filePath = Path.Combine(assemblyDirectory, "SampleInputs", fileName);

        Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .MinimumLevel.Debug()
                    .CreateLogger();

        if (filePath != null)
        {
            try
            {
                BunkerBiotope bunkerBiotope = new FileService().Parse(filePath);
                new WalkService().Walk(bunkerBiotope);
                bunkerBiotope.Hero.IsHeroSurvived();
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                throw;
            }
        }
        else
        {
            Console.WriteLine("FilePath is null or empty please check App.Config file");
            return;
        }
        Console.Read();
    }
}



