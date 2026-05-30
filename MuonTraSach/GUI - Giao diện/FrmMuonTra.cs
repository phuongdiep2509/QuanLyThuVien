using MuonTraSach.BLL_check_thông_tin;
using MuonTraSach.DTO_chứa_dữ_liệu;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using static MuonTraSach.DTO_chứa_dữ_liệu.PhieuMuonDTO;

namespace MuonTraSach.GUI___Giao_diện
{
    public partial class FrmMuonTra : Form
    {
        private readonly PhieuMuonBLL bll = new PhieuMuonBLL();
        private readonly List<ChiTietPhieuMuonDTO> gioHang = new List<ChiTietPhieuMuonDTO>();

        // Trạng thái phiếu đang được hiển thị trên form (null = đang tạo mới)
        private string _trangThaiHienTai = null;
        private DateTime _hanTraHienTai;
        public FrmMuonTra()
        {
            InitializeComponent();
        }

        private void FrmMuonTra_Load(object sender, EventArgs e)
        {
            NapCboTrangThai();
            SetupDgvGioHang();
            ApDungPhanQuyen();
            ResetForm();
            LoadDgvTimKiem();
        }
        private void NapCboTrangThai()
        {
            cboTrangthai.DataSource = TrangThai.TatCa;
            cboTrangthai.SelectedIndex = -1;
        }
        //  PHÂN QUYỀN
        // ═══════════════════════════════════════════════

        private void ApDungPhanQuyen()
        {
            bool isNV = UserSession.IsNhanVienOrAdmin;

            // ── Nút nghiệp vụ ───────────────────────────────────
            // NV/Admin: đủ hết; DocGia: ẩn nút Duyệt, Từ chối, Trả sách
            btnDuyet.Visible = isNV;
            btnTuchoi.Visible = isNV;
            btnTra.Visible = isNV;

            // ── Textbox ─────────────────────────────────────────
            if (isNV)
            {
                txtManhanvien.Text = UserSession.MaNguoiDung;
                txtManhanvien.ReadOnly = true;
                txtMadocgia.ReadOnly = false;
                rdoDocgia.Visible = true;
            }
            else
            {
                txtMadocgia.Text = UserSession.MaNguoiDung;
                txtMadocgia.ReadOnly = true;
                txtManhanvien.Text = "";
                txtManhanvien.ReadOnly = true;
                rdoDocgia.Visible = false;
                rdoTaiLieu.Checked = true;
            }

            // Mã PM luôn readonly
            txtMaPM.ReadOnly = true;

            // Trạng thái: độc giả không được sửa trực tiếp
            cboTrangthai.Enabled = isNV;
        }

        /// <summary>Cập nhật enable/disable từng nút tùy trạng thái phiếu đang chọn.</summary>
        private void CapNhatTrangThaiNut()
        {
            bool coPhieu = !string.IsNullOrEmpty(txtMaPM.Text);
            bool isNV = UserSession.IsNhanVienOrAdmin;
            string tt = _trangThaiHienTai;

            // Gia hạn: chỉ khi đang mượn, chưa gia hạn, chưa quá hạn
            btnGiahan.Enabled = coPhieu
                && tt == TrangThai.DangMuon
                && _hanTraHienTai.Date >= DateTime.Today;

            // Hủy: chờ duyệt hoặc đã duyệt
            btnHuybo.Enabled = !coPhieu
                || tt == TrangThai.ChoDuyet
                || tt == TrangThai.DaDuyet;

            // Cập nhật: DocGia chỉ khi chờ duyệt; NV luôn được
            btnCapnhat.Enabled = coPhieu
                && (isNV || tt == TrangThai.ChoDuyet);

            // Duyệt / Từ chối: NV, khi đang chờ duyệt
            if (isNV)
            {
                btnDuyet.Enabled = coPhieu && tt == TrangThai.ChoDuyet;
                btnTuchoi.Enabled = coPhieu
                    && (tt == TrangThai.ChoDuyet || tt == TrangThai.DaDuyet);
                btnTra.Enabled = coPhieu
                    && (tt == TrangThai.DangMuon || tt == TrangThai.DaGiaHan);
            }
        }
        //  GIỎ HÀNG (dgvChitietmuon)
        // ═══════════════════════════════════════════════

        private void SetupDgvGioHang()
        {
            dgvChitietmuon.AutoGenerateColumns = false;
            dgvChitietmuon.Columns.Clear();

            dgvChitietmuon.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colMa", HeaderText = "Mã TL", DataPropertyName = "MaTaiLieu", Width = 80 });
            dgvChitietmuon.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colTen", HeaderText = "Tên TL", DataPropertyName = "TenTaiLieu", Width = 200, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvChitietmuon.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colLoai", HeaderText = "Loại", DataPropertyName = "LoaiTaiLieu", Width = 100 });
            dgvChitietmuon.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colSL", HeaderText = "Số lượng", DataPropertyName = "SoLuong", Width = 80 });
            dgvChitietmuon.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colTT", HeaderText = "Tình trạng", DataPropertyName = "TinhTrang", Width = 90 });

            // Nút Xóa — chỉ hiện khi đang tạo phiếu mới
            var btnXoa = new DataGridViewButtonColumn
            {
                Name = "colXoa",
                HeaderText = "Xóa",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvChitietmuon.Columns.Add(btnXoa);
        }

        private void RefreshGioHang()
        {
            dgvChitietmuon.DataSource = null;
            dgvChitietmuon.DataSource = gioHang;

            // Nút Xóa chỉ hiện khi đang tạo phiếu mới (chưa có MaPM)
            bool dangTaoMoi = string.IsNullOrEmpty(txtMaPM.Text);
            if (dgvChitietmuon.Columns.Contains("colXoa"))
                dgvChitietmuon.Columns["colXoa"].Visible = dangTaoMoi;
        }
        //  RESET FORM
        // ═══════════════════════════════════════════════

        private void ResetForm()
        {
            txtMaPM.Clear();
            if (UserSession.IsNhanVienOrAdmin)
                txtMadocgia.Clear();
            // txtManhanvien không clear — giữ MaNV đăng nhập

            int maxDays = GetMaxDays();
            dtimeNgaymuon.Value = DateTime.Today;
            dtimeNgaytra.Value = DateTime.Today.AddDays(maxDays);

            cboTrangthai.SelectedIndex = -1;
            _trangThaiHienTai = null;

            gioHang.Clear();
            RefreshGioHang();
            CapNhatTrangThaiNut();
        }

        private int GetMaxDays()
        {
            if (UserSession.IsNhanVienOrAdmin) return 10;
            return (UserSession.LoaiDocGia?.Contains("CBGV") == true
                    || UserSession.LoaiDocGia?.Contains("Giảng viên") == true) ? 10 : 8;
        }
        private void btnMuon_Click(object sender, EventArgs e)
        {
            var pm = new PhieuMuonDTO
            {
                MaDG = txtMadocgia.Text.Trim(),
                MaNV = UserSession.IsNhanVienOrAdmin ? txtManhanvien.Text.Trim() : null,
                NgayMuon = dtimeNgaymuon.Value,
                NgayTra = dtimeNgaytra.Value,
                DanhSachChiTiet = gioHang.ToList()
            };

            string result = bll.XuLyLuuPhieu(pm);
            if (result.StartsWith("OK:"))
            {
                string maMoi = result.Substring(3);
                MessageBox.Show($"Lưu phiếu mượn thành công!\nMã phiếu: {maMoi}", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                if (rdoPhieuMuon.Checked) LoadDgvTimKiem();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGhilai_Click(object sender, EventArgs e)
        {
            ResetForm();
            MessageBox.Show("Đã làm mới form.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGiahan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPM.Text)) { MessageBox.Show("Chưa chọn phiếu mượn!"); return; }

            string result = bll.GiaHanPhieu(txtMaPM.Text, _trangThaiHienTai, _hanTraHienTai);
            if (result.StartsWith("OK:"))
            {
                DateTime hanMoi = DateTime.ParseExact(result.Substring(3), "dd/MM/yyyy", null);
                MessageBox.Show($"Gia hạn thành công!\nHạn trả mới: {hanMoi:dd/MM/yyyy}", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _hanTraHienTai = hanMoi;
                dtimeNgaytra.Value = hanMoi;
                _trangThaiHienTai = TrangThai.DaGiaHan;
                cboTrangthai.SelectedItem = TrangThai.DaGiaHan;
                CapNhatTrangThaiNut();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnTra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPM.Text)) return;
            if (MessageBox.Show("Xác nhận độc giả đã trả đủ sách?", "Xác nhận trả",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string result = bll.TraSach(txtMaPM.Text);
            if (result == "OK")
            {
                MessageBox.Show("Xác nhận trả sách thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _trangThaiHienTai = TrangThai.DaTra;
                cboTrangthai.SelectedItem = TrangThai.DaTra;
                CapNhatTrangThaiNut();
            }
            else
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPM.Text)) return;
            string result = bll.DuyetPhieu(txtMaPM.Text);
            if (result == "OK")
            {
                MessageBox.Show("Đã duyệt và bàn giao sách!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _trangThaiHienTai = TrangThai.DangMuon;
                cboTrangthai.SelectedItem = TrangThai.DangMuon;
                CapNhatTrangThaiNut();
            }
            else
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void btnTuchoi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPM.Text)) return;
            if (MessageBox.Show("Xác nhận từ chối phiếu mượn này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string result = bll.TuChoiPhieu(txtMaPM.Text);
            if (result == "OK")
            {
                MessageBox.Show("Đã từ chối phiếu mượn.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _trangThaiHienTai = TrangThai.TuChoi;
                cboTrangthai.SelectedItem = TrangThai.TuChoi;
                CapNhatTrangThaiNut();
            }
            else
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPM.Text)) return;

            string ttMoi = cboTrangthai.SelectedItem?.ToString() ?? _trangThaiHienTai;
            var pm = new PhieuMuonDTO
            {
                MaPM = txtMaPM.Text,
                MaDG = txtMadocgia.Text,
                MaNV = txtManhanvien.Text,
                NgayMuon = dtimeNgaymuon.Value,
                NgayTra = dtimeNgaytra.Value,
                TrangThai = ttMoi
            };

            string result = bll.CapNhatThongTinPhieu(pm);
            if (result == "OK")
            {
                MessageBox.Show("Cập nhật thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _trangThaiHienTai = ttMoi;
                _hanTraHienTai = dtimeNgaytra.Value;
                CapNhatTrangThaiNut();
            }
            else
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void btnHuybo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPM.Text))
            {
                ResetForm();
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn hủy phiếu mượn này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string result = bll.HuyPhieuMuon(txtMaPM.Text, _trangThaiHienTai);
            if (result == "OK")
            {
                MessageBox.Show("Hủy phiếu thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                LoadDgvTimKiem();
            }
            else
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void dgvTimkiem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                if (e.RowIndex < 0) return;
                var row = dgvTimkiem.Rows[e.RowIndex];

                if (rdoDocgia.Checked)
                {
                    // Chọn độc giả → điền vào txtMadocgia
                    txtMadocgia.Text = row.Cells["MaDocGia"].Value?.ToString();
                    rdoTaiLieu.Checked = true;
                    LoadDgvTimKiem();
                }
                else if (rdoTaiLieu.Checked)
                {
                    // Kiểm tra SoLuongConLai
                    int conLai = Convert.ToInt32(row.Cells["SoLuongConLai"].Value);
                    if (conLai <= 0)
                    {
                        MessageBox.Show("Tài liệu này hiện đã hết!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string maTL = row.Cells["MaTaiLieu"].Value?.ToString();
                    if (gioHang.Any(x => x.MaTaiLieu == maTL))
                    {
                        MessageBox.Show("Tài liệu này đã có trong danh sách mượn!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    gioHang.Add(new ChiTietPhieuMuonDTO
                    {
                        MaTaiLieu = maTL,
                        TenTaiLieu = row.Cells["TenTaiLieu"].Value?.ToString(),
                        LoaiTaiLieu = row.Cells["LoaiTaiLieu"].Value?.ToString(),
                        SoLuong = 1,
                        TinhTrang = "Mới",
                        SoLuongConLai = conLai
                    });
                    RefreshGioHang();
                }
                else if (rdoPhieuMuon.Checked)
                {
                    // Chọn phiếu mượn → hiển thị lên form
                    HienThiPhieuMuonLenForm(row);
                }
            }
        }
            private void HienThiPhieuMuonLenForm(DataGridViewRow row)
            {
            txtMaPM.Text = row.Cells["MaPhieuMuon"].Value?.ToString();
            txtMadocgia.Text = row.Cells["MaDocGia"].Value?.ToString();
            txtManhanvien.Text = row.Cells["MaNhanVien"].Value?.ToString();
            dtimeNgaymuon.Value = Convert.ToDateTime(row.Cells["NgayMuon"].Value);
            _hanTraHienTai = Convert.ToDateTime(row.Cells["HanTra"].Value);
            dtimeNgaytra.Value = _hanTraHienTai;
            _trangThaiHienTai = row.Cells["TrangThai"].Value?.ToString();
            cboTrangthai.SelectedItem = _trangThaiHienTai;

            // Load chi tiết phiếu vào giỏ hàng
            DataTable dtCT = bll.GetChiTietPhieuMuon(txtMaPM.Text);
            gioHang.Clear();
            if (dtCT != null)
            {
                foreach (DataRow r in dtCT.Rows)
                {
                    gioHang.Add(new ChiTietPhieuMuonDTO
                    {
                        MaTaiLieu = r["MaTaiLieu"].ToString(),
                        TenTaiLieu = r["TenTaiLieu"].ToString(),
                        LoaiTaiLieu = r["LoaiTaiLieu"].ToString(),
                        SoLuong = Convert.ToInt32(r["SoLuongMuon"]),
                        TinhTrang = r["TinhTrangTaiLieu"].ToString()
                    });
                }
            }
            RefreshGioHang();
            CapNhatTrangThaiNut();
            }

        private void dgvChitietmuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;
            if (dgvChitietmuon.Columns[e.ColumnIndex].Name != "colXoa") return;
            if (string.IsNullOrEmpty(txtMaPM.Text)) // chỉ xóa khi đang tạo mới
            {
                if (MessageBox.Show("Xóa tài liệu này khỏi danh sách?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    gioHang.RemoveAt(e.RowIndex);
                    RefreshGioHang();
                }
            }

        }
        private void LoadDgvTimKiem()
        {
            string tuKhoa = txtTimkiem.Text.Trim();
            DataTable dt = null;

            if (rdoDocgia.Checked)
                dt = bll.TimKiemDocGia(tuKhoa);
            else if (rdoTaiLieu.Checked)
                dt = bll.TimKiemTaiLieu(tuKhoa);
            else if (rdoPhieuMuon.Checked)
            {
                string ttFilter = (cboTrangthai?.SelectedItem?.ToString());
                dt = bll.TimKiemPhieuMuon(tuKhoa, ttFilter);
            }

            dgvTimkiem.DataSource = dt;
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            LoadDgvTimKiem();
        }

        private void rdoDocgia_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDocgia.Checked && !UserSession.IsNhanVienOrAdmin)
            {
                MessageBox.Show("Bạn không có quyền tìm kiếm độc giả khác!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rdoTaiLieu.Checked = true;
                return;
            }
            // Ẩn/hiện combobox lọc trạng thái chỉ khi tìm phiếu mượn
            if (cboTrangthai != null)
                cboTrangthai.Visible = rdoPhieuMuon.Checked;
            LoadDgvTimKiem();
        }

        private void rdoTaiLieu_CheckedChanged(object sender, EventArgs e)
        {
            if (cboTrangthai != null)
                cboTrangthai.Visible = rdoPhieuMuon.Checked;
            LoadDgvTimKiem();
        }

        private void rdoPhieuMuon_CheckedChanged(object sender, EventArgs e)
        {
            if (cboTrangthai != null)
                cboTrangthai.Visible = rdoPhieuMuon.Checked;
            LoadDgvTimKiem();
        }

        private void dtimeNgaymuon_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPM.Text)) // chỉ áp dụng khi tạo mới
                dtimeNgaytra.Value = dtimeNgaymuon.Value.AddDays(GetMaxDays());
        }
    }
}
