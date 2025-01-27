using HeroWillSurviveOrNot.Species;
using Serilog;

namespace HeroWillSurviveOrNot.Services
{
    public class FileService
    {
        public BunkerBiotope Parse(string filePath)
        {
            Hero hero = new();
            List<Enemy> enemyTypeList = new();
            List<Enemy> enemyList = new();
            Resource resource = new();
            foreach (string line in File.ReadLines(filePath))
            {
                try
                {
                    if (CheckResource(line))
                    {
                        SetResource(resource, line);
                        continue;
                    }

                    if (CheckHeroHitPower(line))
                    {
                        SetHeroHitPower(hero, line);
                        continue;
                    }

                    if (CheckHeroAttackPower(line))
                    {
                        SetHeroAttackPower(hero, line);
                        continue;
                    }

                    if (CheckEnemyType(line))
                    {
                        AddEnemyToEnemyTypeList(enemyTypeList, line);
                        continue;
                    }

                    EnemyHitPowerAndAttackPowerSet(enemyTypeList, line);
                    EnemyPositionSet(enemyTypeList, enemyList, line);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.StackTrace);
                    throw;
                }
            }
            enemyList = enemyList.OrderBy(x => x.Position).ToList();
            return new BunkerBiotope(resource, enemyList, hero);
        }
        private void EnemyHitPowerAndAttackPowerSet(List<Enemy> enemyTypeList, string line)
        {
            for (int i = 0; i < enemyTypeList.Count; i++)
            {
                if (line.Contains(enemyTypeList[i].Name + " has "))
                {
                    string[] lineElements = line.Split(' ');
                    if (lineElements.Length > 2)
                    {
                        var isNumeric = int.TryParse(lineElements[2], out int val);
                        if (isNumeric)
                            enemyTypeList[i].HitPower = val;                        
                    }
                        
                }

                if (line.Contains(enemyTypeList[i].Name + " attack "))
                {
                    string[] lineElements = line.Split(' ');
                    if (lineElements.Length > 3)
                    {
                        var isNumeric = int.TryParse(lineElements[3], out int val);
                        if (isNumeric)
                            enemyTypeList[i].AttackPower = val;                        
                    }
                }
            }
        }

        private void AddEnemyToEnemyTypeList(List<Enemy> enemyTypeList, string line)
        {
            string[] lineElements = line.Split(' ');
            if (lineElements.Length > 1)
            {
                Enemy enemy = new Enemy(lineElements[0]);
                enemyTypeList.Add(enemy);
            }
        }

        private bool CheckEnemyType(string line)
        {
            return line.Contains("is Enemy");
        }


        private void SetHeroAttackPower(Hero hero, string line)
        {
            string[] lineElements = line.Split(' ');
            if (lineElements.Length > 3)
            {
                var isNumeric = int.TryParse(lineElements[3], out int val);
                if (isNumeric)
                    hero.AttackPower = val;
            }

        }

        private bool CheckHeroAttackPower(string line)
        {
            return line.StartsWith("Hero attack");
        }

        private void SetHeroHitPower(Hero hero, string line)
        {
            string[] lineElements = line.Split(' ');
            if (lineElements.Length > 2)
            {
                var isNumeric = int.TryParse(lineElements[2], out int val);
                if (isNumeric)
                    hero.HitPower = val;
            }
            Console.WriteLine($"Hero started journey with {hero.HitPower} HP!");
        }

        private bool CheckHeroHitPower(string line)
        {
            return line.StartsWith("Hero has");
        }

        private void SetResource(Resource resource, string line)
        {
            string[] lineElements = line.Split(' ');
            if (lineElements.Length > 2)
            {
                var isNumeric = int.TryParse(lineElements[2], out int val);
                if (isNumeric)
                    resource.Position = val;
            }
        }

        private bool CheckResource(string line)
        {
            return line.StartsWith("Resource");
        }

        private void EnemyPositionSet(List<Enemy> enemyTypeList, List<Enemy> enemyList, string line)
        {
            if (line.Contains(" at position "))
            {
                for (int i = 0; i < enemyTypeList.Count; i++)
                {
                    if (line.Contains(enemyTypeList[i].Name))
                    {
                        string[] lineElements = line.Split(' ');
                        if (lineElements.Length > 6)
                        {
                            var isNumeric = int.TryParse(lineElements[6], out int val);
                            if (isNumeric)
                            {
                                Enemy enemy = new Enemy
                                (
                                    enemyTypeList[i].HitPower,
                                    enemyTypeList[i].AttackPower,
                                    val,
                                    enemyTypeList[i].Name
                                );
                                enemyList.Add(enemy);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}

