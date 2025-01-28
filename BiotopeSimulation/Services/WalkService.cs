using BiotopeSimulation.Species;

namespace BiotopeSimulation.Services
{
    public class WalkService : IWalkService
    {
        public void Walk(BunkerBiotope bunkerBiotope)
        {
            if (bunkerBiotope.Enemies.Count <= 0) return;
            var currentPosition = bunkerBiotope.Enemies.First().Position;
            while (currentPosition < bunkerBiotope.Resource.Position)
            {
                var currentEnemy = bunkerBiotope.Enemies.Find(x => x.Position == currentPosition);
                if (currentEnemy != null)
                {
                    bunkerBiotope.Hero.Position = currentPosition;
                    var isEnemyDead = bunkerBiotope.Hero.Fight(currentEnemy);
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
