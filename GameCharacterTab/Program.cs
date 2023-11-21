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
        //void gameUsing(List<Game> chars) //Метод для выбора персонажа по номеру
        //{
        //    string numb = "";

        //    while (numb == "")
        //    {
        //        Console.WriteLine("\nВведите номер персонажа, за которого хотите играть:\n");
        //        numb = Console.ReadLine();
        //        if (Convert.ToInt32(numb) > 0 && Convert.ToInt32(numb) <= characters.Count())
        //        {
        //            characters[Convert.ToInt32(numb) - 1].Gaming(characters, alive, dead);
        //        }
        //        else
        //        {
        //            Console.WriteLine("\nВыбран несуществующий номер персонажа.\n");
        //            numb = "";
        //        }
        //    }
        //}

        //Console.WriteLine("Добро пожаловать в Игру..");
        //Console.WriteLine("Создайте игроков для первой команды");
        //string answer = "";
        //while (answer != "нет")
        //{
        //    Game character = new Game();
        //    character.charCreation(characters, alive,true);
        //    characters.Add(character);

        //    do
        //    {
        //        Console.WriteLine("\nПродолжить? (да/нет)\n");
        //        answer = Console.ReadLine();
        //        switch (answer)
        //        {
        //            case "да":
        //            case "нет":
        //                break;
        //            default:
        //                answer = "";
        //                break;
        //        }
        //    } while (answer != "да" && answer != "нет");
        //}

        //Console.WriteLine("\nСоздайте игроков для второй команды");
        //answer = "";
        //while (answer != "нет")
        //{
        //    Game character = new Game();
        //    character.charCreation(characters, alive, false);
        //    characters.Add(character);

        //    do
        //    {
        //        Console.WriteLine("\nПродолжить? (да/нет)\n");
        //        answer = Console.ReadLine();
        //        switch (answer)
        //        {
        //            case "да":
        //            case "нет":
        //                break;
        //            default:
        //                answer = "";
        //                break;
        //        }
        //    } while (answer != "да" && answer != "нет");
        //}

        //Console.WriteLine("Создание персонажей завершено.\nВыберите персонажа, за которого хотите играть:\n");

        //int i = 1;
        //foreach (Game gamer in characters)
        //{
        //    Console.Write($"{i}. {gamer.Name} - ");
        //    if (gamer.Team == true)
        //    {
        //        Console.Write("команда 1");
        //    }
        //    else
        //    {
        //        Console.Write("команда 2");
        //    }
        //    if (alive.Contains(gamer) == true)
        //    {
        //        Console.Write(" - жив\n");
        //    }
        //    else
        //    {
        //        Console.Write(" - мертв\n");
        //    }
        //    i++;
        //}

        //string answ = "";
        //while (answ != "нет")
        //{
        //    gameUsing(characters);
        //    do
        //    {
        //        Console.WriteLine("\nВернуться к выбору персонажа? (да/нет)\n");
        //        answ = Console.ReadLine();
        //        switch (answ)
        //        {
        //            case "да":
        //                int j = 1;
        //                foreach (Game gamer in characters)
        //                {
        //                    Console.Write($"{j}. {gamer.Name} - ");
        //                    if (gamer.Team == true)
        //                    {
        //                        Console.Write("команда 1");
        //                    }
        //                    else
        //                    {
        //                        Console.Write("команда 2");
        //                    }
        //                    if (alive.Contains(gamer) == true)
        //                    {
        //                        Console.Write(" - жив\n");
        //                    }
        //                    else
        //                    {
        //                        Console.Write(" - мертв\n");
        //                    }
        //                    j++;
        //                }
        //                break;
        //            case "нет":
        //                break;
        //            default:
        //                answ = "";
        //                break;
        //        }
        //    } while (answ != "да" && answ != "нет");
        //}
    }
}