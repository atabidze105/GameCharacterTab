using System.Collections.Generic;

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
        private int _killList;
        private int _killScore;
        private int _healCharge;
        private int _choise;

        
        




        public void newCharacter(string name, int locX, int locY, bool? friend, int health) //Создание персонажа
        {            
            _name = name;
            _locationX = locX;
            _locationY = locY;
            _friend = friend;
            _healpoints = _maxHP = health;
            _killList = 0;
            _healCharge = 0;
            Random random = new Random();
            _strength = random.Next((_maxHP/2), (_maxHP/3)*2);
            Console.WriteLine("\nСоздание персонажа завершено.\n");
        }

        private void getInfo() //Вывод иннформации
        {
            Console.WriteLine($"\nИмя персонажа: {_name}.\nМестоположение: X:{_locationX}; Y:{_locationY}.\nКоличество ОЗ: {_healpoints}/{_maxHP}.\nСила: {_strength}.\nВрагов побеждено: {_killList}.");
            if (_friend == true)
            {
                Console.WriteLine("Член дружественного лагеря.\n");
            }
            else
            {
                Console.WriteLine("Член вражеского лагеря.\n");
            }
        }

        private void battle(Game gamer, List<Game> alive, List<Game> dead) //Битва двух персонажей
        {
            Console.WriteLine($"Битва начинается!\n");
            while (_healpoints > 0 && gamer._healpoints > 0) 
            {
                _healpoints -= gamer._strength;
                gamer._healpoints -= _strength;
            }
            if (_healpoints <= 0 && gamer._healpoints <= 0)
            {
                Console.WriteLine($"Игроки {_name} и {gamer._name} убили друг друга!\n");
                death(alive, dead);
                _killList++;
                _killScore++;
                _healCharge++;
                gamer.death(alive, dead);
                gamer._killList++;
                gamer._healCharge++;
                gamer._killScore++;
            }
            else
            {
                if (gamer._healpoints <= 0) 
                {
                    Console.WriteLine($"Игрок {_name} убил игрока {gamer._name}!\n");
                    gamer.death(alive, dead);
                    _killList++;
                    _killScore++;
                    _healCharge++;
                }
                else
                {
                    if (_healpoints <= 0)
                    {
                        Console.WriteLine($"Игрок {gamer._name} убил игрока {_name}!\n");
                        death(alive, dead);
                        gamer._killList++;
                        gamer._killScore++;
                        gamer._healCharge++;
                    }
                        
                }
            }
        }

        private void healingBurst(Game gamer, List<Game> alive) //Полное исцеление
        {
            if (gamer != null)
            {
                if (gamer._friend == _friend || alive.Contains(gamer) == true || gamer != this || _healCharge <= 5)
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
                if (_healpoints > _heal || gamer._friend == _friend || alive.Contains(gamer) == true || gamer != this) 
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

        private Game searchingByXY(Game[] characters, int X, int Y) //Поиск персонажа по его местоположению
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

        private Game searchingByName(Game[] gamers, string name)
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

        public void Gaming(Game[] gamers, List<Game> alive, List<Game> dead)
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
                Console.WriteLine("Выберите принадлежность к лагерю.\nЛагерь 1 - 1\nЛагерь 2 - 2");
                int campChar = Convert.ToInt32(Console.ReadLine());
                switch (campChar)
                {
                    case 1:
                        _friend = true;
                        break;
                    case 2:
                        _friend = false;
                        break;
                    default:
                        _friend = null;
                        break;
                }
                foreach (Game gamer in gamers)
                {
                    if (nameChar == gamer._name)
                    {
                        nameChar = "";
                    }
                }
                if (hpChar <= 0 || _friend == null || nameChar == "" || nameChar == null)
                {
                    Console.WriteLine("Значения введены некорректно. Попробуйте снова.");
                }
                else
                {
                    newCharacter(nameChar, XChar, YChar, _friend, hpChar);
                    alive.Add(this);
                }
            }

            Console.WriteLine($"Меню персонажа {_name}.\nВыберите действия:\n\n");
            string continuation;
            
            do
            {
                Console.WriteLine(" Вывод информации - 1\n Передвижение - 2\n Исцеление союзника - 3\n Полное исцеление союзника - 4\n Битва с противником - 5");
                _choise = Convert.ToInt32(Console.ReadLine());
                switch (_choise)
                {
                    case 1:
                        getInfo(); 
                        break;
                    case 2:
                        Console.WriteLine("Задайте новое местоположение вашего персонажа.\nВведите X:");
                        int Xmove = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите Y:");
                        int Ymove = Convert.ToInt32(Console.ReadLine());
                        moveX(Xmove);
                        moveY(Ymove);
                        break;
                    case 3:
                        Console.WriteLine("Введите имя союзника, которому хотите отдать свои ОЗ:");
                        string nameFriend = Console.ReadLine();
                        healing(searchingByName(gamers, nameFriend), alive);
                        break;
                    case 4:
                        Console.WriteLine("Введите имя союзника, которого хотите полностью иссцелить:");
                        string nameFriend2 = Console.ReadLine();
                        healingBurst(searchingByName(gamers, nameFriend2), alive);
                        break;
                    case 5:
                        if (searchingByXY(gamers, _locationX, _locationY) == null || searchingByXY(gamers, _locationX, _locationY) == this)
                        {
                            Console.WriteLine("Нет врагов поблизости.");
                        }
                        else 
                        {
                            battle(searchingByXY(gamers, _locationX, _locationY), alive, dead);
                        }
                        break;
                    default:
                        Console.WriteLine("Введено некорректное значение.");
                        break;
                }

                Console.WriteLine("Чтобы продолжить, нажмите \"Enter\".\nЧтобы выйти, напишите что-нибудь и нажмите \"Enter\".");
                continuation = Console.ReadLine();
            } while (continuation == "");
        }
    }
}
