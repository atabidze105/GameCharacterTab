using GameCharacterTab;  

class Program
{
    static void Main()
    {
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
                        gamers[nom - 1].trueGaming(gamers, alive, dead);
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

        Console.WriteLine("Чтобы начать игру, необходимо задать количество персонажей.\nВНИМАНИЕ: Чтобы игра закончилась естественно, необходимо победить всех созданных персонажей, а чтобы победить их - нужно их создать.\nСоздайте столько персонажей, сколько заявите.\n\nВведите количество персонажей, которое хотите создать:");
        int quantityOfCharacters = Convert.ToInt32(Console.ReadLine());
        Game[] accountsGamers = new Game[quantityOfCharacters];
        for (int i = 0; i < accountsGamers.Length; i++) //Цикл для создания элементов массива
        {
            accountsGamers[i] = new Game();
        }

        accountChoshing(accountsGamers, quantityOfCharacters, alive, dead); //Первое обращение к методу        

        Console.WriteLine("\nЧтобы вернуться к выбору персонажа, нажмите \"Enter\".\nЧтобы выйти из программы напишите что-нибудь и нажмите \"Enter\".\n");
        string thisAccount = ""; //Переменная для поддержания работы следующего цикла
        thisAccount = Console.ReadLine();

        if (thisAccount == "")
        {
            do
            {
                accountChoshing(accountsGamers, quantityOfCharacters, alive, dead);
                Console.WriteLine("\nЧтобы вернуться к выбору персонажа, нажмите \"Enter\".\nЧтобы выйти из программы, напишите что-нибудь и нажмите \"Enter\".\n");
                thisAccount = Console.ReadLine();
            } while (thisAccount == ""); //Метод работает пока строка пуста
        }
    }
}