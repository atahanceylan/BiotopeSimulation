using HeroWillSurviveOrNot.Species;

namespace HeroWillSurviveOrNot.Services
{
    public static class FightService
    {
        public static void Simulate(BunkerBiotope bunkerBiotope)
        {
            HeroWalk(bunkerBiotope);
            IsHeroSurvived(bunkerBiotope.Enemies);
        }

        private static void HeroWalk(BunkerBiotope bunkerBiotope)
        {
            int currentPosition = bunkerBiotope.Enemies.First().Position;
            while (currentPosition < bunkerBiotope.Resource.Position)
            {
                Enemy? currentEnemy = bunkerBiotope.Enemies.Find(x => x.Position == currentPosition);
                if (currentEnemy != null)
                {
                    bunkerBiotope.Hero.Position = currentPosition;
                    bool isEnemyDead = bunkerBiotope.Hero.Fight(currentEnemy);
                    if (isEnemyDead)
                    {
                        bunkerBiotope.Enemies.Remove(currentEnemy);
                    }
                    else
                    {
                        break;
                    }
                }
                currentPosition = bunkerBiotope.Enemies.Any() ? bunkerBiotope.Enemies.First().Position : bunkerBiotope.Resource.Position;
            }
        }

        private static void IsHeroSurvived(List<Enemy> enemies)
        {
            if (!enemies.Any())
            {
                Console.WriteLine("Hero Survived!");
            }
        }
    }
}
