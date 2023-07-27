using HeroWillSurviveOrNot.Species;

namespace HeroWillSurviveOrNot.Services
{
    public class WalkService : IWalkService
    {
        public void Walk(BunkerBiotope bunkerBiotope)
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
    }
}
