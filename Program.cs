using arena;

int count = 0;
try
{
    count = int.Parse(args[0]);
}
catch (Exception ex)
{
    Console.Error.WriteLine("Please give an integer value as the 1st param!");
    Environment.Exit(0xC);
}

Arena arena = new Arena(count);
arena.StartFighting();

