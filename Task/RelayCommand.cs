using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Task
{
    public class RelayCommand : ICommand
    {
        Action execute;
        Func<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute();
            if (execute != null)
                return true;
            else return false;
        }
        public void Execute(object parameter)
        {
            this?.execute();
        }
    }
}
