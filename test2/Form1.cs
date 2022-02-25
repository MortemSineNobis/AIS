using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = File.ReadAllText(@"G:\7.txt");
            JSONDecode.Test(s);
            //var a = new AidToNavigationReport();
            //a.type = 100;
            //a.mmsi = "12312312";
            //Decoded d = a;
            //Console.WriteLine($"{a.type} -> {d.type}");
            //Console.WriteLine($"{a.mmsi} -> {d.mmsi}");
        }
    }
}
