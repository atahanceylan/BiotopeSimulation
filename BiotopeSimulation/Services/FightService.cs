
using BiotopeSimulation.Species;

namespace BiotopeSimulation.Services
{
    public static class FightService
    {
        public static void Simulate(BunkerBiotope bunkerBiotope)
        {
            new WalkService().Walk(bunkerBiotope);
            IsHeroSurvived(bunkerBiotope.Enemies);
        }
        
        private static void IsHeroSurvived(List<Enemy> enemies)
        {
            if (enemies.Any()) return;
            Console.WriteLine("Hero Survived!");
        }
    }
}
