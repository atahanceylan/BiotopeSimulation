namespace HeroWillSurviveOrNot.Species
{
    public class Enemy
    {
        public int HitPower { get; set; }

        public int AttackPower { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }
        public Enemy(string name)
        {
            Name = name;
        }

        public Enemy(int hitPower, int attackPower, int position, string name)
        {
            HitPower = hitPower;
            AttackPower = attackPower;
            Position = position;
            Name = name;
        }

    }
}
