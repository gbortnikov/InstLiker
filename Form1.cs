using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace InstLiker
{

    public partial class Form1 : Form
    {
        bool Start = false;
        public Form1()
        {
            InitializeComponent();
        }
    
        void Send(string Text)
        {
            richTextBox1.AppendText($"{Text}");

        }
        
        void Check()
        {
            if (loginTextBox.Text.Length == 0)
            {
                Send("Проверьте логин");
            }
            else if (passwordTextBox.Text.Length == 0)
            {
                Send("Проверьте пароль");
            }
            else
                Start = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Check();
            if(Start == true)
            {
                //Send(inst.Auth(loginTextBox.Text, passwordTextBox.Text));
                string response = inst.Auth(loginTextBox.Text, passwordTextBox.Text);
                var json = JsonConvert.DeserializeObject<Auth>(response);
                if (json.authenticated == true)
                {
                    Send("Авторизация прошла успешно");
                }
            }
        }





    }
}
