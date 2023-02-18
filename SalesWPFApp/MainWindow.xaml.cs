using BusinessObject;
using BusinessObject.DTO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IOrderDetailRepository _orderDetailRepository;
        public IOrderRepository _orderRepository;
        public IProductRepository _productRepository;
        public IMemberRepository _memberRepository;

        public MainWindow(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IProductRepository productRepository, IMemberRepository memberRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _memberRepository = memberRepository;
            InitializeComponent();
        }

        private List<Product> GetAllProduct()
        {
            return _productRepository.GetAllProduct();           
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Product newProd = AppendProd();
            if (newProd.ProductId != 0)
            {
                var prodExist = _productRepository.GetProduct(newProd.ProductId);
                if (prodExist != null)
                {
                    MessageBox.Show("ID sản phẩm đã tồn tại vui lòng nhập hoặc để trống");
                }
            }
            else if (string.IsNullOrEmpty(newProd.ProductName))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm");
            }
            else
            {
                _productRepository.InsertProduct(newProd);
            }
        }

        private Product AppendProd()
        {
            Product prod = new Product();
            if (!string.IsNullOrWhiteSpace(IDBox.Text)) prod.ProductId = Convert.ToInt32(IDBox.Text);
            if (!string.IsNullOrWhiteSpace(catBox.Text)) prod.CategoryId = Convert.ToInt32(catBox.Text);
            prod.ProductName = nameBox.Text;
            prod.Weight = weightBox.Text;
            if (!string.IsNullOrWhiteSpace(priceBox.Text)) prod.UnitPrice = Convert.ToDecimal(priceBox.Text);
            if (!string.IsNullOrWhiteSpace(unitInStockBox.Text)) prod.UnitsInStock = Convert.ToInt32(unitInStockBox.Text);
            return prod;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Product productSearch = AppendProd();
            var listProd = _productRepository.SearchProduct(productSearch).Select(x => new ProductDTO
            {
                ID = x.ProductId,
                Name = x.ProductName,
                Category = x.CategoryId,
                WeightOfProduct = x.Weight,
                Price = x.UnitPrice,
                Instock = x.UnitsInStock
            });
            if (listProd != null && listProd.Count() > 0)
            {
                dataView.ItemsSource = listProd;
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm");
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataView.ItemsSource = GetAllProduct().Select(x => new ProductDTO
            {
                ID = x.ProductId,
                Name = x.ProductName,
                Category = x.CategoryId,
                WeightOfProduct = x.Weight,
                Price = x.UnitPrice,
                Instock = x.UnitsInStock
            });
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            IDBox.Text = "";
            nameBox.Text = "";
            priceBox.Text = "";
            weightBox.Text = "";
            catBox.Text = "";
            unitInStockBox.Text = "";
            dataView.ItemsSource = GetAllProduct().Select(x => new ProductDTO
            {
                ID = x.ProductId,
                Name = x.ProductName,
                Category = x.CategoryId,
                WeightOfProduct = x.Weight,
                Price = x.UnitPrice,
                Instock = x.UnitsInStock
            }); 
        }

        private void dataView_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (dataView.SelectedCells != null && dataView.SelectedCells.Count() > 0)
                {
                    var cellInfor = dataView.SelectedCells.Distinct().Last();
                    var content = (ProductDTO)cellInfor.Item;
                    IDBox.Text = content.ID.ToString();
                    nameBox.Text = content.Name;
                    priceBox.Text = content.Price.ToString();
                    unitInStockBox.Text = content.Instock.ToString();
                    weightBox.Text = content.WeightOfProduct.ToString();
                    catBox.Text = content.Category.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
