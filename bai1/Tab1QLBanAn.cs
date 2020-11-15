using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bai1
{
    public partial class Tab1QLBanAn : UserControl
    {
        string CurrentProjectD;
        public Tab1QLBanAn()
        {
            InitializeComponent();
            //Lấy file path mà chương trình chạy(VD:C:\...\bai1\bin\debug
            string CurrentDirectory = System.Environment.CurrentDirectory;
            //Lấy file path đã bỏ đi bin\debug -> Đây là địa chỉ của toàn bộ project
            CurrentProjectD = Directory.GetParent(CurrentDirectory).Parent.FullName;
        }

        private void gunaShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {

        }

        private void Tab1QLBanAn_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaShadowPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFBMoBan_Click(object sender, EventArgs e)
        {
            
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + CurrentProjectD + @"\Database\NHAHANG.db;"))
            {
                conn.Open();
                string date = mTBNgay.Text;
            }
        }
    }
}
