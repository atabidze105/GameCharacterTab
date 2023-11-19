﻿using System.Collections.Generic;

namespace GameCharacterTab
{
    internal class Game
    {
        private string _name;
        private int _locationX;
        private int _locationY;
        private bool? _friend;
        private int _healpoints;
        private int _maxHP;
        private int _heal;
        private int _strength;
        private int _killCharge;
        private int _killScore;
        private int _healCharge;
        private int _omen;
        private int _end;
        public string Name
        {
            get { return _name; }
        }
        public bool? Team
        {
            get { return _friend; }
        }

        public void newCharacter(string name, int locX, int locY, bool? friend, int health) //Создание персонажа
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
            _strength = random.Next((_maxHP / 2), Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_maxHP) / 3) * 2));
            Console.WriteLine("\nСоздание персонажа завершено.\n");
        }

        private void getInfo() //Вывод иннформации
        {
            Console.WriteLine($"\nИмя персонажа: {_name}.\nМестоположение: X:{_locationX}; Y:{_locationY}.\nКоличество ОЗ: {_healpoints}/{_maxHP}.\nСила: {_strength}.\nВрагов побеждено: {_killScore}.");
            if (_friend == true)
            {
                Console.WriteLine("Член дружественного лагеря.\n");
            }
            else
            {
                Console.WriteLine("Член вражеского лагеря.\n");
            }
        }


        private void battle(List<Game> opponents, List<Game> alive, List<Game> dead) //Большая битва
        {
            int partyDamage = 0;
            int separatedDamage = _strength / opponents.Count; //поделенный урон не используется в битве, исправить
            if (opponents.Count > 1)
            {
                Console.WriteLine("\nОбнаружены враги!\n\nВаши противники: ");

                foreach (Game opponent in opponents)
                {
                    Console.WriteLine($"{opponent._name}");
                    partyDamage += opponent._strength;
                }

            }
            else
            {
                Console.WriteLine("\nОбнаружен враг!");
            }

            string answer = "";

            if (_killCharge >= 2)
            {
                Console.WriteLine("\nВозможно использование ультимативной способности.");
                while (answer == "")
                {
                    Console.WriteLine("Использовать? (да/нет)");
                    answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "да":
                            ultimate(opponents, alive, dead);
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

                while (_healpoints > 0 && opponents.Count > 0)
                {
                    foreach (Game opponent in opponents)
                    {
                        //Здесь начинается сражение двух персонажей - применившего метод и объекта из списка врагов
                        if (_healpoints > 0)
                        {
                            while (_healpoints > 0 && opponent._healpoints > 0)
                            {
                                Console.WriteLine($"Игроки {_name} и {opponent._name} обмениваются ударами!\n");
                                _healpoints -= opponent._strength;
                                opponent._healpoints -= separatedDamage;
                            }
                            if (_healpoints <= 0 && opponent._healpoints <= 0)
                            {
                                Console.WriteLine($"Игроки {_name} и {opponent._name} убили друг друга!\n");
                                death(alive, dead);
                                _killCharge++;
                                _killScore++;
                                _healCharge++;
                                opponent.death(alive, dead);
                                opponent._killCharge++;
                                opponent._healCharge++;
                                opponent._killScore++;
                            }
                            else
                            {
                                if (opponent._healpoints <= 0)
                                {
                                    Console.WriteLine($"Игрок {_name} убил игрока {opponent._name}!\n");
                                    opponent.death(alive, dead);
                                    _killCharge++;
                                    _killScore++;
                                    _healCharge++;
                                }
                                else
                                {
                                    if (_healpoints <= 0)
                                    {
                                        Console.WriteLine($"Игрок {opponent._name} убил игрока {_name}!\n");
                                        death(alive, dead);
                                        opponent._killCharge++;
                                        opponent._killScore++;
                                        opponent._healCharge++;
                                    }

                                }
                            }

                            if (opponent._healpoints <= 0)
                            {
                                opponent._omen = 1;
                            }
                        }
                    }

                    for (int i = 0; i < opponents.Count; i++)
                    {
                        if (opponents[i]._omen == 1)
                        {
                            opponents.Remove(opponents[i]);
                        }
                    }

                }

                Console.WriteLine("\nБитва окончена.");

                if (dead.Contains(this) == true && opponents.Count == 0)
                {
                    Console.WriteLine("Все воины погибли.\n");
                }
                else
                {
                    if (opponents.Count == 0)
                    {
                        Console.WriteLine("\nОтряд противников повержен.\n");
                    }
                    else
                    {
                        if (dead.Contains(this) == true)
                        {
                            Console.WriteLine($"\nПерсонаж {_name} погиб.\n");
                        }
                    }
                }
            }
        }

        private void win(List<Game> alive, List<Game> dead)
        {
            if (alive.Count(c => c._friend == true) == 0 && alive.Count(c => c._friend == false) == 0)
            {
                _end = 1;
            }
            else
            {
                if (alive.Count(c => c._friend == true) > 0 && alive.Count(c => c._friend == false) == 0)
                {
                    _end = 2;
                }
                else
                {
                    _end = 3;
                }
            }
        }

        private void ultimate(List<Game> opponents, List<Game> alive, List<Game> dead) //ульта
        {
            Console.WriteLine("Использована ультимативная способность.\nПоверженные враги:");
            foreach (Game opponent in opponents)
            {
                _killCharge -= 2;
                opponent._healpoints = 0;
                opponent.death(alive, dead);
                Console.WriteLine(opponent._name);
                _killScore++;
                _healCharge++;
            }
        }

        private void healingBurst(Game gamer, List<Game> alive) //Полное исцеление
        {
            if (gamer != null)
            {
                if (gamer._friend == _friend && gamer._healpoints < gamer._maxHP && alive.Contains(gamer) == true && gamer != this && _healCharge <= 5)
                {
                    gamer._healpoints = _maxHP;
                    _healCharge -= 5;
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

        private void death(List<Game> alive, List<Game> dead) //Смерть персонажа
        {
            alive.Remove(this);
            dead.Add(this);
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

        //private void characterChosing

        public void charCreation(List<Game> gamers, List<Game> alive, List<Game> dead, bool team) //Создание персонажа
        {
            while (_name == "" || _friend == null || _locationX == null || _locationY == null || _maxHP <= 0)
            {
                Console.WriteLine("Необходимо создать персонажа, чтобы продолжить.\nВведите имя (должно быть уникальным):");
                string nameChar = Console.ReadLine();
                Console.WriteLine("Введите количетсво ОЗ (очков здоровья) персонажа:");
                int hpChar = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Задайте местоположение вашего персонажа.\nВведите X:");
                int XChar = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите Y:");
                int YChar = Convert.ToInt32(Console.ReadLine());

                foreach (Game gamer in gamers)
                {
                    if (nameChar == gamer._name)
                    {
                        nameChar = "";
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
                        if (searchingByXY(gamers, XChar, YChar)._friend != team)
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

        //public Game prepareForGaming(List<Game> gamers, List<Game> alive)
        //{
        //    string answer = "";
        //    bool team = true;
        //    do
        //    {
        //        Console.WriteLine("\nВыберите команду, за которую хотите играть:\nКоманда 1 - 1\nКоманда 2 - 2");
        //        answer = Console.ReadLine();
        //        switch (answer)
        //        {
        //            case "1":
        //                getTeamInfo(gamers, alive, true);
        //                team = true;
        //                break;
        //            case "2":
        //                getTeamInfo(gamers, alive, false);
        //                team = false;
        //                break;
        //            default:
        //                answer = "";
        //                break;
        //        }
        //    } while (answer != "1" && answer != "2");
        //    Console.WriteLine("Введите имя персонажа, за которого хотите играть:");
        //    string name = "";
        //    while (name == "")
        //    {
        //        name = Console.ReadLine();
        //        if (searchingByName(gamers, name) != null)
        //        {
        //            if (searchingByName(gamers, name)._friend != team)
        //            {
        //                Console.WriteLine("Невозможно выбрать члена вражеской команды. Чтобы играть за членов другой команды, сперва выберите другую команду.");
        //                answer = "";
        //                return null;
        //            }
        //            else
        //            {
        //                return searchingByName(gamers, name);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Некорректный ввод имени.");
        //            answer = "";
        //            return null;
        //        }
        //    }
        //    return null;
        //}


        public void Gaming(List<Game> gamers, List<Game> alive, List<Game> dead)
        {
            Console.WriteLine($"\nМеню персонажа {_name}.\nВыберите действия:\n");
            string continuation = "";

            do
            {
                if (alive.Contains(this) == true)
                {
                    Console.WriteLine(" Вывод информации - 1\n Передвижение - 2\n Исцеление союзника - 3\n Полное исцеление союзника - 4\n Досрочное завершение игры - 5\n");
                    string choise = Console.ReadLine();
                    switch (choise)
                    {
                        case "1":
                            getInfo();
                            break;
                        case "2":
                            Console.WriteLine("\nЗадайте новое местоположение вашего персонажа.\nВведите X:");
                            int Xmove = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите Y:");
                            int Ymove = Convert.ToInt32(Console.ReadLine());
                            moveX(Xmove);
                            moveY(Ymove);
                            if (searchingByXY(gamers, _locationX, _locationY) == null || searchingByXY(gamers, _locationX, _locationY) == this || searchingByXY(gamers, _locationX, _locationY)._friend == _friend)
                            {
                                Console.WriteLine("\nНет врагов поблизости.\n");
                            }
                            else
                            {
                                List<Game> opponents = new List<Game>();

                                foreach (Game opponent in alive)
                                {
                                    if (opponent._locationX == _locationX && opponent._locationY == _locationY && opponent._friend != _friend && opponent != this)
                                    {
                                        opponents.Add(opponent);
                                    }
                                }

                                battle(opponents, alive, dead);
                                opponents.Clear();
                            }
                            break;
                        case "3":
                            Console.WriteLine("\nВведите имя союзника, которому хотите отдать свои ОЗ:");
                            string nameFriend = Console.ReadLine();
                            healing(searchingByName(gamers, nameFriend), alive);
                            break;
                        case "4":
                            Console.WriteLine("\nВведите имя союзника, которого хотите полностью иссцелить:");
                            string nameFriend2 = Console.ReadLine();
                            healingBurst(searchingByName(gamers, nameFriend2), alive);
                            break;
                        case "5":
                            Console.WriteLine("\nВы уверены, что хотите завершить игру? (да/нет)\n");
                            string answer = Console.ReadLine();
                            if (answer == "да")
                            {
                                _end = 4;
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

                    Console.WriteLine("\nЧтобы продолжить, нажмите \"Enter\".\nЧтобы выйти в меню выбора персонажа, напишите что-нибудь и нажмите \"Enter\".\n");
                    continuation = Console.ReadLine();

                }
                else
                {
                    Console.WriteLine($"\nПерсонаж {_name} погиб.\n");
                    Console.WriteLine("\nЧтобы продолжить, нажмите \"Enter\".\nЧтобы выйти в меню выбора персонажа, напишите что-нибудь и нажмите \"Enter\".\n");
                    continuation = Console.ReadLine();
                }

            } while (continuation == "");
        }


        public void trueGaming(List<Game> gamers, List<Game> alive, List<Game> dead)
        {
            switch (_end)
            {
                case 1:
                    Console.WriteLine("Игра окончена.\nВсе погибли");
                    break;
                case 2:
                    Console.WriteLine("Игра окончена.\nПобедила команда 1");
                    break;
                case 3:
                    Console.WriteLine("Игра окончена.\nПобедила команда 2");
                    break;
                case 4:
                    Console.WriteLine("Игра окончена.");
                    break;
                default:
                    Gaming(gamers, alive, dead);
                    break;


            }
        }
    }

}