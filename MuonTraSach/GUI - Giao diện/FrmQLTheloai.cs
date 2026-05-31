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
    public partial class FrmQLTheloai : Form
    {
        public FrmQLTheloai()
        {
            InitializeComponent();
            this.Load += FrmQLTheloai_Load;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmQLTheloai_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDanhSach();
        }
        private readonly TheLoaiBLL theLoaiBLL = new TheLoaiBLL();

        private int currentPage = 1;
        private int pageSize = 10;
        private int totalRows = 0;
        private int totalPages = 1;

        private void LoadComboBox()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Active");
            cboTrangThai.Items.Add("Inactive");
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadDanhSach()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            totalRows = theLoaiBLL.DemSoDong(tuKhoa);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            if (totalPages == 0)
            {
                totalPages = 1;
            }

            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            dgvTheLoai.DataSource = theLoaiBLL.LayDanhSach(tuKhoa, currentPage, pageSize);

            lblTrang.Text = $"Trang {currentPage}/{totalPages} - Tổng {totalRows} bản ghi";

            DinhDangDataGridView();
        }

        private void DinhDangDataGridView()
        {
            dgvTheLoai.ColumnHeadersVisible = true;
            dgvTheLoai.EnableHeadersVisualStyles = false;
            dgvTheLoai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTheLoai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTheLoai.MultiSelect = false;
            dgvTheLoai.ReadOnly = true;
            dgvTheLoai.AllowUserToAddRows = false;
            dgvTheLoai.RowHeadersVisible = false;

            dgvTheLoai.ColumnHeadersHeight = 40;
            dgvTheLoai.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 64, 175);
            dgvTheLoai.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTheLoai.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTheLoai.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTheLoai.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvTheLoai.DefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235);
            dgvTheLoai.DefaultCellStyle.SelectionForeColor = Color.White;

            if (dgvTheLoai.Columns["MaTheLoai"] != null)
                dgvTheLoai.Columns["MaTheLoai"].HeaderText = "Mã thể loại";

            if (dgvTheLoai.Columns["TenTheLoai"] != null)
                dgvTheLoai.Columns["TenTheLoai"].HeaderText = "Tên thể loại";

            if (dgvTheLoai.Columns["MoTa"] != null)
                dgvTheLoai.Columns["MoTa"].HeaderText = "Mô tả";

            if (dgvTheLoai.Columns["TrangThai"] != null)
                dgvTheLoai.Columns["TrangThai"].HeaderText = "Trạng thái";
        }

        private TheLoaiDTO LayDuLieuTuForm()
        {
            return new TheLoaiDTO
            {
                MaTheLoai = txtMaTheLoai.Text.Trim(),
                TenTheLoai = txtTenTheLoai.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cboTrangThai.Text == "Active"
            };
        }

        private void LamMoiForm()
        {
            txtMaTheLoai.Clear();
            txtTenTheLoai.Clear();
            txtMoTa.Clear();

            if (cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }

            txtMaTheLoai.Enabled = true;
            txtMaTheLoai.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                TheLoaiDTO theLoai = LayDuLieuTuForm();

                string message;
                bool result = theLoaiBLL.Them(theLoai, out message);

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
                MessageBox.Show("Lỗi khi thêm thể loại:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                TheLoaiDTO theLoai = LayDuLieuTuForm();

                string message;
                bool result = theLoaiBLL.Sua(theLoai, out message);

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
                MessageBox.Show("Lỗi khi cập nhật thể loại:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                TheLoaiDTO theLoai = LayDuLieuTuForm();

                string message;
                bool result;

                if (txtMaTheLoai.Enabled)
                {
                    result = theLoaiBLL.Them(theLoai, out message);
                }
                else
                {
                    result = theLoaiBLL.Sua(theLoai, out message);
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
                MessageBox.Show("Lỗi khi lưu thể loại:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maTheLoai = txtMaTheLoai.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTheLoai))
            {
                MessageBox.Show("Vui lòng chọn thể loại cần xóa.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa thể loại này không?\nLưu ý: Thể loại đã có tài liệu liên quan sẽ không thể xóa.",
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
                bool result = theLoaiBLL.Xoa(maTheLoai, out message);

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
                MessageBox.Show("Lỗi khi xóa thể loại:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNgungHoatDong_Click(object sender, EventArgs e)
        {
            string maTheLoai = txtMaTheLoai.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTheLoai))
            {
                MessageBox.Show("Vui lòng chọn thể loại cần ngừng hoạt động.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn ngừng hoạt động thể loại này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            string message;
            bool result = theLoaiBLL.NgungHoatDong(maTheLoai, out message);

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
            string maTheLoai = txtMaTheLoai.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTheLoai))
            {
                MessageBox.Show("Vui lòng chọn thể loại cần kích hoạt lại.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn kích hoạt lại thể loại này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            string message;
            bool result = theLoaiBLL.HoatDong(maTheLoai, out message);

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

        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvTheLoai.Rows[e.RowIndex];

            txtMaTheLoai.Text = row.Cells["MaTheLoai"].Value?.ToString();
            txtTenTheLoai.Text = row.Cells["TenTheLoai"].Value?.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();

            if (row.Cells["TrangThai"].Value != null)
            {
                bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                cboTrangThai.Text = trangThai ? "Active" : "Inactive";
            }

            txtMaTheLoai.Enabled = false;
        }
    }

}
