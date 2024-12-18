using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shop
{
    internal class Menu
    {
        private int currentSelection = 0;
        private Dictionary<string, string> users = new Dictionary<string, string>();
        private string FilePath = "./Users.json";



        public string[] menuItems = new string[]
        {
            "Регистрация",
            "Вход",
            "Выход"
        };

        public void Start()
        {
            Action();
        }


        public void Action()
        {
            while (true)
            {
                DrawMenu(currentSelection);
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentSelection--;

                        if (currentSelection < 0)
                            currentSelection = menuItems.Length - 1;

                        break;

                    case ConsoleKey.DownArrow:
                        currentSelection++;

                        if (currentSelection >= menuItems.Length)
                            currentSelection = 0;

                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        ExecuteAction();

                        return;

                    default:
                        break;
                }
            }
        }

        public void DrawMenu(int selectIndex)
        {
            Console.Clear();

            for (int i = 0; i < menuItems.Length; i++)
            {
                string? menuItem = menuItems[i];

                if (selectIndex == i)
                {
                    Console.Write("~");
                    Console.WriteLine(menuItem);
                }
                else
                {
                    Console.WriteLine(menuItem);
                }
            }
        }

        private void ExecuteAction()
        {
            switch (currentSelection)
            {
                case 0:
                    string newLogin;
                    string newPassword;

                    Console.WriteLine("Придумайте логин");
                    newLogin = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(newLogin))
                    {
                        Console.WriteLine("Логин не может быть пустым.");
                        Console.WriteLine("Придумайте логин: ");
                        newLogin = Console.ReadLine();
                    }

                    Console.WriteLine("Придумайте пароль");
                    newPassword = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(newPassword))
                    {
                        Console.WriteLine("Пароль не может быть пустым.");
                        Console.WriteLine("Придумайте пароль: ");
                        newPassword = Console.ReadLine();
                    }


                    InitUsers(newLogin, newPassword);

                    break;

                case 1:
                    string login;
                    string password;

                    Console.WriteLine("Введите логин");
                    login = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(login))
                    {
                        Console.WriteLine("Логин не может быть пустым.");
                        Console.WriteLine("Введите логин: ");
                        login = Console.ReadLine();
                    }

                    Console.WriteLine("Введите пароль");
                    password = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("Пароль не может быть пустым.");
                        Console.WriteLine("Ведите пароль: ");
                        password = Console.ReadLine();
                    }

                    FindUsers(login, password);

                    break;


                case 2:
                    Console.WriteLine("Вы вышли!");

                    break;

                default:
                    break;
            }
        }

        public void InitUsers(string login, string password)
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                users = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }

            users.Add(login, password);

            string result = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FilePath, result);

        }

        public void FindUsers(string login, string password)
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                users = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                if (users.TryGetValue(login, out string storedPassword))
                {
                    if (password == storedPassword)
                    {
                        Console.WriteLine("Вы вошли!");
                    }
                    else
                    {
                        Console.WriteLine("Неверный пароль!");
                    }
                }
                else
                {
                    Console.WriteLine("Пользователь не найден!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}