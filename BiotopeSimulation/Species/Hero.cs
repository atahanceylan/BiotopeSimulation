namespace HeroWillSurviveOrNot.Species
{
    public class Hero
    {
        public int HitPower { get; set; }

        public int AttackPower { get; set; }

        public int Position { get; set; }

        public bool Fight(Enemy e)
        {
            if (HitPower > e.HitPower)
            {
                while (e.HitPower > 0)
                {
                    e.HitPower -= AttackPower;
                    HitPower -= e.AttackPower;
                }

                Console.WriteLine($"Hero defeated {e.Name} with {HitPower} HP remaining");
                return true;
            }
            else
            {
                while (HitPower > 0)
                {
                    HitPower -= e.AttackPower;
                    e.HitPower -= AttackPower;
                }
                Console.WriteLine($"{e.Name} defeated {nameof(Hero)} with {e.HitPower} HP remaining");
                Console.WriteLine($"Hero is Dead!! Last seen at position {Position}!!");
                return false;
            }
        }

        public void IsHeroSurvived()
        {
            if(IsHeroAlive)
            {
                Console.WriteLine("Hero Survived!");
            }            
        }

        public bool IsHeroAlive => HitPower > 0;
    }
}
