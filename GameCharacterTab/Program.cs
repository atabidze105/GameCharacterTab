using GameCharacterTab;  

class Program
{
    static void Main()
    {

        //Game[] game = new Game[3];

        //game[0] = new Game();
        //game[1] = new Game();
        //game[2] = new Game();

        //game[0].newCharacter("jwhrg", 6, 3, true, 10);
        //game[1].newCharacter("jwhrg", 4, 2, true, 10);
        //game[2].newCharacter("jwhrg", 5, 9, false, 10);


        //Console.WriteLine("Введите границы поля (ось X):");
        //int lengthX = Convert.ToInt32(Console.ReadLine());
        //int[] axisX = new int[lengthX];
        //for (int i = 0; i < axisX.Length; i++) //Цикл для создания элементов массива
        //{
        //    axisX[i] = i+1;
        //}

        //Console.WriteLine("Введите границы поля (ось Y):");
        //int lengthY = Convert.ToInt32(Console.ReadLine());
        //int[] axisY = new int[lengthY];
        //for (int i = 0; i < axisY.Length; i++) //Цикл для создания элементов массива
        //{
        //    axisY[i] = i + 1;
        //}

        //game[1].map(game, axisX, axisY);
        //game[1].movement(axisX, axisY, 2, 8);
        //game[1].map(game, axisX, axisY);

        List<Game> alive = new List<Game>();
        List<Game> dead = new List<Game>();

        void accountChoshing(Game[] gamers, int quantity, List<Game> alive, List<Game> dead) //Метод для выбора из массива объекта для применения метода
        {
            int nom = -1;
            Console.WriteLine("\nНеобходимо выбрать персонажа чтобы продолжить.");
            while (nom > gamers.Length || nom < 0)
            {
                Console.WriteLine($"Введите один из доступных номеров персонажей:\n\nот 1 до {quantity}:\n");
                nom = Convert.ToInt32(Console.ReadLine()); //Выбор индекса элемента массива
                if (nom > 0)
                {
                    if (nom <= gamers.Length)
                    {
                        gamers[nom - 1].Gaming(gamers, alive, dead);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка. Введите значение в заданом диапазоне.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка. Введите значение в заданом диапазоне.\n");
                }
            }
        }

        Console.WriteLine("Чтобы начать работу, необходимо задать количество персонажей.\nВведите количество аккаунтов, которое хотите создать:");
        int quantityOfCharacters = Convert.ToInt32(Console.ReadLine());
        Game[] accountsGamers = new Game[quantityOfCharacters];
        for (int i = 0; i < accountsGamers.Length; i++) //Цикл для создания элементов массива
        {
            accountsGamers[i] = new Game();
        }

        accountChoshing(accountsGamers, quantityOfCharacters, alive, dead); //Первое обращение к методу        

        Console.WriteLine("\nЧтобы вернуться к выбору персонажа, нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".\n");
        string thisAccount = ""; //Переменная для поддержания работы следующего цикла
        thisAccount = Console.ReadLine();

        if (thisAccount == "")
        {
            do
            {
                accountChoshing(accountsGamers, quantityOfCharacters, alive, dead);
                Console.WriteLine("\nЧтобы вернуться к выбору персонажа, нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".\n");
                thisAccount = Console.ReadLine();
            } while (thisAccount == ""); //Метод работает пока строка пуста
        }
    }
}