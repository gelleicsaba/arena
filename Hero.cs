namespace arena
{

    public class Hero
    {
        public int Id { get; }
        public HeroType Type { get; }
        public int Life { get; set; }
        public int Maxlife { get; }
        public string OutputName { get; }
        public bool Dead { get => Life < Maxlife >> 2; }

        public Hero(int Id, HeroType Type, int Maxlife)
        {
            this.Id = Id;
            this.Type = Type;
            this.Maxlife = Life = Maxlife;
            OutputName = string.Format("{0}{1}", Type.ToString()[0], Id);
        }
    }

    public enum HeroType
    {
        Horseman = 1,
        Archer = 2,
        Swordsman = 3
    }

}
