using System.Text.Json;

namespace Shop
{
    internal class UserManager
    {
        private Dictionary<string, string> users = new Dictionary<string, string>();
        private string FilePath = "./Users.json";

        public UserManager() 
        {
            LoadUsers();
        }

        public void Register()
        {
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
        }

        public void Login()
        {
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
        }

        private void InitUsers(string login, string password)
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                users = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }

            if (!users.ContainsKey(login))
            {
                users.Add(login, password);
                string result = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, result);
                Console.WriteLine("Пользователь зарегистрирован!");
            }
            else
            {
                Console.WriteLine("Этот логин уже существует.");
            }
        }

        private void FindUsers(string login, string password)
        {
            try
            {
                if (users.TryGetValue(login, out string storedPassword))
                {
                    if (password == storedPassword)
                    {
                        Console.WriteLine("Вы вошли!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Неверный пароль!");
                        Login();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Пользователь не найден!");
                    Login();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadUsers()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                users = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
        }
    }
}
