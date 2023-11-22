using GameCharacterTab;

class Program
{
    static void Main()
    {
        List<Game> characters = new();
        List<Game> alive = new();
        List<Game> dead = new();

        Game idle = new();
        idle.GAME(characters, alive, dead);
    }
}