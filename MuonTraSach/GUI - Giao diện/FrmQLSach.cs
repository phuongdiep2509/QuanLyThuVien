using MuonTraSach.BLL;
using MuonTraSach.DTO;
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

namespace MuonTraSach.GUI
{
    public partial class FrmQLSach : Form
    {
        private readonly TaiLieuBLL taiLieuBLL = new TaiLieuBLL();
        private readonly TacGiaBLL tacGiaBLL = new TacGiaBLL();
        private readonly TheLoaiBLL theLoaiBLL = new TheLoaiBLL();

        private int currentPage = 1;
        private int pageSize = 10;
        private int totalRows = 0;
        private int totalPages = 1;

        public FrmQLSach()
        {
            InitializeComponent();
            txtSoLuongHienCo.KeyPress += ChiChoNhapSo_KeyPress;
            txtSoLuongConLai.KeyPress += ChiChoNhapSo_KeyPress;
        }

        private void FrmQLSach_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDanhSach();
        }

        private void LoadComboBox()
        {
            cboTacGia.DataSource = tacGiaBLL.LayDangHoatDong();
            cboTacGia.DisplayMember = "TenTacGia";
            cboTacGia.ValueMember = "MaTacGia";
            cboTacGia.SelectedIndex = -1;

            cboTheLoai.DataSource = theLoaiBLL.LayDangHoatDong();
            cboTheLoai.DisplayMember = "TenTheLoai";
            cboTheLoai.ValueMember = "MaTheLoai";
            cboTheLoai.SelectedIndex = -1;

            var dsTheLoaiLoc = theLoaiBLL.LayDangHoatDong();
            dsTheLoaiLoc.Insert(0, new TheLoaiDTO
            {
                MaTheLoai = "",
                TenTheLoai = "Tất cả"
            });

            cboLocTheLoai.DataSource = dsTheLoaiLoc;
            cboLocTheLoai.DisplayMember = "TenTheLoai";
            cboLocTheLoai.ValueMember = "MaTheLoai";
            cboLocTheLoai.SelectedIndex = 0;

            cboTinhTrang.Items.Clear();
            cboTinhTrang.Items.Add("Bình thường");
            cboTinhTrang.Items.Add("Hư hỏng");
            cboTinhTrang.Items.Add("Mất");
            cboTinhTrang.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Active");
            cboTrangThai.Items.Add("Inactive");
            cboTrangThai.SelectedIndex = 0;

            cboLocTrangThai.Items.Clear();
            cboLocTrangThai.Items.Add("Tất cả");
            cboLocTrangThai.Items.Add("Active");
            cboLocTrangThai.Items.Add("Inactive");
            cboLocTrangThai.SelectedIndex = 0;
        }

        private bool? LayTrangThaiLoc()
        {
            if (cboLocTrangThai.SelectedItem == null)
            {
                return null;
            }

            string value = cboLocTrangThai.SelectedItem.ToString();

            if (value == "Active")
            {
                return true;
            }

            if (value == "Inactive")
            {
                return false;
            }

            return null;
        }

        private void LoadDanhSach()
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            bool? trangThai = LayTrangThaiLoc();
            string maTheLoai = LayMaTheLoaiLoc();

            totalRows = taiLieuBLL.DemSoDong(tuKhoa, trangThai, maTheLoai);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            if (totalPages == 0)
            {
                totalPages = 1;
            }

            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            dgvSach.DataSource = taiLieuBLL.LayDanhSach(tuKhoa, trangThai, maTheLoai, currentPage, pageSize);

            lblTrang.Text = $"Trang {currentPage}/{totalPages} - Tổng {totalRows} bản ghi";

            DinhDangDataGridView();
        }

        private void DinhDangDataGridView()
        {
            if (dgvSach.Columns["MaTaiLieu"] != null)
                dgvSach.Columns["MaTaiLieu"].HeaderText = "Mã sách";

            if (dgvSach.Columns["TenTaiLieu"] != null)
                dgvSach.Columns["TenTaiLieu"].HeaderText = "Tên sách";

            if (dgvSach.Columns["LoaiTaiLieu"] != null)
                dgvSach.Columns["LoaiTaiLieu"].HeaderText = "Loại tài liệu";

            if (dgvSach.Columns["TenTacGia"] != null)
                dgvSach.Columns["TenTacGia"].HeaderText = "Tác giả";

            if (dgvSach.Columns["TenTheLoai"] != null)
                dgvSach.Columns["TenTheLoai"].HeaderText = "Thể loại";

            if (dgvSach.Columns["NamXuatBan"] != null)
                dgvSach.Columns["NamXuatBan"].HeaderText = "Năm XB";

            if (dgvSach.Columns["NhaXuatBan"] != null)
                dgvSach.Columns["NhaXuatBan"].HeaderText = "Nhà XB";

            if (dgvSach.Columns["SoLuongHienCo"] != null)
                dgvSach.Columns["SoLuongHienCo"].HeaderText = "SL hiện có";

            if (dgvSach.Columns["SoLuongConLai"] != null)
                dgvSach.Columns["SoLuongConLai"].HeaderText = "SL còn lại";

            if (dgvSach.Columns["TinhTrang"] != null)
                dgvSach.Columns["TinhTrang"].HeaderText = "Tình trạng";

            if (dgvSach.Columns["TrangThai"] != null)
                dgvSach.Columns["TrangThai"].HeaderText = "Trạng thái";

            if (dgvSach.Columns["AnhBia"] != null)
            {
                dgvSach.Columns["AnhBia"].Visible = false;
            }

            if (dgvSach.Columns["MaTacGia"] != null)
                dgvSach.Columns["MaTacGia"].Visible = false;

            if (dgvSach.Columns["MaTheLoai"] != null)
                dgvSach.Columns["MaTheLoai"].Visible = false;
        }

        private TaiLieuDTO LayDuLieuTuForm()
        {
            int namXuatBan;
            int soLuongHienCo;
            int soLuongConLai;

            int.TryParse(txtNamXuatBan.Text.Trim(), out namXuatBan);
            int.TryParse(txtSoLuongHienCo.Text.Trim(), out soLuongHienCo);
            int.TryParse(txtSoLuongConLai.Text.Trim(), out soLuongConLai);

            return new TaiLieuDTO
            {
                MaTaiLieu = txtMaTaiLieu.Text.Trim(),
                TenTaiLieu = txtTenTaiLieu.Text.Trim(),
                LoaiTaiLieu = "Sách",
                MaTacGia = cboTacGia.SelectedValue == null ? "" : cboTacGia.SelectedValue.ToString(),
                MaTheLoai = cboTheLoai.SelectedValue == null ? "" : cboTheLoai.SelectedValue.ToString(),
                NamXuatBan = string.IsNullOrWhiteSpace(txtNamXuatBan.Text) ? null : (int?)namXuatBan,
                NhaXuatBan = txtNhaXuatBan.Text.Trim(),
                SoLuongHienCo = soLuongHienCo,
                SoLuongConLai = soLuongConLai,
                TinhTrang = cboTinhTrang.Text,
                TrangThai = cboTrangThai.Text == "Active",
                AnhBia = txtAnhBia.Text.Trim()
            };
        }

        private void LamMoiForm()
        {
            txtMaTaiLieu.Clear();
            txtTenTaiLieu.Clear();
            txtNamXuatBan.Clear();
            txtNhaXuatBan.Clear();
            txtSoLuongHienCo.Clear();
            txtSoLuongConLai.Clear();
            txtAnhBia.Clear();

            if (cboTacGia.Items.Count > 0) cboTacGia.SelectedIndex = -1;
            if (cboTheLoai.Items.Count > 0) cboTheLoai.SelectedIndex = -1;
            if (cboTinhTrang.Items.Count > 0) cboTinhTrang.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;

            picAnhBia.Image = null;
            txtMaTaiLieu.Enabled = true;
            txtMaTaiLieu.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            TaiLieuDTO taiLieu = LayDuLieuTuForm();

            string message;
            bool result = taiLieuBLL.Them(taiLieu, out message);

            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (result)
            {
                LamMoiForm();
                LoadDanhSach();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            TaiLieuDTO taiLieu = LayDuLieuTuForm();

            string message;
            bool result = taiLieuBLL.Sua(taiLieu, out message);

            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (result)
            {
                LamMoiForm();
                LoadDanhSach();
            }
        }

        private void btnNgungHoatDong_Click(object sender, EventArgs e)
        {
            string maTaiLieu = txtMaTaiLieu.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTaiLieu))
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần ngừng hoạt động.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn ngừng hoạt động tài liệu này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            string message;
            bool result = taiLieuBLL.NgungHoatDong(maTaiLieu, out message);

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

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvSach.Rows[e.RowIndex];

            txtMaTaiLieu.Text = row.Cells["MaTaiLieu"].Value?.ToString();
            txtTenTaiLieu.Text = row.Cells["TenTaiLieu"].Value?.ToString();
            txtNamXuatBan.Text = row.Cells["NamXuatBan"].Value?.ToString();
            txtNhaXuatBan.Text = row.Cells["NhaXuatBan"].Value?.ToString();
            txtSoLuongHienCo.Text = row.Cells["SoLuongHienCo"].Value?.ToString();
            txtSoLuongConLai.Text = row.Cells["SoLuongConLai"].Value?.ToString();
            cboTinhTrang.Text = row.Cells["TinhTrang"].Value?.ToString();

            bool trangThai = false;
            if (row.Cells["TrangThai"].Value != null)
            {
                bool.TryParse(row.Cells["TrangThai"].Value.ToString(), out trangThai);
            }

            cboTrangThai.Text = trangThai ? "Active" : "Inactive";

            if (row.Cells["MaTacGia"].Value != null)
            {
                cboTacGia.SelectedValue = row.Cells["MaTacGia"].Value.ToString();
            }

            if (row.Cells["MaTheLoai"].Value != null)
            {
                cboTheLoai.SelectedValue = row.Cells["MaTheLoai"].Value.ToString();
            }

            if (dgvSach.Columns.Contains("AnhBia"))
            {
                txtAnhBia.Text = row.Cells["AnhBia"].Value?.ToString();
                HienThiAnh(txtAnhBia.Text);
            }

            txtMaTaiLieu.Enabled = false;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn ảnh bìa sách";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourcePath = openFileDialog.FileName;
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(sourcePath);

                    string folderPath = Path.Combine(Application.StartupPath, "Images");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string destPath = Path.Combine(folderPath, fileName);

                    File.Copy(sourcePath, destPath, true);

                    txtAnhBia.Text = Path.Combine("Images", fileName);
                    HienThiAnh(txtAnhBia.Text);
                }
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            txtAnhBia.Clear();
            picAnhBia.Image = null;
        }

        private void HienThiAnh(string relativePath)
        {
            try
            {
                picAnhBia.Image = null;

                if (string.IsNullOrWhiteSpace(relativePath))
                {
                    return;
                }

                string fullPath = Path.Combine(Application.StartupPath, relativePath);

                if (!File.Exists(fullPath))
                {
                    return;
                }

                using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    picAnhBia.Image = Image.FromStream(stream);
                }
            }
            catch
            {
                picAnhBia.Image = null;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlThongTinSach_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtAnhBia_TextChanged(object sender, EventArgs e)
        {

        }

        private void picAnhBia_Click(object sender, EventArgs e)
        {

        }

        private void cboTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTacGia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSoLuongConLai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoLuongHienCo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNhaXuatBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNamXuatBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenTaiLieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaTaiLieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblThongTin_Click(object sender, EventArgs e)
        {

        }

        private void pnlChucNang_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlTimkiem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboLocTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTrang_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                TaiLieuDTO taiLieu = LayDuLieuTuForm();

                string message;
                bool result;

                if (txtMaTaiLieu.Enabled == true)
                {
                    result = taiLieuBLL.Them(taiLieu, out message);
                }
                else
                {
                    result = taiLieuBLL.Sua(taiLieu, out message);
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
                MessageBox.Show("Lỗi khi lưu tài liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
        "Bạn có chắc muốn đóng form quản lý sách không?",
        "Xác nhận",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnHoatdong_Click(object sender, EventArgs e)
        {
            string maTaiLieu = txtMaTaiLieu.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTaiLieu))
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần kích hoạt lại.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn kích hoạt lại tài liệu này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            string message;
            bool result = taiLieuBLL.HoatDong(maTaiLieu, out message);

            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK,
                result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (result)
            {
                LamMoiForm();
                LoadDanhSach();
            }
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
            string maTaiLieu = txtMaTaiLieu.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTaiLieu))
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần xóa.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa tài liệu này không?\nLưu ý: Tài liệu đã phát sinh trong phiếu mượn sẽ không thể xóa.",
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
                bool result = taiLieuBLL.Xoa(maTaiLieu, out message);

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
                MessageBox.Show("Lỗi khi xóa tài liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ChiChoNhapSo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private string LayMaTheLoaiLoc()
        {
            if (cboLocTheLoai.SelectedValue == null)
            {
                return "";
            }

            return cboLocTheLoai.SelectedValue.ToString();
        }
    }
}
