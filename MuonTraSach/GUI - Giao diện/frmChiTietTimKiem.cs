using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Dashboard_Search.BLL;

namespace Dashboard_Search.GUI
{
    public partial class frmChiTietTimKiem : Form
    {
        private string phanLoai;
        private string maDoiTuong;
        private SearchBLL bll = new SearchBLL();

        public frmChiTietTimKiem(string phanLoai, string maDoiTuong)
        {
            InitializeComponent();
            this.phanLoai = phanLoai;
            this.maDoiTuong = maDoiTuong;
            btnDong.Click += (s, e) => this.Close();
            this.Load += (s, e) => {
                lblTitle.Text = "CHI TIẾT " + phanLoai.ToUpper();
                LoadDuLieu();
            };
        }

        private void LoadDuLieu()
        {
            DataTable dt = bll.LayChiTiet(phanLoai, maDoiTuong);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                int yPos = 20;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Label lbl = new Label() { Text = dt.Columns[i].ColumnName + ":", Location = new Point(30, yPos), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
                    TextBox txt = new TextBox() { Text = row[i].ToString(), Location = new Point(180, yPos - 3), Size = new Size(270, 32), ReadOnly = true };
                    pnlContent.Controls.Add(lbl);
                    pnlContent.Controls.Add(txt);
                    yPos += 45;
                }
            }
        }
    }
}