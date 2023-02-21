using BusinessObject;
using BusinessObject.DTO;
using DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Window
    {
        private ServiceProvider serviceProvider;
        public IOrderRepository _orderRepository;
        public IOrderDetailRepository _orderDetailRepository;

        public OrderManagement(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            InitializeComponent();
            if (!isAdmin())
            {
                EditButton.Visibility = Visibility.Hidden;
                DelButton.Visibility = Visibility.Hidden;
            }
        }

        private List<Order> GetAllOrder()
        {
            return _orderRepository.GetAllOrder();
        }

        private List<Order> GetOrderOfMember()
        {
            if(Application.Current.Resources["logged"] != null)
            {
                Member member = (Member)Application.Current.Resources["logged"];
                return _orderRepository.ListOrderByMemberId(member.MemberId);
            }
            return null;
        }

        private bool isAdmin()
        {
            var isAdmin = Application.Current.Properties["admin"];
            if (isAdmin != null)
            {
                return true;
            }
            return false;
        }

        private void RedirectWindow()
        {

            ServiceCollection services = new ServiceCollection();
            ConfigService.ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ResetWindow()
        {
            if (isAdmin())
            {
                orderDataView.ItemsSource = GetAllOrder().Select(x => new OrderDTO
                {
                    ID = x.OrderId,
                    MemberName = x.Member.Email,
                    OrderDate = x.OrderDate.ToString("dd/MM/yyyy"),
                    RequiredDate = x.RequiredDate?.ToString("dd/MM/yyyy"),
                    ShippedDate = x.ShippedDate?.ToString("dd/MM/yyyy"),
                    Freight = Convert.ToDecimal(x.Freight)
                });
            }
            else
            {
                orderDataView.ItemsSource = GetOrderOfMember()
                    .Select(x => new OrderDTO
                    {
                        ID = x.OrderId,
                        MemberName = x.Member.Email,
                        OrderDate = x.OrderDate.ToString("dd/MM/yyyy"),
                        RequiredDate = x.RequiredDate?.ToString("dd/MM/yyyy"),
                        ShippedDate = x.ShippedDate?.ToString("dd/MM/yyyy"),
                        Freight = Convert.ToDecimal(x.Freight)
                    });
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isAdmin())
            {
                orderDataView.ItemsSource = GetAllOrder().Select(x => new OrderDTO
                {
                    ID = x.OrderId,
                    MemberName = x.Member.Email,
                    OrderDate = x.OrderDate.ToString("dd/MM/yyyy"),
                    RequiredDate = x.RequiredDate?.ToString("dd/MM/yyyy"),
                    ShippedDate = x.ShippedDate?.ToString("dd/MM/yyyy"),
                    Freight = Convert.ToDecimal(x.Freight)
                });
            }
            else
            {
                orderDataView.ItemsSource = GetOrderOfMember()
                    .Select(x => new OrderDTO
                     {
                         ID = x.OrderId,
                         MemberName = x.Member.Email,
                         OrderDate = x.OrderDate.ToString("dd/MM/yyyy"),
                         RequiredDate = x.RequiredDate?.ToString("dd/MM/yyyy"),
                         ShippedDate = x.ShippedDate?.ToString("dd/MM/yyyy"),
                         Freight = Convert.ToDecimal(x.Freight)
                     });
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderDataView.SelectedCells != null && orderDataView.SelectedCells.Count() > 0)
                {
                    var cellInfor = orderDataView.SelectedCells.Distinct().Last();
                    var content = (OrderDTO)cellInfor.Item;
                    orderDetailDataView.ItemsSource =  _orderDetailRepository.GetListOrderDetailByOrderId(content.ID).Select(x =>new OrderDetailDTO
                    {
                        OrderID = x.OrderId,
                        ProductName = x.Product.ProductName,
                        Price = x.UnitPrice,
                        Quantity = x.Quantity,
                        Discount = x.Discount
                    });
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn Order để xem chi tiết");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private OrderDTO AppendOrder()
        {
            OrderDTO orderAppend = new OrderDTO();
            if (!string.IsNullOrWhiteSpace(orderIDBox.Text)) orderAppend.ID = Convert.ToInt32(orderIDBox.Text);
            if (!string.IsNullOrWhiteSpace(memberNameBox.Text)) orderAppend.MemberName = orderIDBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(freighBox.Text)) orderAppend.Freight = Convert.ToDecimal(freighBox.Text);
            return orderAppend;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
