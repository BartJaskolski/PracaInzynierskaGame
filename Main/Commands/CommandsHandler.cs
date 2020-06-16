using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.Commands
{
    public class CommandsHandler : ICommand
    {
        private Action _methodToExecute;
        private Func<bool> _canExecute;

        public CommandsHandler(Action methodToExecute, Func<bool> canExecute)
        {
            _methodToExecute = methodToExecute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _methodToExecute();
        }
        
    }
}
