using System;
using System.Data;
using System.Windows.Forms;
using MuonTraSach.BLL;

namespace MuonTraSach.GUI
{
    public partial class frmSearch : Form
    {
        private SearchBLL bll = new SearchBLL();

        public frmSearch()
        {
            InitializeComponent();
            btnTimKiem.Click += BtnTimKiem_Click;
            txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnTimKiem_Click(null, null); };
            dgvKetQua.CellDoubleClick += DgvKetQua_CellDoubleClick;
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text.Trim())) return;
            DataTable dt = bll.TimKiem(txtTimKiem.Text.Trim());
            dgvKetQua.DataSource = dt;
        }

        private void DgvKetQua_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string phanLoai = dgvKetQua.Rows[e.RowIndex].Cells["PhanLoai"].Value.ToString();
                string ma = dgvKetQua.Rows[e.RowIndex].Cells["MaDoiTuong"].Value.ToString();
                new frmChiTietTimKiem(phanLoai, ma).ShowDialog();
            }
        }
    }
}
