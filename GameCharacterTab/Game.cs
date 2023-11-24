using System.Collections.Generic;

namespace GameCharacterTab
{
    internal class Game
    {
        private string _name; //Имя персонажа
        private int _locationX; //Координата X
        private int _locationY; //Координата Y
        private bool? _friend; //Принадлежность к лагерю
        private int _healpoints; //Кол-во ОЗ (очков здоровья)
        private int _maxHP; //Макс. кол-во ОЗ
        private int _heal; //Получаемое лечение
        private int _strength; //Сила (урон)
        private int _killCharge; //Заряд для ультимативной способности
        private int _killScore; //Кол-во убийств
        private int _healCharge; //Заряд для полного исцеления
        private int _omen; //метка для удаления объектов из списка врагов
        private int _end; //метка для завершения игры

        private void newCharacter(string name, int locX, int locY, bool? friend, int health) //Создание персонажа
        {
            _name = name;
            _locationX = locX;
            _locationY = locY;
            _friend = friend;
            _healpoints = _maxHP = health;
            _killCharge = 0;
            _healCharge = 0;
            _omen = 0;
            Random random = new Random();
            _strength = random.Next(_maxHP / 2, _maxHP);
            Console.WriteLine("\nСоздание персонажа завершено.\n");
        }

        private void getInfo() //Вывод иннформации
        {
            Console.WriteLine($"\nИмя персонажа: {_name}.\nМестоположение: X:{_locationX}; Y:{_locationY}.\nКоличество ОЗ: {_healpoints}/{_maxHP}.\nСила: {_strength}.\nВрагов побеждено: {_killScore}.");
            if (_friend == true)
            {
                Console.WriteLine("Член команды 1.\n");
            }
            else
            {
                Console.WriteLine("Член команды 2.\n");
            }
        }


        private void battle(List<Game> opponents, List<Game> alive) //Большая битва
        {
            int partyDamage = 0;
            int separatedDamage = _strength / opponents.Count; //Урон игрока делится на кол-во противников
            if (opponents.Count > 1)
            {
                Console.WriteLine("\nОбнаружены враги!\n\nВаши противники: ");

                foreach (Game opponent in opponents) //Вывод списка противников
                {
                    Console.WriteLine($"{opponent._name}");
                    partyDamage += opponent._strength; //общий урон отряда противников
                }

            }
            else
            {
                Console.WriteLine("\nОбнаружен враг!");
                foreach (Game opponent in opponents)
                {
                    partyDamage += opponent._strength; //общий урон отряда противников
                }
            }

            string answer = "";

            if (_killCharge >= 5)//Если до этой битвы убито 5 противников, появляется возможность примненить ульту
            {
                Console.WriteLine("\nВозможно использование ультимативной способности.");
                while (answer == "")
                {
                    Console.WriteLine("Использовать? (да/нет)");
                    answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "да":
                            ultimate(opponents, alive);
                            Console.WriteLine("Битва окончена.");
                            break;
                        case "нет":
                            break;
                        default:
                            answer = "";
                            break;
                    }
                }
            }
            
            if (answer == "нет" || answer == "")
            {
                Console.WriteLine("\nБитва начинается!\n");

                while (_healpoints > 0 && opponents.Count > 0) //Битва идет пока мы живы и противники есть
                {
                    if (opponents.Count > 1) //Если враг был или остается один, то выводится его имя. Иначе пишет что нас бьет отряд
                    {
                        Console.WriteLine("Отряд врагов наносит удар!\n");
                    }
                    else
                    {
                        Console.WriteLine($"{opponents[0]._name} наносит удар!\n");                        
                    }

                    _healpoints -= partyDamage; //Враги первыми наносят урон

                    Console.WriteLine($"{_name} наносит ответный удар!\n");

                    foreach (Game opponent in opponents) //Мы бьем каждого из списка врагов
                    {
                        opponent._healpoints -= separatedDamage;
                        if (opponent._healpoints <= 0) //проверка на наличие мертвых врагов
                        {
                            opponent._omen = 1; //метка для удаления объекта с отрицательными или нулевыми ОЗ из списка врагов
                            partyDamage -= opponent._strength; //уменьшение общего урона на значение силы убитого
                        }
                    }

                    if (_healpoints <= 0) //Если мы получили фатальный урон, то всем противникам дают очки, а нас убивает
                    {
                        death(alive);
                        foreach (var opponent in opponents)
                        {
                            opponent._killScore++;
                            opponent._killCharge++;
                            opponent._healCharge++;
                        }
                    }

                    foreach (Game opponent in alive) //перебираеьтся сприсок живых, противники с меткой удаляются, а ам за это дают очки
                    {
                        if (opponent._omen == 1)
                        {
                            Console.WriteLine($"{opponent._name} повержен.\n");
                            opponents.Remove(opponent);
                            _killScore++;
                            _killCharge++;
                            _healCharge++;
                        }
                    }

                    separatedDamage = opponents.Count > 0 ? _strength / opponents.Count : separatedDamage; //Пересчет урона отряда (если протиивников больше 0)
                }

                Console.WriteLine("\nБитва окончена.\n");

                if (alive.Contains(this) == false && opponents.Count == 0)
                {
                    Console.WriteLine("Все воины погибли.\n");
                }
                else
                {
                    if (opponents.Count == 0)
                    {
                        Console.WriteLine("Отряд противников повержен.\n");
                    }
                    else
                    {
                        if (alive.Contains(this) == false)
                        {
                            Console.WriteLine($"Персонаж {_name} погиб.\n");
                            foreach (var opponent in opponents) 
                            {
                                opponent._killScore++;
                                opponent._killCharge++;
                                opponent._healCharge++;
                            }
                        }
                    }
                }
            }
        }

        private void win(List<Game> alive) //Проверка условий завершения игры
        {
            if (alive.Count(c => c._friend == true) == 0 && alive.Count(c => c._friend == false) == 0) //Если все члены обеих команд погибли
            {
                _end = 1;
            }
            else
            {
                if (alive.Count(c => c._friend == true) > 0 && alive.Count(c => c._friend == false) == 0) //если в 1 команде остались живые
                {
                    _end = 2;
                }
                else
                {
                    if (alive.Count(c => c._friend == true) == 0 && alive.Count(c => c._friend == false) > 0) //если во 2 команде остались живые
                    {
                        _end = 3;
                    }
                }
            }
        }

        private void win2(List<Game> gamers, List<Game> alive) //Объявление победы
        {
            switch (_end)
            {
                case 1:
                    Console.WriteLine("Игра окончена.\nВсе погибли");
                    statistics(gamers, alive);
                    break;
                case 2:
                    Console.WriteLine("Игра окончена.\nПобедила команда 1");
                    statistics(gamers, alive);
                    break;
                case 3:
                    Console.WriteLine("Игра окончена.\nПобедила команда 2");
                    statistics(gamers, alive);
                    break;
                case 4:
                    Console.WriteLine("Игра окончена.");
                    statistics(gamers, alive);
                    break;
            }
        }

        private void ultimate(List<Game> opponents, List<Game> alive) //ульта
        {
            Console.WriteLine("Использована ультимативная способность.\nПоверженные враги:");
            foreach (Game opponent in opponents)
            {
                _killCharge -= 5;
                opponent._healpoints = 0;
                opponent.death(alive);
                Console.WriteLine(opponent._name);
                _killScore++;
                _healCharge++;
            }
        }

        private void healingBurst(Game gamer, List<Game> alive) //Полное исцеление
        {
            if (gamer != null)
            {
                if (gamer._friend == _friend && gamer._healpoints < gamer._maxHP && alive.Contains(gamer) == true && gamer != this && _healCharge <= 3)
                {
                    gamer._healpoints = _maxHP;
                    _healCharge -= 3;
                    Console.WriteLine($"\nИгрок {gamer._name} полностью исцелен.");
                }
                else
                {
                    Console.WriteLine("\nНевозможно выполнить лечение.");
                }
            }
            else
            {
                Console.WriteLine("Адресат лечения задан некорректно.");
            }
        }

        private void healing(Game gamer, List<Game> alive) //Лечение персонажа
        {
            if (gamer != null)
            {
                Console.WriteLine($"Введите количетсво своих ОЗ, которые хотите отдать союзнику {gamer._name}:");
                _heal = Convert.ToInt32(Console.ReadLine());
                if (_heal > 0 && _healpoints > _heal && gamer._friend == _friend && alive.Contains(gamer) == true && gamer != this && gamer._maxHP - gamer._healpoints >= _heal)
                {
                    gamer._healpoints += _heal;
                    _healpoints -= _heal;
                    Console.WriteLine($"\nИгрок {gamer._name} исцелен на {_heal} ОЗ.\nТекущее кол-во ОЗ {gamer._name}: {gamer._healpoints}.\nТекущее кол-во ОЗ: {_healpoints}.");
                }
                else
                {
                    Console.WriteLine("\nНевозможно выполнить лечение.");
                }
            }
            else
            {
                Console.WriteLine("Адресат лечения задан некорректно.");
            }
        }

        private void death(List<Game> alive) //Смерть персонажа
        {
            alive.Remove(this);
        }

        private void moveX(int x)//перемещение по x
        {
            _locationX = x;
        }

        private void moveY(int y)//перемещение по y
        {
            _locationY = y;
        }

        private Game searchingByXY(List<Game> characters, int X, int Y) //Поиск персонажа по его местоположению
        {
            foreach (Game gamer in characters) //Перебор объектов в массиве
            {
                if (gamer._locationX == X && gamer._locationY == Y && gamer != this) //Проверка элементов массива на соответствие искомомым координатам
                {
                    return gamer; //Возврат искомого элемента массива
                }
            }
            return null;    //Если объект не был найден, вернется пустое значение
        }

        private Game searchingByName(List<Game> gamers, string name) //поиск по имени
        {
            foreach (Game gamer in gamers)
            {
                if (name == gamer._name)
                {
                    return gamer;
                }
            }
            return null;
        }

        private void charCreation(List<Game> gamers, List<Game> alive, bool team) //Создание персонажа
        {
            while (_name == "" || _friend == null || _locationX == null || _locationY == null || _maxHP <= 0)
            {
                Console.WriteLine("\nНеобходимо создать персонажа, чтобы продолжить.\nВведите имя (должно быть уникальным):");
                string nameChar = Console.ReadLine();
                Console.WriteLine("Введите количетсво ОЗ (очков здоровья) персонажа:");
                int hpChar = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Задайте местоположение вашего персонажа.\nВведите X:");
                int XChar = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите Y:");
                int YChar = Convert.ToInt32(Console.ReadLine());

                foreach (Game gamer in gamers) //Ищет объект с таким же именем
                {
                    if (nameChar == gamer._name)
                    {
                        nameChar = "";
                        Console.WriteLine("Персонаж с таким именем уже существует.");
                    }
                }

                if (hpChar <= 0 || nameChar == "" || nameChar == null)
                {
                    Console.WriteLine("Какие-то из значений введены некорректно. Попробуйте снова.\n");
                }
                else
                {
                    if (searchingByXY(gamers, XChar, YChar) != null)
                    {
                        if (searchingByXY(gamers, XChar, YChar)._friend != team) //Не позволяет разместить персонажа там, где враги
                        {
                            Console.WriteLine("Введенные координаты занял враг. Разместите своего персонажа в другом месте.\n");
                        }
                        else
                        {
                            newCharacter(nameChar, XChar, YChar, team, hpChar);
                            alive.Add(this);
                        }
                    }
                    else
                    {
                        newCharacter(nameChar, XChar, YChar, team, hpChar);
                        alive.Add(this);
                    }
                }
            }
        }

        private void teamBuild(List<Game> characters, List<Game> alive, bool team) //Применение метода создания персонажей 
        {
            Console.Write("\nСоздайте игроков для ");
            if (team == true) 
            {
                Console.Write("первой команды\n");
            }
            else
            {
                Console.Write("второй команды\n");
            }

            string answer = "";
            while (answer != "нет")
            {
                Game character = new Game();
                character.charCreation(characters, alive, team);
                characters.Add(character);

                do
                {
                    Console.WriteLine("\nПродолжить? (да/нет)\n");
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
        }

        private void charList(List<Game> characters, List<Game> alive) //  Вывод всех персонажей с нумерацией
        {
            Console.WriteLine();
            int i = 1;
            foreach (Game gamer in characters)
            {
                Console.Write($"{i}. {gamer._name} - ");
                if (gamer._friend == true)
                {
                    Console.Write("команда 1");
                }
                else
                {
                    Console.Write("команда 2");
                }
                if (alive.Contains(gamer) == true)
                {
                    Console.Write(" - жив\n");
                }
                else
                {
                    Console.Write(" - мертв\n");
                }
                i++;
            }
        }

        private void statistics(List<Game> gamers, List<Game> alive) //Вывод статистики в конце игры
        {
            Console.WriteLine("\nКоманда 1:");
            int i = 1;
            foreach (Game gamer in gamers)
            {
                if (gamer._friend == true)
                {
                    Console.Write($"{i}. {gamer._name}");
                    if (alive.Contains(gamer) == true)
                    {
                        Console.Write(" - жив");
                    }
                    else
                    {
                        Console.Write(" - мертв");
                    }
                    Console.Write($" - Убито: {gamer._killScore}\n");
                    i++;
                }
            }

            Console.WriteLine("\nКоманда 2:");
            i = 1;
            foreach (Game gamer in gamers)
            {
                if (gamer._friend == false)
                {
                    Console.Write($"{i}. {gamer._name}");
                    if (alive.Contains(gamer) == true)
                    {
                        Console.Write(" - жив");
                    }
                    else
                    {
                        Console.Write(" - мертв");
                    }
                    Console.Write($" - Убито: {gamer._killScore}\n");
                    i++;
                }
            }
        }

        public void Playing(List<Game> gamers, List<Game> alive) //Метод для вызова общего метода
        {
            GAME(gamers, alive);
        }

        private void GAME(List<Game> gamers, List<Game> alive) //Общий метод
        {
            Console.WriteLine("Добро пожаловать в Игру.\nПРАВИЛА:\nУничтожьте всех членов вражеской команды до того, как они уничтожат вас. Поддерживайте союзников лечением, группируйтесь в отряды и устраивайте засады.\nПолное исцеление: 3 очка;\nУльтимативная способность: 5 очков.");
            teamBuild(gamers, alive, true); //Создание персонажей 1 команды
            teamBuild(gamers, alive, false);//Создание персонажей 2 команды
            Console.WriteLine("\nСоздание персонажей завершено.\nВыберите персонажа, за которого хотите играть:\n");
            charList(gamers, alive);//Вывод списка персонажей
            string answ = "да"; //значение пропускает первый цикл в следующем цикле
            string numb = "";
            while (answ != "нет")
            {
                while (answ == "")
                {
                    Console.WriteLine("\nВернуться к выбору персонажа? (да/нет)\n");
                    answ = Console.ReadLine();
                    switch (answ)
                    {
                        case "да":
                            charList(gamers, alive);
                            numb = "";
                            break;
                        case "нет":
                            numb = " "; 
                            break;
                        default:
                            answ = "";
                            break;
                    }
                }
                
                while (numb == "")
                {
                    Console.WriteLine("\nВведите номер персонажа:\n");
                    numb = Console.ReadLine();
                    if (Convert.ToInt32(numb) > 0 && Convert.ToInt32(numb) <= gamers.Count()) //Проверка вхождения введенного номера в установленные рамки (больше нуля и меньше вол-ва объектов в списке)
                    {
                        Console.WriteLine($"\nМеню персонажа {gamers[Convert.ToInt32(numb) - 1]._name}.\nВыберите действия:\n");//процесс игры
                        string continuation = "";

                        while (continuation == "")
                        {
                            if (alive.Contains(gamers[Convert.ToInt32(numb) - 1]) == true) //если жив то можно играть
                            {
                                Console.WriteLine(" Вывод информации - 1\n Передвижение - 2\n Исцеление союзника - 3\n Полное исцеление союзника - 4\n Досрочное завершение игры - 5\n");
                                string choise = Console.ReadLine();
                                switch (choise)
                                {
                                    case "1":
                                        gamers[Convert.ToInt32(numb) - 1].getInfo();
                                        break;
                                    case "2":
                                        Console.WriteLine("\nЗадайте новое местоположение вашего персонажа.\nВведите X:");
                                        int Xmove = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Введите Y:");
                                        int Ymove = Convert.ToInt32(Console.ReadLine());
                                        gamers[Convert.ToInt32(numb) - 1].moveX(Xmove);
                                        gamers[Convert.ToInt32(numb) - 1].moveY(Ymove);
                                        if (gamers[Convert.ToInt32(numb) - 1].searchingByXY(gamers, gamers[Convert.ToInt32(numb) - 1]._locationX, gamers[Convert.ToInt32(numb) - 1]._locationY) != null && gamers[Convert.ToInt32(numb) - 1].searchingByXY(gamers, gamers[Convert.ToInt32(numb) - 1]._locationX, gamers[Convert.ToInt32(numb) - 1]._locationY) != this && gamers[Convert.ToInt32(numb) - 1].searchingByXY(gamers, gamers[Convert.ToInt32(numb) - 1]._locationX, gamers[Convert.ToInt32(numb) - 1]._locationY)._friend != gamers[Convert.ToInt32(numb) - 1]._friend)
                                        {
                                            List<Game> opponents = new();

                                            foreach (Game opponent in alive)
                                            {
                                                if (opponent._locationX == gamers[Convert.ToInt32(numb) - 1]._locationX && opponent._locationY == gamers[Convert.ToInt32(numb) - 1]._locationY && opponent._friend != gamers[Convert.ToInt32(numb) - 1]._friend && opponent != gamers[Convert.ToInt32(numb) - 1])
                                                {
                                                    opponents.Add(opponent);
                                                }
                                            }

                                            gamers[Convert.ToInt32(numb) - 1].battle(opponents, alive);

                                            for (int i = 0; i < gamers.Count; i++) //дополнительная проверка на наличие метки для удаления из списка живых
                                            {
                                                if (gamers[i]._omen == 1 || gamers[i]._healpoints <= 0)
                                                {
                                                    gamers[i].death(alive);
                                                }
                                            }

                                            gamers[Convert.ToInt32(numb) - 1].win(alive);
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nНет врагов поблизости.\n");
                                        }
                                        break;
                                    case "3":
                                        Console.WriteLine("\nВведите имя союзника, которому хотите отдать свои ОЗ:");
                                        string nameFriend = Console.ReadLine();
                                        gamers[Convert.ToInt32(numb) - 1].healing(gamers[Convert.ToInt32(numb) - 1].searchingByName(gamers, nameFriend), alive);
                                        break;
                                    case "4":
                                        Console.WriteLine("\nВведите имя союзника, которого хотите полностью иссцелить:");
                                        string nameFriend2 = Console.ReadLine();
                                        gamers[Convert.ToInt32(numb) - 1].healingBurst(gamers[Convert.ToInt32(numb) - 1].searchingByName(gamers, nameFriend2), alive);
                                        break;
                                    case "5":
                                        Console.WriteLine("\nВы уверены, что хотите завершить игру? (да/нет)\n");
                                        string answer = Console.ReadLine();
                                        if (answer == "да")
                                        {
                                            gamers[Convert.ToInt32(numb) - 1]._end = 4;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nОтмена.\n");
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("\nВведено некорректное значение.\n");
                                        break;
                                }

                                if (gamers[Convert.ToInt32(numb) - 1]._end == 0)
                                {
                                    Console.WriteLine("\nЧтобы продолжить, нажмите \"Enter\".\nЧтобы выйти в меню выбора персонажа, напишите что-нибудь и нажмите \"Enter\".\n");
                                    continuation = Console.ReadLine();
                                    answ = "";
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\nПерсонаж {gamers[Convert.ToInt32(numb) - 1]._name} погиб.\n\nЧтобы продолжить, нажмите \"Enter\".\nЧтобы выйти в меню выбора персонажа, напишите что-нибудь и нажмите \"Enter\".\n");
                                continuation = Console.ReadLine();
                                answ = "";
                            }
                        }
                        if (gamers[Convert.ToInt32(numb) - 1]._end != 0)
                        {
                            gamers[Convert.ToInt32(numb) - 1].win2(gamers, alive);
                            answ = "нет";
                            numb = " ";
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nВыбран несуществующий номер персонажа.\n");
                        numb = "";
                    }
                }
            }
        }
    }
}