namespace Products.ViewModels
{
    using System.Collections.Generic;
    using Products.Models;
    using System.ComponentModel;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Products.Services;

    public class ProductsViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
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
            apiService = new ApiService();
            dialogService = new DialogService();
            instance = this;
            this.products = products;
            Products = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
        }

        #endregion

        #region Sigleton
        static ProductsViewModel instance;

        public static ProductsViewModel GetInstance(List<Product> Products)
        {
            if (instance == null)
            {
                return new ProductsViewModel(Products);
            }

            return instance;
        }
        #endregion

        #region Methodos

        public void AddProduct(Product product)
        {
            //IsRefreshing = true;
            products.Add(product);
            Products = new ObservableCollection<Product>(products.OrderBy(c => c.Description));
            //IsRefreshing = false;
        }

        public void UpdateProduct(Product product)
        {
            //IsRefreshing = true;
            var oldProduct = products
                .Where(c => c.ProductId == product.ProductId)
                .FirstOrDefault();
            oldProduct = product;
            Products = new ObservableCollection<Product>(products.OrderBy(c => c.Description));
            //IsRefreshing = false;
        }
        /*
        public async Task DeleteCategory(Category category)
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.Delete(
              "http://200.76.182.140:8085",
              "/api",
              "/Categories",
              mainViewModel.Token.TokenType,
              mainViewModel.Token.AccessToken,
              category);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }
            categories.Remove(category);
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }*/
        #endregion
    }
}
