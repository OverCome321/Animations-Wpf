using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Net;
using System.Net.Mail;
namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataBase database = new DataBase();
        Random rnd = new Random();
        public Window1()
        {
            InitializeComponent();           
            Password.MaxLength = 50;
            Pass.MaxLength = 50;
            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 370;
            btnAnimation.Duration = TimeSpan.FromSeconds(0.5);
            Go.BeginAnimation(Button.WidthProperty, btnAnimation);
            DoubleAnimation btnAnimation3 = new DoubleAnimation();
            btnAnimation3.From = 0;
            btnAnimation3.To = 292;
            btnAnimation3.Duration = TimeSpan.FromSeconds(0.15);
            Register.BeginAnimation(Button.WidthProperty, btnAnimation3);
            VerificationLabel.Visibility = Visibility.Hidden;
            VerificationButt.Visibility = Visibility.Hidden;
            VerificationTB.Visibility = Visibility.Hidden;
            SendAgain.Visibility = Visibility.Hidden;
    }
        private void ShowPasswordFunction()
        {
            Pass.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Hidden;
            Pass.Text = Password.Password;
        }

        private void HidePasswordFunction()
        {
            Pass.Visibility = Visibility.Hidden;
            Password.Visibility = Visibility.Visible;
            Password.Password = Pass.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pass.Text = Convert.ToString(Password);
            Pass.Text = "";
            if (checkPhoneAndPass() == true)
            {

            }
            else if (checkuser() == true)
            {

            }
            else
            {
                try
                {
                    string s = Convert.ToString(rnd.Next(1000, 10000));
                    HidenVerification.Text = s;
                    MailAddress FromMailAddress = new MailAddress("simpleclub321@gmail.com", "SimpleClub");
                    MailAddress ToMailAddress = new MailAddress(Mail.Text, "DearUser");
                    MailMessage mailMessage = new MailMessage(FromMailAddress, ToMailAddress);
                    mailMessage.Body = s;
                    mailMessage.Subject = "Проверочный код";
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(FromMailAddress.Address, "PaNdA^^)p12");
                    smtpClient.Send(mailMessage);
                    MessageBox.Show("На вашу почту отправлен 4-х значный код", "Проверка", MessageBoxButton.OK, MessageBoxImage.Information);
                    VerificationLabel.Visibility = Visibility.Visible;
                    VerificationButt.Visibility = Visibility.Visible;
                    VerificationTB.Visibility = Visibility.Visible;
                    SendAgain.Visibility = Visibility.Visible;
                    MailLab.Visibility = Visibility.Hidden;
                    Mail.Visibility = Visibility.Hidden;
                    PhoneBorder.Visibility = Visibility.Hidden;
                    PhoneButt.Visibility = Visibility.Hidden;
                    PasswordLabel.Visibility = Visibility.Hidden;
                    Pass.Visibility = Visibility.Hidden;
                    Password.Visibility = Visibility.Hidden;
                    CheckPass.Visibility = Visibility.Hidden;
                    Go.Visibility = Visibility.Hidden;
                    PasswordBorder.Visibility = Visibility.Hidden;
                    SignInBut.Visibility = Visibility.Hidden;
                }
                catch
                {
                    MessageBox.Show("Не верный адресс электронной почты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private Boolean checkuser()
        {
            var mailUser = Mail.Text;
            var passUser = Password;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user, phone_user, password_user, mail_user from register where mail_user = '{mailUser}' and password_user = '{passUser}'";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if(table.Rows.Count > 0)
            {
                TranslateTransform trans = new TranslateTransform();
                Go.RenderTransform = trans;
                DoubleAnimation doubleAnimation2X = new DoubleAnimation(0, 7, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2X);
                DoubleAnimation doubleAnimation2Y = new DoubleAnimation(0, -7, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2Y);
                DoubleAnimation doubleAnimation2x = new DoubleAnimation(7, 0, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2x);
                MessageBox.Show("Пользователь с такими данными уже создан!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else
            {
                return false;
            }
            
        }
        private Boolean checkPhoneAndPass()
        {
            if (Mail.Text == "" || Mail.Text == " " || Password.Password == "" || Password.Password == "")
            {
                TranslateTransform trans = new TranslateTransform();
                Go.RenderTransform = trans;
                DoubleAnimation doubleAnimation2X = new DoubleAnimation(0, 7, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2X);
                DoubleAnimation doubleAnimation2Y = new DoubleAnimation(0, -7, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2Y);
                DoubleAnimation doubleAnimation2x = new DoubleAnimation(7, 0, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2x);
                MessageBox.Show("Вы ввели запрещенный символ пробел или вы просто ничего не ввели!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else
            {
                return false;
            }
        } 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }       
        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (CheckPass.IsChecked == true)
            {
                ShowPasswordFunction();
            }
            else
            {
                HidePasswordFunction();
            }
        }

        private void _2323_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PhoneButt_Click(object sender, RoutedEventArgs e)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.From = Colors.SteelBlue;
            colorAnimation.To = Colors.Red;
            colorAnimation.From = Colors.Red;
            colorAnimation.To = Colors.SteelBlue;
            colorAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            Storyboard.SetTarget(colorAnimation, PhoneBorder);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("Background.Color"));
            Storyboard stb = new Storyboard();
            stb.Children.Add(colorAnimation);
            stb.Begin();
            TranslateTransform trans = new TranslateTransform();
            PhoneBorder.RenderTransform = trans;
            DoubleAnimation doubleAnimation2X = new DoubleAnimation(0, 7, TimeSpan.FromSeconds(0.1));
            trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2X);
            DoubleAnimation doubleAnimation2Y = new DoubleAnimation(0, -7, TimeSpan.FromSeconds(0.1));
            trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2Y);
            DoubleAnimation doubleAnimation2x = new DoubleAnimation(7, 0, TimeSpan.FromSeconds(0.1));
            trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2x);
            MessageBox.Show("you can't use mobile authorization", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void VerificationButt_Click(object sender, RoutedEventArgs e)
        {
            if (HidenVerification.Text == VerificationTB.Text)
            {
                Pass.Text = Password.Password;
                var mail = Mail.Text;
                var password = Pass.Text;
                string querystring = $"insert into register(mail_user, password_user) values('{mail}', '{password}')";
                SqlCommand command = new SqlCommand(querystring, database.getConnection());
                database.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Аккаунт успешно создан!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    Window2 window2 = new Window2();
                    window2.Show();
                    this.Close();
                }
                else
                {
                    TranslateTransform trans = new TranslateTransform();
                    VerificationButt.RenderTransform = trans;
                    DoubleAnimation doubleAnimation2X = new DoubleAnimation(0, 7, TimeSpan.FromSeconds(0.1));
                    trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2X);
                    DoubleAnimation doubleAnimation2Y = new DoubleAnimation(0, -7, TimeSpan.FromSeconds(0.1));
                    trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2Y);
                    DoubleAnimation doubleAnimation2x = new DoubleAnimation(7, 0, TimeSpan.FromSeconds(0.1));
                    trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2x);
                    MessageBox.Show("Аккаунт не создан!", "Попробуйте еще раз", MessageBoxButton.OK, MessageBoxImage.Warning);
                    VerificationLabel.Visibility = Visibility.Hidden;
                    VerificationButt.Visibility = Visibility.Hidden;
                    VerificationTB.Visibility = Visibility.Hidden;
                    MailLab.Visibility = Visibility.Visible;
                    Mail.Visibility = Visibility.Visible;
                    PhoneBorder.Visibility = Visibility.Visible;
                    PhoneButt.Visibility = Visibility.Visible;
                    PasswordLabel.Visibility = Visibility.Visible;
                    Pass.Visibility = Visibility.Visible;
                    Password.Visibility = Visibility.Visible;
                    CheckPass.Visibility = Visibility.Visible;
                    Go.Visibility = Visibility.Visible;
                    PasswordBorder.Visibility = Visibility.Visible;
                    SignInBut.Visibility = Visibility.Visible;
                }
            }
            else
            {
                TranslateTransform trans = new TranslateTransform();
                VerificationButt.RenderTransform = trans;
                DoubleAnimation doubleAnimation2X = new DoubleAnimation(0, 7, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2X);
                DoubleAnimation doubleAnimation2Y = new DoubleAnimation(0, -7, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2Y);
                DoubleAnimation doubleAnimation2x = new DoubleAnimation(7, 0, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2x);
            }
            database.closeConnection();
        }
        private void SendAgain_Click(object sender, RoutedEventArgs e)
        {
            string s = Convert.ToString(rnd.Next(1000, 10000));
            HidenVerification.Text = s;
            MailAddress FromMailAddress = new MailAddress("simpleclub321@gmail.com", "SimpleClub");
            MailAddress ToMailAddress = new MailAddress(Mail.Text, "DearUser");
            MailMessage mailMessage = new MailMessage(FromMailAddress, ToMailAddress);
            mailMessage.Body = s;
            mailMessage.Subject = "Проверочный код";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(FromMailAddress.Address, "PaNdA^^)p12");
            smtpClient.Send(mailMessage);
        }
    }
}
