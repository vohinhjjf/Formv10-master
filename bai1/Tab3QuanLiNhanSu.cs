using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace bai1
{
    public partial class Tab3QuanLiNhanSu : UserControl
    {
        string CurrentProjectD;
        public Tab3QuanLiNhanSu()
        {
            InitializeComponent();
            //Lấy file path mà chương trình chạy(VD:C:\...\bai1\bin\debug
            string CurrentDirectory = System.Environment.CurrentDirectory;
            //Lấy file path đã bỏ đi bin\debug -> Đây là địa chỉ của toàn bộ project
            CurrentProjectD = Directory.GetParent(CurrentDirectory).Parent.FullName;
        }
        private void Tab3QuanLiNhanSu_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }
        public void RefreshTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + CurrentProjectD + @"\Database\NHAHANG.db;"))
            {
                conn.Open();
                string sql = "select * from NhanVien";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader dta = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        DataTableReader dada = dt.CreateDataReader();

                        dt.Load(dta);
                        //dta.Close();
                        gunaDataGridView1.DataSource = dt;
                        for (int i = 0; i < 7; i++) gunaDataGridView1.AutoResizeColumn(i);
                    }
                }
            }
        }

        private string GTCheck()
        {
            if (rdNam.Checked)
            {
                return "Nam";
            }
            return "Nữ";
        }

        private void ClearThongTin()
        {
            txtMaNhanVien.Text = "";
            txtHoTen.Text = "";
            dtpNgaySinh.Text = "";
            rdNam.Checked = true;
            txtSoDienThoai.Text = "";
            txtChucVu.Text = "";
            txtBacLuong.Text = "";
            rtxtDiaChi.Text = "";
        }
        
        //Button Thêm
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (rtxtDiaChi.Text == "" || txtBacLuong.Text == "" || txtChucVu.Text == "" || txtSoDienThoai.Text == "" || txtHoTen.Text == "" || txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin ...");

            }
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + CurrentProjectD + @"\Database\NHAHANG.db;"))
                {
                    conn.Open();
                    string date = dtpNgaySinh.Text;
                    string sql = $" INSERT INTO NhanVien ([Địa chỉ],Lương,[Chức vụ],[Số điện thoại],[Giới tính],[Ngày sinh],[Họ và tên],[Mã NV]) " +
                        $"VALUES('{rtxtDiaChi.Text}',{txtBacLuong.Text},'{txtChucVu.Text}',{txtSoDienThoai.Text},'{GTCheck()}','{date}','{txtHoTen.Text}','{txtMaNhanVien.Text}'); ";
                    ClearThongTin();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                        RefreshTable();
                        MessageBox.Show("Thêm Thành Công");
                    }
                }
            }
        }

        //Button Cập Nhật
        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count > 0 && txtMaNhanVien.Text != "")
            {
                string MSNV = txtMaNhanVien.Text;
                string fullname = txtHoTen.Text;
                string birthday = dtpNgaySinh.Text;
                string sex = "";
                if (rdNam.Checked)
                {
                    sex = "Nam";
                }   
                else if (rdNu.Checked)
                {
                    sex = "Nữ";
                }    
                string phone = txtSoDienThoai.Text;
                string position = txtChucVu.Text;
                string salary = txtBacLuong.Text;
                string address = rtxtDiaChi.Text;

                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + CurrentProjectD + @"\Database\NHAHANG.db;");
                conn.Open();
                string strUpdate = string.Format("UPDATE NhanVien set [Họ và tên]='{0}', [Ngày sinh]='{1}', [Giới tính]='{2}', [Số điện thoại]='{3}', [Chức vụ]='{4}', Lương='{5}', [Địa chỉ]='{6}' where [Mã NV]='{7}'", fullname, birthday, sex, phone, position, salary, address, MSNV);

                SQLiteCommand cmd = new SQLiteCommand(strUpdate, conn);
                cmd.ExecuteNonQuery();
                RefreshTable();
                MessageBox.Show("Cập Nhật Thành Công!");
                ClearThongTin();
                conn.Close();
            }
        }

        //Button Xóa
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + CurrentProjectD + @"\Database\NHAHANG.db;");
            conn.Open();
            DialogResult tb = MessageBox.Show("Bạn Có Chắc Chắn Muốn Xóa ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tb == DialogResult.Yes)
            {
                string strDelate = string.Format("DELETE FROM NhanVien where [Mã NV] ='{0}'", txtMaNhanVien.Text);
                SQLiteCommand cmd = new SQLiteCommand(strDelate, conn);
                ClearThongTin();
                cmd.ExecuteNonQuery();
            }
            RefreshTable();
            MessageBox.Show("Xóa Thành Công");
            conn.Close();
        }


        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Load dữ liệu từ DataGridView lên Textbox
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtMaNhanVien.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Mã NV"].Value);
                txtHoTen.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Họ và tên"].Value);
                dtpNgaySinh.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Ngày sinh"].Value);
                string str = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Giới tính"].Value);
                if (str == "Nam")
                    rdNam.Checked = true;
                else
                    rdNu.Checked = true;
                txtSoDienThoai.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Số điện thoại"].Value);
                txtChucVu.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Chức vụ"].Value);
                txtHoTen.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Họ Và Tên"].Value);
                txtBacLuong.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Lương"].Value);
                rtxtDiaChi.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["Địa chỉ"].Value);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gunaShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
