using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Task.Data;
using Task.Models;

namespace Task.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            availableTables = new ObservableCollection<string>();
            foreach (var navPoint in Enum.GetNames(typeof(NavPoint)))
            {
                availableTables.Add(navPoint.ToString());
            }
            SelectedTable = availableTables.FirstOrDefault();
        }
        private EmployeeViewModel employeeViewModel = new EmployeeViewModel();
        private BranchViewModel branchViewModel = new BranchViewModel();

        #region Navigation
        private BindableBase currentViewModel;
        public BindableBase CurrentViewModel
        {
            get => currentViewModel;
            set { SetProperty(ref currentViewModel, value); }
        }
        private void Navigate(string destination)
        {
            switch (destination)
            {
                case "Employees":
                    CurrentViewModel = employeeViewModel as BindableBase; break;
                case "Branches":
                    CurrentViewModel = branchViewModel as BindableBase; break;
                default: throw new ArgumentException("Invalid destination");
            }
        }
        private ObservableCollection<string> availableTables;
        public ObservableCollection<string> AvailableTables 
        {
            get => availableTables;
            set { SetProperty(ref availableTables, value); }
        }
        private string selectedTable;
        public string SelectedTable
        {
            get => selectedTable;
            set
            {
                if (selectedTable == value)
                    return;
                selectedTable = value;
                Navigate(selectedTable);
            }
        }
        #endregion
    }
}
