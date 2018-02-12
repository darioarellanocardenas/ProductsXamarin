﻿namespace Products.ViewModels
{
    using System.Collections.Generic;
    using Products.Models;
    using System.ComponentModel;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ProductsViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        List<Product> products;
        ObservableCollection<Product> _products;
        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Products)));
                }
            }
        }
        #endregion

        #region Constructor
        public ProductsViewModel(List<Product> products)
        {
            this.products = products;
            Products = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
        }
        #endregion

        
    }
}