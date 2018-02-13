namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using Products.ViewModels;
    using System;
    using System.Windows.Input;

    public class Product
    {
        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Properties
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastPurchase { get; set; }
        public double Stock { get; set; }
        public string Image { get; set; }
        public string Remarks { get; set; }
        public int CategoryId { get; set; }

        public string ImageFullPath
        {
            get
            {
                return String.Format("http://orion-v-des1:8086/{0}",
                    Image.Substring(1));
            }

        }
        #endregion

        #region Constructor
        public Product()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();

        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ProductId;
        }
        #endregion

        #region Commands

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }

        async void Edit()
        {
            var mainViewModel = MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
            await navigationService.Navigate("EditProductView");
        }

       /* public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(DeleteCategory);
            }
        }

        async void DeleteCategory()
        {
            var response = await dialogService.ShowConfirm("Delete Category", "Are you sure to delete this record?");
            if (!response)
            {
                return;
            }

            CategoriesViewModel.GetInstance().DeleteCategory(this);
        }*/

        #endregion
    }
}
