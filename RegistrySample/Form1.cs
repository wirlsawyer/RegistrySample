using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace RegistrySample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String _class = RegistryHelp.ReadRegistryKey(@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Class\{4D36E96C-E325-11CE-BFC1-08002BE10318}", "Class");
            Console.WriteLine(_class);

            IDictionary dict = new Dictionary<String, Object>();
            RegistryHelp.ReadRegistryKey(@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Class\{4D36E96C-E325-11CE-BFC1-08002BE10318}", dict);
            foreach (String key in dict.Keys)
            {
                Console.WriteLine("key:" + key + " Value:" + dict[key].ToString());
            }
        }
    }
}
