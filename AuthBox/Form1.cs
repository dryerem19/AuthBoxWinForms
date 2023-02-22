using System.Windows.Forms;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Text;
using System.Resources;

namespace AuthBox
{
    public partial class Window : Form
    {
        bool isForm = false;
        string password = "";

        string oldPass = "";
        string newPass = "";

        string file = @"..\..\Resources\password.txt";

        public Window()
        {
            InitializeComponent();
        }

        void WindowLoad(object sender, EventArgs e)
        {
            Encoding UTF8Encoding = Encoding.GetEncoding(1251);
            StreamReader stream = new StreamReader(file, UTF8Encoding);
            if (File.Exists(file))
            {
                password = stream.ReadLine();
                stream.Close();
            }

            for (int i = 2; i >= 0; i--)
            {
                string input = Interaction.InputBox("Введите пароль для входа в систему", "Авторизация");
                if (input == "")
                {
                    DialogResult result = MessageBox.Show("Вы не ввели пароль! Желаете выйти?", "Внимание!", MessageBoxButtons.YesNo);
                    if (DialogResult.Yes == result)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        if (i == 0)
                        {
                            MessageBox.Show("Аккаунт заблокирован! Обратитесь к администратору!");
                            Close();
                            Application.Exit();
                        }
                        else
                            MessageBox.Show("Вы неправильно ввели пароль! У вас осталось " + i + " попыток");
                    }
                }
                else if (input == password)
                {
                    Show();
                    isForm = true;
                    break;
                }
                else
                {
                    if (i == 0)
                    {
                        MessageBox.Show("Аккаунт заблокирован! Обратитесь к администратору!");
                        Close();
                        Application.Exit();
                    }    
                    else
                        MessageBox.Show("Вы неправильно ввели пароль! У вас осталось " + i + " попыток");
                }
            }
        }

        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            if (isForm)
            {
                DialogResult result = MessageBox.Show("Вы хотите сменить пароль?", "Выход",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    oldPass = Interaction.InputBox("Введите старый пароль");
                    if (oldPass == password)
                    {
                        newPass = Interaction.InputBox("Введите новый пароль");
                        StreamWriter stream = new StreamWriter(file);
                        stream.WriteLine(newPass);
                        stream.Close();
                        MessageBox.Show("Пароль успешно изменен!");
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают!");
                    }
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MessageBox.Show(listBox1.SelectedItem.ToString() + " начислена зарплата в размере " + textBox1.Text);
            }
            else
            {
                MessageBox.Show("Вы не ввели сумму перевода!");
            }
        }
    }
}