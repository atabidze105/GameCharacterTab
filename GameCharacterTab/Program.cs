using GameCharacterTab;  

class Program
{
    static void Main()
    {
        List<Game> characters = new List<Game>();
        List<Game> alive = new List<Game>();
        List<Game> dead = new List<Game>();

        //Console.WriteLine("Добро пожаловать в Игру..");
        Console.WriteLine("\nСоздайте игроков для первой команды");
        string answer = "";
        while (answer != "нет")
        {
            Game character = new Game();
            character.charCreation(characters, alive, dead, true);
            characters.Add(character);

            do
            {
                Console.WriteLine("Продолжить? (да/нет)");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "да":
                    case "нет":
                        break;
                    default:
                        answer = "";
                        break;
                }
            } while (answer != "да" && answer != "нет");
        }

        Console.WriteLine("\nСоздайте игроков для второй команды");
        answer = "";
        while (answer != "нет")
        {
            Game character = new Game();
            character.charCreation(characters, alive, dead, false);
            characters.Add(character);

            do
            {
                Console.WriteLine("Продолжить? (да/нет)");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "да":
                    case "нет":
                        break;
                    default:
                        answer = "";
                        break;
                }
            } while (answer != "да" && answer != "нет");
        }

        Console.WriteLine("Создание персонажей завершено.\nВыберите команду, за которую хотите играть:\nКоманда 1 - 1\nКоманда 2 - 2");

        do
        {
            Console.WriteLine("\nВыберите команду, за которую хотите играть:\nКоманда 1 - 1\nКоманда 2 - 2");
            answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                case "2":
                    break;
                default:
                    answer = "";
                    break;
            }
        } while (answer != "1" && answer != "2");
















        
    }
}