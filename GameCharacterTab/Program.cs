using GameCharacterTab;

class Program
{
    static void Main()
    {
        List<Game> characters = new();
        List<Game> alive = new();
        List<Game> dead = new();

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

        Console.WriteLine("Создание персонажей завершено.\nВыберите персонажа, за которого хотите играть:");

        int i = 1;
        foreach (Game gamer in characters)
        {
            Console.Write($"{i}. {gamer.Name} - ");
            if (gamer.Team == true)
            {
                Console.Write("команда 1");
            }
            else
            {
                Console.Write("команда 2");
            }
            if (alive.Contains(gamer) == true)
            {
                Console.Write( " - жив\n");
            }
            else
            {
                Console.Write(" - мертв\n");
            }
            i++;
        }

        Console.WriteLine("\nВведите номер персонажа, за которого хотите играть:");
        
        
        string numb = "";
        numb = Console.ReadLine();
        if (Convert.ToInt32(numb) > 0 && Convert.ToInt32(numb) < characters.Count())
        {
            characters[Convert.ToInt32(numb)-1].Gaming(characters, alive, dead);
        }
        else
        {
            Console.WriteLine("Выбран несуществующий номер персонажа.");
        }

        //while (numb == "")
        //{
        //    numb = Console.ReadLine();
        //    foreach (Game character in characters)
        //    {
        //        try
        //        {
        //            if (Convert.ToInt32(numb) > 0 && Convert.ToInt32(numb) < characters.Count())
        //            {
        //                characters[Convert.ToInt32(numb)].Gaming(characters,alive,dead);
        //            }
        //            else
        //            {
        //                Console.WriteLine("Выбран несуществующий номер персонажа.");
        //            }
        //        }
        //        catch
        //        {
        //            numb = "";
        //        }
        //    }
        //}

        //do
        //{
        //    Console.WriteLine("\nВыберите команду, за которую хотите играть:\nКоманда 1 - 1\nКоманда 2 - 2");
        //    answer = Console.ReadLine();
        //    switch (answer)
        //    {
        //        case "1":
        //        case "2":
        //            break;
        //        default:
        //            answer = "";
        //            break;
        //    }
        //} while (answer != "1" && answer != "2");

        
        
















    }
}