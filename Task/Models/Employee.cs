using System;
using System.ComponentModel;

namespace Task.Models
{
    public class Employee : BindableBase
    {
        private int id;
        private string firstName;
        private string lastName;
        private string patronymic;
        private DateTime birthDate;
        private Gender gender;

        private Branch branchManager;
        private int? branchId;
        private Branch branch;

        #region Properties
        public int Id
        {
            get => id;
            set { SetProperty(ref id, value); }
        }
        public string FirstName
        {
            get => firstName;
            set { SetProperty(ref firstName, value); }
        }
        public string LastName
        {
            get => lastName;
            set { SetProperty(ref lastName, value); }
        }
        public string Patronymic
        {
            get => patronymic;
            set { SetProperty(ref patronymic, value); } 
        }
        public DateTime BirthDate 
        {
            get => birthDate;
            set { SetProperty(ref birthDate, value); }
        }
        public Gender Gender
        {
            get => gender;
            set { SetProperty(ref gender, value); }
        }
        public Branch Branch
        {
            get => branch;
            set { SetProperty(ref branch, value); }
        }
        public int? BranchId
        {
            get => branchId;
            set { SetProperty(ref branchId, value); }
        }
        public Branch BranchManager
        {
            get => branchManager;
            set { SetProperty(ref branchManager, value); }
        }
        #endregion
        public override string ToString()
        {
            return $"{id}. {lastName} {firstName} {patronymic}";
        }
    }
}
