﻿namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.ViewModels;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Services;
    public class Category
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
        #endregion


        #region Constructor
        public Category()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands

        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectCategory);
            }
        }

        async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModel(Products);
            await navigationService.Navigate("ProductsView");
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(EditCategory);
            }
        }

        async void EditCategory()
        {
            var mainViewModel = MainViewModel.GetInstance().EditCategory = new EditCategoryViewModel(this);
            await navigationService.Navigate("EditCategoryView");
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(DeleteCategory);
            }
        }

        async void DeleteCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModel(Products);
            await navigationService.Navigate("ProductsView");
        }

        #endregion


    }
}
