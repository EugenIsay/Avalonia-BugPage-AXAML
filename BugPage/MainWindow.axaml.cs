using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;
using Avalonia.Media;
using System;
using System.Xml.Linq;

namespace BugPage
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string CurUser
        {
            get; set;
        }
        private string GetName => Users[Users.IndexOf(new User { UserEmail = CurUser })].UserName;
        private string GetSurname => Users[Users.IndexOf(new User { UserEmail = CurUser })].UserSurname;
        private string GetUserEmail => Users[Users.IndexOf(new User { UserEmail = CurUser })].UserEmail;
        private string GetAge => Users[Users.IndexOf(new User { UserEmail = CurUser })].Age;
        private string GetDate => (Users[Users.IndexOf(new User { UserEmail = CurUser })].RegTime).ToString("MM/dd/yyyyy HH:mm");

        //Методы----------------------------------------------------

        List<User> Users = new List<User>();
        void CheckReg(TextBox box)
        {
            if (string.IsNullOrEmpty(box.Text) == true)
            {
                MakeRed(box);
            }
        }
        async void MakeRed(TextBox box)
        {
            box.BorderBrush = Brushes.Red;
            await Task.Delay(500);
            box.BorderBrush = Brushes.Gainsboro;
        }
        public void AllFalse()
        {
            Profile.IsVisible = false;
            BugPage.IsVisible = false;
            Reg.IsVisible = false;
            SplitView1.IsPaneOpen = false;
        }

        // Кнопки---------------------------------------------------
        //Кнопки регистрации

        public void Reg1Button(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(name.Text) == true ||
                string.IsNullOrEmpty(email.Text) == true ||
                string.IsNullOrEmpty(password.Text) == true ||
                email.Text.Contains("@") == false ||
                Users.Contains(new User { UserEmail = email.Text }) == true)
            {
                CheckReg(name);
                CheckReg(email);
                CheckReg(password);
                if (Users.Contains(new User { UserEmail = email.Text }) == true)
                {
                    MakeRed(email);
                    InfoReg.Text = "Данная почта уже занята";
                }
                if (email.Text.Contains("@") == false)
                {
                    MakeRed(email);
                    InfoReg.Text = "Некоректный ввод почты";
                }
            }
            else
            {
                registration.IsVisible = false;
                registration2.IsVisible = true;
            }
        }

        public void Reg2Button(object sender, RoutedEventArgs args)
        {
            if (Code.Text == "123")
            {
                Users.Add(new User
                {
                    UserName = name.Text,
                    UserEmail = email.Text,
                    UserSurname = surname.Text,
                    UserPassword = password.Text,
                    RegTime = DateTime.Now
                });
                CurUser = email.Text;
                registration.IsVisible = true;
                registration2.IsVisible = false;
                name.Text = null; email.Text = null;
                surname.Text = null; password.Text = null;
                AllFalse();
                BugPage.IsVisible = true;
                Pane.IsVisible = true;
            }
        }

        public void ButtonEnter(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(emailentry.Text) == true || string.IsNullOrEmpty(pass.Text) == true || Users.Contains(new User { UserEmail = emailentry.Text }) == false)
            {
                CheckReg(emailentry);
                CheckReg(pass);
                if (Users.Contains(new User { UserEmail = emailentry.Text }) == false)
                {
                    MakeRed(emailentry);
                    EtryInfo.Text = "Такого пользователя не существует";
                }

            }
            else
            {
                if (Users[Users.IndexOf(new User { UserEmail = emailentry.Text })].UserPassword == pass.Text)
                {
                    AllFalse(); 
                    BugPage.IsVisible = true;
                    Pane.IsVisible = true; CurUser = emailentry.Text;
                }
                else
                {
                    MakeRed(pass);
                    EtryInfo.Text = "Неправильный пароль";
                }
            }

        }

        // Кнопки основы
        public void PaneButton(object sender, RoutedEventArgs args)
        {
            SplitView1.IsPaneOpen = !SplitView1.IsPaneOpen;
            PanePic1.IsVisible = !PanePic1.IsVisible;
            PanePic2.IsVisible = !PanePic2.IsVisible;
            PaneName.Text = GetName;
        }
        public void SettingComfirm(object sender, RoutedEventArgs args)
        {
            if (String.IsNullOrEmpty(CurName.Text) == false)
            {
                Users[Users.IndexOf(new User { UserEmail = CurUser })].UserName = CurName.Text;
            }
            Users[Users.IndexOf(new User { UserEmail = CurUser })].UserSurname = CurSurName.Text;
            Users[Users.IndexOf(new User { UserEmail = CurUser })].Age = CurAge.Text;
        }
        public void Settings(object sender, RoutedEventArgs args)
        {
            AllFalse();
            Profile.IsVisible = true;
            CurName.Text = GetName;
            CurSurName.Text = GetSurname;
            CurEmail.Text = GetUserEmail;
            CurAge.Text = GetAge;
            CurRegDate.Text = GetDate;
        }
        public void MainPage(object sender, RoutedEventArgs args)
        {
            AllFalse();
            BugPage.IsVisible = true;
        }
        public void QiutButton(object sender, RoutedEventArgs args)
        {
            AllFalse();
            CurUser = null;
            Pane.IsVisible = false;
            Reg.IsVisible = true;
        }
    }
}