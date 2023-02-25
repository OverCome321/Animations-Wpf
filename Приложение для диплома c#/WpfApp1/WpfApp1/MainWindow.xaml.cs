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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using System.Net;
using System.Net.Mail;
using System.Security;
namespace WpfApp1
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 370;
            btnAnimation.Duration = TimeSpan.FromSeconds(0.5);
            Go.BeginAnimation(Button.WidthProperty, btnAnimation);
            DoubleAnimation btnAnimation3 = new DoubleAnimation();
            btnAnimation3.From = 0;
            btnAnimation3.To = 242;
            btnAnimation3.Duration = TimeSpan.FromSeconds(0.15);
            SignIn.BeginAnimation(Button.WidthProperty, btnAnimation3);
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
            DataBase database = new DataBase();
            var mailUser = Mail.Text;
            var passUser = Pass.Text;
            var passUserTB = Password.Password;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            string querystring = $"select id_user, phone_user, password_user, mail_user from register where mail_user = '{mailUser}' and password_user = '{passUser}' or password_user = '{passUserTB}'";
            string querystring2 = $"select password_user from register where mail_user = '{mailUser}'";
            SqlCommand comand = new SqlCommand(querystring, database.getConnection());
            SqlCommand comand2 = new SqlCommand(querystring2, database.getConnection());
            adapter.SelectCommand = comand;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                adapter2.SelectCommand = comand2;
                adapter2.Fill(table2);
                if (table2.Rows.Count == 1)
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
                    NoAccount.Visibility = Visibility.Hidden;
                }
                else
                {
                    TranslateTransform trans = new TranslateTransform();
                    Go.RenderTransform = trans;
                    DoubleAnimation doubleAnimation2X = new DoubleAnimation(0, 7, TimeSpan.FromSeconds(0.1));
                    trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2X);
                    DoubleAnimation doubleAnimation2Y = new DoubleAnimation(0, -7, TimeSpan.FromSeconds(0.1));
                    trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2Y);
                    DoubleAnimation doubleAnimation2x = new DoubleAnimation(7, 0, TimeSpan.FromSeconds(0.1));
                    trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2x);
                }
            }
            else
            {
                TranslateTransform trans = new TranslateTransform();
                Go.RenderTransform = trans;
                DoubleAnimation doubleAnimation2X = new DoubleAnimation(0, 7, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2X);
                DoubleAnimation doubleAnimation2Y = new DoubleAnimation(0, -7, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2Y);
                DoubleAnimation doubleAnimation2x = new DoubleAnimation(7, 0, TimeSpan.FromSeconds(0.1));
                trans.BeginAnimation(TranslateTransform.XProperty, doubleAnimation2x);
                MessageBox.Show("Неверный адресс электронной почты или пароль!", "Попробуйте снова!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
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
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                Window2 window2 = new Window2();
                this.Close();
                window2.Show();
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
