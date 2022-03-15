using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using system.data;
using system.Data.sqlclient;
using system.configuration;

namespace SmartDeviceProject2inC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aHGroupDataSet.AbbrevTable' table. You can move, or remove it, as needed.
            this.abbrevTableTableAdapter.Fill(this.aHGroupDataSet.AbbrevTable);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconn
            string sConstra = "Data Source=SSPLSERVER;Initial Catalog=AHGroup_SSPL;Persist Security Info=True;User ID=erp;Password=KHLIerp1234;Connect Timeout=15";
            SqlConnection sCon = new SqlConnection(sConstra);
            sCon.Open();
        }
    }
}