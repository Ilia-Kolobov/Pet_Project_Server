using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Console = Colorful.Console;
using System.Threading.Tasks;
using System.Threading;

namespace UserInterface.Menu
{
    public class ConsoleMenu
    {
        #region Public var

        public ConsoleMenu ParentMenu { get; set; }

        public string Header { get; set; }
        public string SubTitle { get; set; }
        public string CursorText { get; set; }
        public Color CursorColor { get; set; }
        public Color HeaderColor { get; set; }
        public Color ForeColor { get; set; }
        public Color MenuItemColor { get; set; }
        public Color SubTitleColor { get; set; }

        #endregion

        #region Private var

        private List<MenuItem> menuItemList;

        private int cursor;
        private bool exit;

        #endregion

        #region Constructor

        public ConsoleMenu(string cursorText = "->")
        {
            menuItemList = new List<MenuItem>();
            cursor = 0;

            this.CursorText = cursorText;
            CursorColor = Color.Khaki;
            HeaderColor = Color.DarkSalmon;
            ForeColor = Color.DarkGoldenrod;
            MenuItemColor = Color.White;
            SubTitleColor = Color.White;
        }

        #endregion 

        #region Public methods

        public bool AddMenuItem(int id, string text, Action action)
        {
            // check if it dosen't already exists
            if (!menuItemList.Any(item => item.ID == id))
            {
                menuItemList.Add(new MenuItem(id, text, action));
                return true;
            }
            return false;
        }


        public bool RemoveMenuItem(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Virtual methods

        public virtual void DrawHeader()
        {
            Console.ForegroundColor = HeaderColor;
            Console.WriteAscii(Header, HeaderColor);
            Console.ForegroundColor = ForeColor;
        }

        public virtual void DrawWithHeader()
        {
            DrawHeader();
            Draw();
        }

        public virtual void Draw()
        {
            Console.WriteLine(SubTitle);

            for (int i = 0; i < menuItemList.Count; i++)
            {
                if (i == cursor)
                { 
                    Console.ForegroundColor = CursorColor;
                    Console.Write(CursorText + " ");
                    Console.WriteLine(menuItemList[i].Text);
                    Console.ForegroundColor = ForeColor;
                }
                else
                {
                    Console.Write(new string(' ', (CursorText.Length + 1)));
                    Console.WriteLine(menuItemList[i].Text);
                }
            }
            Console.SetWindowSize(130, 30);

        }

        public virtual void Clear()
        {
            Console.Clear();
        }

        public virtual void ClearWithoutHeader()
        {
            Console.Clear();
            DrawHeader();
        }

        public virtual void ShowMenu()
        {
            Console.CursorVisible = false;
            Console.Clear();
            DrawWithHeader();
            exit = false;
            while (!exit)
            {
                UpdateMenu();
            }
        }

        public virtual void HideMenu()
        {
            exit = true;
        }

        public virtual void UpdateMenu()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    {
                        if (cursor > 0)
                        {
                            cursor--;
                            Console.Clear();
                            DrawWithHeader();
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    {
                        if (cursor < (menuItemList.Count - 1))
                        {
                            cursor++;
                            Console.Clear();
                            DrawWithHeader();
                        }
                    }
                    break;
                case ConsoleKey.Escape:
                    {
                        if (ParentMenu != null)
                        {

                            HideMenu();
                        }
                    }
                    break;
                case ConsoleKey.Enter:
                    {
                        Console.Clear();
                        DrawHeader();
                        Console.CursorVisible = true;
                        menuItemList[cursor].Action();
                        Console.CursorVisible = false;
                        Console.Clear();
                        DrawWithHeader();
                    }
                    break;
                default:
                    {
                        // Unsuported key. Do nothing....
                    }
                    break;
            }
        }
        #endregion
    }
}
