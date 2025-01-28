
using BiotopeSimulation.Species;
using Serilog;

namespace BiotopeSimulation.Services
{
    public class FileService
    {
        public BunkerBiotope ParseTextFileToObjects(string filePath)
        {
            Hero hero = new();
            List<Enemy> enemyTypeList = new();
            List<Enemy> enemyList = new();
            Resource resource = new();
            foreach (var line in File.ReadLines(filePath))
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

            if (enemyTypeList.Count == 0)
            {
                throw new Exception();
            }
            enemyList = enemyList.OrderBy(x => x.Position).ToList();
            return new BunkerBiotope(resource, enemyList, hero);
        }
        private static void EnemyHitPowerAndAttackPowerSet(List<Enemy> enemyTypeList, string line)
        {
            foreach (var t in enemyTypeList)
            {
                if (line.Contains(t.Name + " has "))
                {
                    var lineElements = line.Split(' ');
                    if (lineElements.Length > 2)
                    {
                        var isNumeric = int.TryParse(lineElements[2], out int val);
                        if (isNumeric)
                            t.HitPower = val;                        
                    }
                        
                }

                if (!line.Contains(t.Name + " attack ")) continue;
                {
                    var lineElements = line.Split(' ');
                    if (lineElements.Length <= 3) continue;
                    var isNumeric = int.TryParse(lineElements[3], out int val);
                    if (isNumeric)
                        t.AttackPower = val;
                }
            }
        }

        private static void AddEnemyToEnemyTypeList(List<Enemy> enemyTypeList, string line)
        {
            var lineElements = line.Split(' ');
            if (lineElements.Length <= 1) return;
            var enemy = new Enemy(lineElements[0]);
            enemyTypeList.Add(enemy);
        }

        private static bool CheckEnemyType(string line)
        {
            return line.Contains("is Enemy");
        }


        private static void SetHeroAttackPower(Hero hero, string line)
        {
            var lineElements = line.Split(' ');
            if (lineElements.Length <= 3) return;
            var isNumeric = int.TryParse(lineElements[3], out int val);
            if (isNumeric)
                hero.AttackPower = val;
        }

        private static bool CheckHeroAttackPower(string line)
        {
            return line.StartsWith("Hero attack");
        }

        private static void SetHeroHitPower(Hero hero, string line)
        {
            var lineElements = line.Split(' ');
            if (lineElements.Length > 2)
            {
                var isNumeric = int.TryParse(lineElements[2], out int val);
                if (isNumeric)
                    hero.HitPower = val;
            }
            Console.WriteLine($"Hero started journey with {hero.HitPower} HP!");
        }

        private static bool CheckHeroHitPower(string line)
        {
            return line.StartsWith("Hero has");
        }

        private static void SetResource(Resource resource, string line)
        {
            var lineElements = line.Split(' ');
            if (lineElements.Length <= 2) return;
            var isNumeric = int.TryParse(lineElements[2], out int val);
            if (isNumeric)
                resource.Position = val;
        }

        private static bool CheckResource(string line)
        {
            return line.StartsWith("Resource");
        }

        private static void EnemyPositionSet(List<Enemy> enemyTypeList, List<Enemy> enemyList, string line)
        {
            if (!line.Contains(" at position ")) return;
            foreach (var t in enemyTypeList)
            {
                if (!line.Contains(t.Name)) continue;
                var lineElements = line.Split(' ');
                if (lineElements.Length <= 6) continue;
                var isNumeric = int.TryParse(lineElements[6], out int val);
                if (!isNumeric) continue;
                var enemy = new Enemy
                (
                    t.HitPower,
                    t.AttackPower,
                    val,
                    t.Name
                );
                enemyList.Add(enemy);
                break;
            }
        }
    }
}

