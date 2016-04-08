using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Crossword_Application_Modern.Commands
{
    /// <summary>
    /// Статический класс кастомных команд приложения.
    /// </summary>
    public static class CustomCommands
    {
        /// <summary>
        /// Команда добавления нового элемента.
        /// </summary>
        public static RoutedCommand AddNewItem { get; set; }

        static CustomCommands()
        {            
            AddNewItem = new RoutedCommand("AddNewItem", typeof(CustomCommands));              
        }
    }
}
