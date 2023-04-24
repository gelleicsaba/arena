using System.Reflection;
using System.Text;

namespace arena
{
    public class Arena
    {
        int count { get; }
        Hero[] Heroes { get; }
        Random rnd { get; }
        string LogFile { get; }
        int turn { get; set; }
        StringBuilder log;

        List<Hero> LivingHeroes { get => Heroes.Where(q => !q.Dead).ToList(); }

        public Arena(int count)
        {
            this.count = count;
            Heroes = new Hero[count];
            rnd = new Random();
            LogFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "log.txt");
        }

        public void StartFighting()
        {
            log = new StringBuilder();
            WriteLog("START ARENA\r\n");
            WriteLog("A=Archer    S=Swordsman    H=Horseman\r\n\r\n");
            File.WriteAllText(LogFile, log.ToString());

            for (int x = 0; x < count; ++x)
            {
                HeroType heroType = (HeroType)rnd.Next(1, 4);
                ushort maxLife = 0;
                switch (heroType)
                {
                    case HeroType.Archer:
                        maxLife = 100;
                        break;
                    case HeroType.Horseman:
                        maxLife = 150;
                        break;
                    case HeroType.Swordsman:
                        maxLife = 120;
                        break;
                }
                Heroes[x] = new Hero(x + 1, heroType, maxLife);
            }
            turn = 1;

            List<Hero> livingHeroes = LivingHeroes;
            while (livingHeroes.Count > 1)
            {
                log = new StringBuilder();
                WriteLog("START " + (turn++) + ". ROUND  *** Living heroes: " + livingHeroes.Count + "\r\n");
                
                int idx = rnd.Next(0, livingHeroes.Count);
                Hero attacker = livingHeroes.ElementAt(idx);
                livingHeroes.RemoveAt(idx);
                
                idx = rnd.Next(0, livingHeroes.Count);
                Hero defender = livingHeroes.ElementAt(idx);
                livingHeroes.RemoveAt(idx);

                int attackerPreLife = attacker.Life;
                int defenderPreLife = defender.Life;
                foreach (Hero someone in livingHeroes)
                {
                    someone.Life = Math.Min(someone.Maxlife, someone.Life + 10);
                }
                attacker.Life >>= 1;
                defender.Life >>= 1;

                WriteLog(string.Format("{0} & {1} are fighting\r\n", attacker.OutputName, defender.OutputName));
                WriteLog(string.Format("{0}'s LF halved: {1} ==> {2}\r\n{3}'s LF halved: {4} ==> {5}\r\n"
                    , attacker.OutputName, attackerPreLife, attacker.Life, defender.OutputName, defenderPreLife, defender.Life));

                if (!attacker.Dead && !defender.Dead)
                {
                    WriteLog(string.Format("{0} hits {1}\r\n", attacker.OutputName, defender.OutputName));
                    Hit(attacker, defender);
                }
                if (attacker.Dead)
                {
                    WriteLog(string.Format("{0} has dead.\r\n", attacker.OutputName));
                }
                if (defender.Dead)
                {
                    WriteLog(string.Format("{0} has dead.\r\n", defender.OutputName));
                }
                
                livingHeroes = LivingHeroes;

                WriteLog("END TURN: *** Living heroes: " + livingHeroes.Count + "   Dead heroes: " + (count - livingHeroes.Count) + "  \r\n");
                WriteLog(string.Format("{0}'s LF: {2} ==> {3}\r\n{1}'s LF: {4} ==> {5}\r\n\r\n\r\n"
                    , attacker.OutputName, defender.OutputName, attackerPreLife, attacker.Life, defenderPreLife, defender.Life));

                File.AppendAllText(LogFile, log.ToString());
            }

            log = new StringBuilder();
            WriteLog("-----------  E N D  -----------------\r\n");

            WriteLog(string.Format("TOTAL ROUNDS: {0}\r\n", turn-1));
            if (livingHeroes.Count == 1)
            {
                WriteLog(string.Format("THE ARENA WINNER: {0}\r\n", livingHeroes.ElementAt(0).OutputName));
            } else
            {
                WriteLog("EVERYONE HAS DEAD.\r\n");
            }
            File.AppendAllText(LogFile, log.ToString());

            Console.WriteLine("\r\nPRESS ANY KEY TO EXIT");
            Console.ReadKey();

        }

        public void Hit(Hero a, Hero b)
        {
            switch (a.Type)
            {
                case HeroType.Archer: 
                    switch (b.Type)
                    {
                        case HeroType.Horseman:
                            int chance = rnd.Next(1, 101);
                            if (chance <= 40)
                            {
                                b.Life = 0;
                                WriteLog(string.Format("{0} kills {1}.\r\n", a.OutputName, b.OutputName));
                            }
                            else
                            {
                                WriteLog(string.Format("{0} has defended the attack.\r\n", b.OutputName));
                            }
                            break;
                        case HeroType.Swordsman:
                        case HeroType.Archer:
                            b.Life = 0;
                            WriteLog(string.Format("{0} kills {1}.\r\n", a.OutputName, b.OutputName));
                            break;
                    }
                    break;
                case HeroType.Swordsman:
                    switch (b.Type)
                    {
                        case HeroType.Swordsman:
                        case HeroType.Archer:
                            b.Life = 0;
                            WriteLog(string.Format("{0} kills {1}.\r\n", a.OutputName, b.OutputName));
                            break;
                    }
                    break;
                case HeroType.Horseman:
                    switch (b.Type)
                    {
                        case HeroType.Swordsman:
                            a.Life = 0;
                            WriteLog(string.Format("{1} kills {0}.\r\n", a.OutputName, b.OutputName));
                            break;
                        case HeroType.Horseman:
                        case HeroType.Archer:
                            b.Life = 0;
                            WriteLog(string.Format("{0} kills {1}.\r\n", a.OutputName, b.OutputName));
                            break;
                    }
                    break;
            }
        }

        public void WriteLog(string logw)
        {
            log.Append(logw);
            Console.Write(logw);
        }

    }
}
