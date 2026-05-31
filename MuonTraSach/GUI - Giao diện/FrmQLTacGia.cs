using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MuonTraSach.BLL;
using MuonTraSach.DTO;

namespace MuonTraSach.GUI
{
    public partial class FrmQLTacGia : Form
    {
        public FrmQLTacGia()
        {
            InitializeComponent();
            this.Load += FrmQLTacGia_Load;
        }

        private void lblThongTin_Click(object sender, EventArgs e)
        {

        }

        private void txtMaTacGia_TextChanged(object sender, EventArgs e)
        {

        }
        private readonly TacGiaBLL tacGiaBLL = new TacGiaBLL();

        private int currentPage = 1;
        private int pageSize = 10;
        private int totalRows = 0;
        private int totalPages = 1;


        private void FrmQLTacGia_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDanhSach();
        }

        private void LoadComboBox()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Active");
            cboTrangThai.Items.Add("Inactive");
            cboTrangThai.SelectedIndex = 0;

            cboLocChuCai.Items.Clear();
            cboLocChuCai.Items.Add("Tất cả");
            cboLocChuCai.Items.Add("A");
            cboLocChuCai.Items.Add("B");
            cboLocChuCai.Items.Add("C");
            cboLocChuCai.Items.Add("D");
            cboLocChuCai.Items.Add("Đ");
            cboLocChuCai.Items.Add("E");
            cboLocChuCai.Items.Add("G");
            cboLocChuCai.Items.Add("H");
            cboLocChuCai.Items.Add("K");
            cboLocChuCai.Items.Add("L");
            cboLocChuCai.Items.Add("M");
            cboLocChuCai.Items.Add("N");
            cboLocChuCai.Items.Add("P");
            cboLocChuCai.Items.Add("Q");
            cboLocChuCai.Items.Add("S");
            cboLocChuCai.Items.Add("T");
            cboLocChuCai.Items.Add("V");
            cboLocChuCai.Items.Add("X");
            cboLocChuCai.Items.Add("Y");
            cboLocChuCai.SelectedIndex = 0;
        }

        private void LoadDanhSach()
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            string chuCai = LayChuCaiLoc();

            totalRows = tacGiaBLL.DemSoDong(tuKhoa, chuCai);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            if (totalPages == 0)
            {
                totalPages = 1;
            }

            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            dgvTacGia.DataSource = tacGiaBLL.LayDanhSach(tuKhoa, chuCai, currentPage, pageSize);

            lblTrang.Text = $"Trang {currentPage}/{totalPages} - Tổng {totalRows} bản ghi";

            DinhDangDataGridView();
        }

        private void DinhDangDataGridView()
        {
            dgvTacGia.ColumnHeadersVisible = true;
            dgvTacGia.EnableHeadersVisualStyles = false;
            dgvTacGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTacGia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTacGia.MultiSelect = false;
            dgvTacGia.ReadOnly = true;
            dgvTacGia.AllowUserToAddRows = false;
            dgvTacGia.RowHeadersVisible = false;

            dgvTacGia.ColumnHeadersHeight = 40;
            dgvTacGia.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 64, 175);
            dgvTacGia.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTacGia.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTacGia.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTacGia.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvTacGia.DefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235);
            dgvTacGia.DefaultCellStyle.SelectionForeColor = Color.White;

            if (dgvTacGia.Columns["MaTacGia"] != null)
                dgvTacGia.Columns["MaTacGia"].HeaderText = "Mã tác giả";

            if (dgvTacGia.Columns["TenTacGia"] != null)
                dgvTacGia.Columns["TenTacGia"].HeaderText = "Tên tác giả";

            if (dgvTacGia.Columns["GhiChu"] != null)
                dgvTacGia.Columns["GhiChu"].HeaderText = "Ghi chú";

            if (dgvTacGia.Columns["TrangThai"] != null)
                dgvTacGia.Columns["TrangThai"].HeaderText = "Trạng thái";
        }

        private TacGiaDTO LayDuLieuTuForm()
        {
            return new TacGiaDTO
            {
                MaTacGia = txtMaTacGia.Text.Trim(),
                TenTacGia = txtTenTacGia.Text.Trim(),
                GhiChu = txtGhiChu.Text.Trim(),
                TrangThai = cboTrangThai.Text == "Active"
            };
        }

        private void LamMoiForm()
        {
            txtMaTacGia.Clear();
            txtTenTacGia.Clear();
            txtGhiChu.Clear();

            if (cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }

            txtMaTacGia.Enabled = true;
            txtMaTacGia.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                TacGiaDTO tacGia = LayDuLieuTuForm();

                string message;
                bool result = tacGiaBLL.Them(tacGia, out message);

                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                    result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (result)
                {
                    LamMoiForm();
                    LoadDanhSach();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tác giả:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                TacGiaDTO tacGia = LayDuLieuTuForm();

                string message;
                bool result = tacGiaBLL.Sua(tacGia, out message);

                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                    result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (result)
                {
                    LamMoiForm();
                    LoadDanhSach();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tác giả:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                TacGiaDTO tacGia = LayDuLieuTuForm();

                string message;
                bool result;

                if (txtMaTacGia.Enabled)
                {
                    result = tacGiaBLL.Them(tacGia, out message);
                }
                else
                {
                    result = tacGiaBLL.Sua(tacGia, out message);
                }

                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                    result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (result)
                {
                    LamMoiForm();
                    LoadDanhSach();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu tác giả:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNgungHoatDong_Click(object sender, EventArgs e)
        {
            string maTacGia = txtMaTacGia.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTacGia))
            {
                MessageBox.Show("Vui lòng chọn tác giả cần ngừng hoạt động.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn ngừng hoạt động tác giả này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            string message;
            bool result = tacGiaBLL.NgungHoatDong(maTacGia, out message);

            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (result)
            {
                LamMoiForm();
                LoadDanhSach();
            }
        }

        private void btnHoatDong_Click(object sender, EventArgs e)
        {
            string maTacGia = txtMaTacGia.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTacGia))
            {
                MessageBox.Show("Vui lòng chọn tác giả cần kích hoạt lại.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn kích hoạt lại tác giả này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            string message;
            bool result = tacGiaBLL.HoatDong(maTacGia, out message);

            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (result)
            {
                LamMoiForm();
                LoadDanhSach();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadDanhSach();
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadDanhSach();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadDanhSach();
            }
        }

        private void dgvTacGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvTacGia.Rows[e.RowIndex];

            txtMaTacGia.Text = row.Cells["MaTacGia"].Value?.ToString();
            txtTenTacGia.Text = row.Cells["TenTacGia"].Value?.ToString();
            txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();

            if (row.Cells["TrangThai"].Value != null)
            {
                bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                cboTrangThai.Text = trangThai ? "Active" : "Inactive";
            }

            txtMaTacGia.Enabled = false;
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
    
            string maTacGia = txtMaTacGia.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTacGia))
            {
                MessageBox.Show("Vui lòng chọn tác giả cần xóa.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa tác giả này không?\nLưu ý: Tác giả đã có tài liệu liên quan sẽ không thể xóa.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            try
            {
                string message;
                bool result = tacGiaBLL.Xoa(maTacGia, out message);

                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                    result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (result)
                {
                    LamMoiForm();
                    LoadDanhSach();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa tác giả:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmQLTacGia_Load_1(object sender, EventArgs e)
        {

        }
        private string LayChuCaiLoc()
        {
            if (cboLocChuCai.SelectedItem == null)
            {
                return "";
            }

            string value = cboLocChuCai.SelectedItem.ToString();

            if (value == "Tất cả")
            {
                return "";
            }

            return value;
        }
    }
}
