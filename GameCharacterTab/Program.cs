using GameCharacterTab;

class Program
{
    static void Main()
    {
        List<Game> characters = new();
        List<Game> alive = new();

        Game idle = new(); //пустой объект для применения общего метода
        idle.Playing(characters, alive);
    }
}