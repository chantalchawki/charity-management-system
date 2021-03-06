﻿using Caliburn.Micro;
using charity_management_system.DataStores;
using charity_management_system.Models;
using charity_management_system.Repositories;
using charity_management_system.Utils;
using charity_management_system.Views;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace charity_management_system.ViewModels
{
    public class EmployeesViewModel : Screen
    {
        public ObservableCollection<EmployeeCardViewModel> FilteredEmployees { get; set; }
        private ObservableCollection<EmployeeCardViewModel> _employees { get; set; }

        public String SearchValue { get; set; }
        private IRepository<PaidEmployee> _repository;

        public EmployeesViewModel()
        {
            this.SearchValue = "";
            _repository = new PaidEmployeeRepo();
            _employees = new ObservableCollection<EmployeeCardViewModel>();
            _employees = Mapper.toViewModel<PaidEmployee, EmployeeCardViewModel>(
                EmployeeDataStore.instance.data as ObservableCollection<PaidEmployee>,
                    (employee) => new EmployeeCardViewModel(employee));

            EmployeeDataStore.instance.data.CollectionChanged += new NotifyCollectionChangedEventHandler((obj, e) =>
            {
                FilteredEmployees = Mapper.toViewModel<PaidEmployee, EmployeeCardViewModel>(
                    obj as ObservableCollection<PaidEmployee>,
                    (employee) => new EmployeeCardViewModel(employee)
                );
            });
            this.PropertyChanged += EmployeesViewModel_PropertyChanged;
            FilteredEmployees = new ObservableCollection<EmployeeCardViewModel>(_employees);
        }

        private void EmployeesViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SearchValue" && SearchValue.Length > 0)
            {
                PaidEmployee p = EmployeeDataStore.instance.repository.findByID(SearchValue);
                FilteredEmployees.Clear();
                if (p != null)
                {
                    var s = new EmployeeCardViewModel(p);
                    FilteredEmployees.Add(s);
                }
            }
            else if (e.PropertyName == "SearchValue")
            {
                FilteredEmployees = _employees;
            }
        }
    }
}
