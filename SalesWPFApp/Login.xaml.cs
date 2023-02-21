using BusinessObject;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private ServiceProvider serviceProvider;
        public IMemberRepository _memberRepository;
        public Login(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
            InitializeComponent();
        }

        private void RedirectWindow()
        {
            
            ServiceCollection services = new ServiceCollection();
            ConfigService.ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var defaultMember = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("account");
            if (!(string.IsNullOrWhiteSpace(emailBox.Text) && string.IsNullOrWhiteSpace(passBox.Text)))
            {
                if(emailBox.Text == defaultMember["email"] && passBox.Text == defaultMember["password"])
                {
                    Application.Current.Properties["admin"] = true;
                    this.Hide();
                    RedirectWindow();
                    var prodWindow = serviceProvider.GetService<ProductManagement>();
                    prodWindow.Show();
                    this.Close();
                }
                else
                {
                    var validMember = _memberRepository.GetMember(emailBox.Text, passBox.Text);
                    if (validMember!=null)
                    {
                        Application.Current.Resources["logged"] = validMember;
                        this.Hide();
                        RedirectWindow();
                        var prodWindow = serviceProvider.GetService<ProductManagement>();
                        prodWindow.Show();   
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản không tồn tại");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập cả email và password");
            }
        }
    }
}
