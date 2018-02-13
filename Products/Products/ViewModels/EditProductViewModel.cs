namespace Products.ViewModels
{
    using Services;
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Models;

    public class EditProductViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        string _description;
        decimal _price;
        bool _isActive;
        DateTime _lastPurchase;
        double _stock;
        string _remarks;
        string _image;
        Product product;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsActive)));
                }
            }
        }

        public DateTime LastPurchase
        {
            get
            {
                return _lastPurchase;
            }
            set
            {
                if (_lastPurchase != value)
                {
                    _lastPurchase = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastPurchase)));
                }
            }
        }

        public double Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stock)));
                }
            }
        }

        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                if (_remarks != value)
                {
                    _remarks = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Remarks)));
                }
            }
        }

        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
                }
            }
        }
        #endregion

        #region Constructors
        public EditProductViewModel(Product product)
        {
            this.product = product;
            dialogService = new DialogService();
            IsEnabled = true;
            apiService = new ApiService();
            navigationService = new NavigationService();

            Description = product.Description;
            Price = product.Price;
            IsActive = product.IsActive;
            LastPurchase = product.LastPurchase;
            Stock = product.Stock;
            Remarks = product.Remarks;
            Image = product.Image;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            if (String.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error", "You must enter a description");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            product.Description = Description;
            product.Price = Price;
            product.IsActive = IsActive;
            product.LastPurchase = LastPurchase;
            product.Stock = Stock;
            product.Remarks = Remarks;
            product.Image = Image;

            var response = await apiService.Put(
              "http://200.76.182.140:8085",
              "/api",
              "/Products",
              mainViewModel.Token.TokenType,
              mainViewModel.Token.AccessToken,
              product);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }
            
            var productsViewModel = ProductsViewModel.GetInstance(mainViewModel.category.Products);
            productsViewModel.UpdateProduct(product);
            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion
    }
}
