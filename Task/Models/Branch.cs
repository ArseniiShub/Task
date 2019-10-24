using System;
using System.Collections.Generic;

namespace Task.Models
{
    public class Branch : BindableBase
    {
        private int id;
        private string name;

        private int? managerId;
        private Employee manager;
        private ICollection<Employee> employees;
        public int Id
        {
            get => id;
            set { SetProperty(ref id, value); }
        }
        public string Name
        {
            get => name;
            set { SetProperty(ref name, value); }
        }
        public ICollection<Employee> Employees
        {
            get => employees;
            set { SetProperty(ref employees, value); }
        }
        public int? ManagerId
        {
            get => managerId;
            set { SetProperty(ref managerId, value); }
        }
        public Employee Manager
        {
            get => manager;
            set { SetProperty(ref manager, value); }
        }
    }
}
