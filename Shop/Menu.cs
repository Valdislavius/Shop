using System.Text.Json;

namespace Shop
{
    internal class Menu
    {
        private int currentSelection = 0;
        private UserManager _userManager;

        public string[] menuItems = new string[]
        {
            "Регистрация",
            "Вход",
            "Выход"
        };

        public Menu(UserManager userManager)
        {
            _userManager = userManager;
        }

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
                    _userManager.Register();
                    break;

                case 1:
                    _userManager.Login();
                    break;


                case 2:
                    Console.WriteLine("Вы вышли!");

                    break;

                default:
                    break;
            }
        }
    }
}