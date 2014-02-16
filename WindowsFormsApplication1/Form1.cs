using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Key key = new Key();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            key.CDKey = textBox1.Text;
            textBox2.Text = key.HexData;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey rkey = localMachine64.OpenSubKey(@"SOFTWARE\Wow6432Node\bohemia interactive studio\arma 2", true);

            key.CDKey = textBox1.Text;
            textBox2.Text = key.HexData;

            if (key.HexData != "")
            {
                rkey.SetValue("key", key.ByteData, RegistryValueKind.Binary);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("Wrong Serial format.");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistryKey localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey rkey = localMachine64.OpenSubKey(@"SOFTWARE\Wow6432Node\bohemia interactive studio\arma 2 oa", true);

            key.CDKey = textBox1.Text;
            textBox2.Text = key.HexData;
            if (key.HexData != "")
            {
                rkey.SetValue("key", key.ByteData, RegistryValueKind.Binary);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("Wrong Serial format.");
            }
        }

    }
}
