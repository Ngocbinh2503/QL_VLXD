using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using System.Data.SqlClient;

namespace GUI
{
    public partial class frDangNhap : Form
    {
        TaiKhoanDTO taikhoan = new TaiKhoanDTO();
        TaiKhoanBLL tkbll = new TaiKhoanBLL();
        public frDangNhap()
        {
            InitializeComponent();
        }

        //private void btnDangNhap_Click(object sender, EventArgs e)
        //{
        //    taikhoan.USERNAME = txtusername.Text;
        //    taikhoan.PASSWORD = txtpassword.Text;
        //    taikhoan.USERNAME = "caotri55";
        //    taikhoan.PASSWORD = "caotri551";
        //    string getuser = tkbll.checklogin(taikhoan);
        //    switch (getuser)
        //    {
        //        case "requeid_taikhoan":
        //            MessageBox.Show("Không được để username trống");
        //            return;

        //        case "requeid_password":
        //            MessageBox.Show("Không được để password trống");
        //            return;
        //        case "Tài khoản hoặc mật khẩu không chính xác":
        //            MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác");
        //            return;
        //    }

        //    int quyen = tkbll.Checkquyen(taikhoan);
        //    DataRow nguoiDung = tkbll.GetDataByID(getuser).Rows[0];
        //    string maND = nguoiDung["MANV"].ToString();

        //    if (quyen == 0)
        //    {
        //        MessageBox.Show("Dăng nhập thành công dưới quyen admin");
        //        frm_Main fm = new frm_Main(maND, quyen);
        //        fm.BringToFront();
        //        fm.Show();
        //        this.Hide();
        //    }
        //    else if (quyen == 1)
        //    {
        //        MessageBox.Show("Dăng nhập  thành công dưới quyền nhân viên");
        //        frm_Main fc = new frm_Main(maND, quyen);
        //        fc.BringToFront();
        //        fc.Show();
        //        this.Hide();
        //    }
        //}
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu SQL Server của bạn
            string connectionString = @"Data Source=NGOCBINH\SQLEXPRESS;Initial Catalog=YourDatabaseName;Integrated Security=True";

            // Truy vấn SQL để kiểm tra thông tin đăng nhập
            string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";

            // Kết nối đến cơ sở dữ liệu SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo lệnh truy vấn
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số để tránh SQL Injection
                        command.Parameters.AddWithValue("@username", txtusername.Text);
                        command.Parameters.AddWithValue("@password", txtpassword.Text);

                        // Thực hiện truy vấn và lấy kết quả
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Kiểm tra nếu có dữ liệu trả về
                            if (dataTable.Rows.Count > 0)
                            {
                                // Đăng nhập thành công
                                MessageBox.Show("Đăng nhập thành công!");
                            }
                            else
                            {
                                // Không có tài khoản phù hợp
                                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ
                    MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }
    }
}
