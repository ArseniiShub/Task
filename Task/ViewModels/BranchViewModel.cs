using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Task.Data;
using Task.Models;

namespace Task.ViewModels
{
    class BranchViewModel : BindableBase
    {
        private TaskContext context;
        public BranchViewModel()
        {
            context = new TaskContext();
            Elements = new ObservableCollection<Branch>(context.Branches.Include(b => b.Manager));
            Employees = new List<Employee>(context.Employees);
        }
        public ObservableCollection<Branch> Elements { get; set; }
        public List<Employee> Employees { get; set; }

        private Branch selectedElement;
        public Branch SelectedElement
        {
            get => selectedElement;
            set { SetProperty(ref selectedElement, value);}
        }
        #region Commands
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get => addCommand ?? (addCommand = new RelayCommand(
                () =>
                {
                    var newBranch = new Branch();
                    context.Branches.Add(newBranch);
                    Elements.Add(newBranch);
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
                        var branch = context.Branches.FirstOrDefault(p => p.Id == item.Id);
                        if (branch != null)
                        {
                            context.Entry(branch).CurrentValues.SetValues(item);
                        }
                    }
                    context.SaveChanges();
                }
                ));
        }
        #endregion
    }
}
