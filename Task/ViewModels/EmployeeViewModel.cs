using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Task.Data;
using Task.Models;

namespace Task.ViewModels
{
    class EmployeeViewModel : BindableBase
    {
        private TaskContext context;
        public EmployeeViewModel()
        {
            context = new TaskContext();
            Elements = new ObservableCollection<Employee>(context.Employees.Include(p => p.Branch));
            Branches = new List<Branch>(context.Branches);
            Genders = Enum.GetValues(typeof(Gender));
        }
        public ObservableCollection<Employee> Elements { get; set; }
        public List<Branch> Branches { get; set; }
        public Array Genders { get; set; }

        private Employee selectedElement;
        public Employee SelectedElement
        {
            get => selectedElement;
            set { SetProperty(ref selectedElement, value); }
        }

        #region Commands
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get => addCommand ?? (addCommand = new RelayCommand(
                () =>
                {
                    var newEmp = new Employee();
                    context.Employees.Add(newEmp);
                    Elements.Add(newEmp);
                }));
        }
        private RelayCommand removeSelectedCommand;
        public RelayCommand RemoveSelectedCommand
        {
            get => removeSelectedCommand ?? (removeSelectedCommand = new RelayCommand(
                () =>
                {
                    context.Remove(SelectedElement);
                    context.SaveChanges();
                    Elements.Remove(SelectedElement);
                },
                () =>
                {
                    return SelectedElement != null;
                }
                ));
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new RelayCommand(
                () =>
                {
                    foreach (var item in Elements)
                    {
                        var emp = context.Employees.FirstOrDefault(p => p.Id == item.Id);
                        if (emp != null)
                        {
                            context.Entry(emp).CurrentValues.SetValues(item);

                        }
                    }
                    context.SaveChanges();
                }
                ));
        }
        #endregion
    }
}
