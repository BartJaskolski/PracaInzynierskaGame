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
        private bool CanExe { get; set; }

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
            CanExe = _canExecute();
            return CanExe;
        }

        public void Execute(object parameter)
        {
            if(CanExe)
                _methodToExecute();
        }
        
    }
}
