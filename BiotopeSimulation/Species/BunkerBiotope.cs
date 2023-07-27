namespace HeroWillSurviveOrNot.Species
{
    public class BunkerBiotope
    {
        public BunkerBiotope(Resource resource, List<Enemy> enemies, Hero hero)
        {
            Resource = resource;
            Enemies = enemies;
            Hero = hero;
        }
        public Resource Resource { get; set; }

        public List<Enemy> Enemies { get; set; }

        public Hero Hero { get; set; }
    }
}
