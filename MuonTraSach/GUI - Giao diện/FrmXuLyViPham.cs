
using MuonTraSach.BLL_check_thông_tin;
using MuonTraSach.DTO_chứa_dữ_liệu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuonTraSach.GUI___Giao_diện
{
    public partial class FrmXuLyViPham : Form
    {
        ViPhamBLL bll = new ViPhamBLL();
        bool dangThemMoi = false; // Biến để biết đang Thêm hay Sửa
        public FrmXuLyViPham()
        {
            InitializeComponent();
        }

        private void FrmXuLyViPham_Load(object sender, EventArgs e)
        {
            LoadData();
            SetButtonsState(true);
            cboHinhthucxuly.Items.Add("Phục vụ cộng đồng");
            cboHinhthucxuly.Items.Add("Cảnh cáo");
            cboHinhthucxuly.Items.Add("Khóa tài khoản");
            cboHinhthucxuly.Items.Add("Bồi thường");

            cboTrangthaixuly.Items.Add("Chưa xử lý");
            cboTrangthaixuly.Items.Add("Đang xử lý");
            cboTrangthaixuly.Items.Add("Đã xử lý");
        }
        private void SetButtonsState(bool isViewing)
        {
            btnThem.Enabled = isViewing;
            btnSua.Enabled = isViewing;
            btnXoa.Enabled = isViewing;
            btnDong.Enabled = isViewing;
            btnLuu.Enabled = !isViewing;
            btnBoqua.Enabled = !isViewing;

            // Các ô nhập liệu
            txtMadocgia.Enabled = !isViewing;
            txtNoidungvipham.Enabled = !isViewing;
            cboHinhthucxuly.Enabled = !isViewing;
            dtmThoigianxuly.Enabled = !isViewing;
            cboTrangthaixuly.Enabled = !isViewing;
            txtTienphat.Enabled = !isViewing;

            // LUÔN CHO PHÉP GRID HOẠT ĐỘNG (Hoặc chỉ khóa khi cần thiết)
            dgvTimkiem.Enabled = true;

            // Mã vi phạm luôn khóa
            btnMavipham.Enabled = false;
        }

        private void LoadData()
        {
            dgvTimkiem.DataSource = bll.LayDanhSach();
        }
        private void XoaTrangCaco()
        {
            txtMadocgia.Clear();
            txtNoidungvipham.Clear();
            txtTienphat.Text = "0";
            cboHinhthucxuly.SelectedIndex = -1;
            cboTrangthaixuly.SelectedIndex = -1;
            dtmThoigianxuly.Value = DateTime.Now;
        }

        private void dgvTimkiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvTimkiem.Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow r = dgvTimkiem.Rows[e.RowIndex];

            // TRƯỜNG HỢP 1: Đang bấm "Thêm" và tìm Độc giả để phạt
            if (dangThemMoi && rdoDocgia.Checked && btnLuu.Enabled)
            {
                string maDG = r.Cells["MaDocGia"].Value?.ToString();
                string tenDG = r.Cells["HoTen"].Value?.ToString();

                DialogResult dr = MessageBox.Show($"Chọn độc giả: {tenDG}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    txtMadocgia.Text = maDG;
                    txtNoidungvipham.Focus();
                }
            }
            // TRƯỜNG HỢP 2: Đang ở chế độ xem, click để hiện thông tin lên Form
            else if (btnThem.Enabled)
            {
                btnMavipham.Text = r.Cells["MaViPham"].Value?.ToString();
                txtMadocgia.Text = r.Cells["MaDocGia"].Value?.ToString();
                txtNoidungvipham.Text = r.Cells["NoiDungViPham"].Value?.ToString();
                cboHinhthucxuly.Text = r.Cells["HinhThucXuLy"].Value?.ToString();
                txtTienphat.Text = r.Cells["TienPhat"].Value?.ToString();

                if (r.Cells["ThoiGianXuLy"].Value != DBNull.Value)
                    dtmThoigianxuly.Value = Convert.ToDateTime(r.Cells["ThoiGianXuLy"].Value);

                cboTrangthaixuly.Text = r.Cells["TrangThaiXuLy"].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dangThemMoi = true;
            SetButtonsState(false);
            XoaTrangCaco();

            // SINH MÃ TỰ ĐỘNG NGAY KHI BẤM THÊM
            btnMavipham.Text = bll.SinhMaTuDong();

            rdoDocgia.Checked = true; // Ưu tiên tìm độc giả để chọn
            txtTimkiem.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMadocgia.Text))
            {
                MessageBox.Show("Vui lòng chọn Mã độc giả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                ViPhamDTO vp = new ViPhamDTO
                {
                    MaViPham = btnMavipham.Text,
                    MaDocGia = txtMadocgia.Text,
                    NoiDungViPham = txtNoidungvipham.Text,
                    HinhThucXuLy = cboHinhthucxuly.Text,
                    ThoiGianXuLy = dtmThoigianxuly.Value,
                    TrangThaiXuLy = cboTrangthaixuly.Text,
                    TienPhat = decimal.Parse(string.IsNullOrEmpty(txtTienphat.Text) ? "0" : txtTienphat.Text)
                };

                if (bll.XuLyLuu(vp, dangThemMoi))
                {
                    MessageBox.Show("Bạn có muốn lưu vi phạm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    LoadData();
                    SetButtonsState(true);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(btnMavipham.Text))
            {
                MessageBox.Show("Chọn bản ghi cần sửa!");
                return;
            }
            dangThemMoi = false;
            SetButtonsState(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(btnMavipham.Text)) return;

            if (MessageBox.Show("Xác nhận xóa?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bll.XuLyXoa(btnMavipham.Text))
                {
                    LoadData();
                    SetButtonsState(true);
                }
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimkiem.Text.Trim();

            // KIỂM TRA ĐANG CHỌN TÌM THEO ĐỘC GIẢ
            if (rdoDocgia.Checked)
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    // Nếu để trống ô tìm kiếm mà bấm nút -> Hiện TẤT CẢ độc giả
                    dgvTimkiem.DataSource = bll.LayTatCaDocGia();
                    MessageBox.Show("Đang hiển thị danh sách tất cả độc giả.", "Thông báo");
                }
                else
                {
                    // Nếu có từ khóa -> Lọc độc giả theo từ khóa
                    dgvTimkiem.DataSource = bll.TimTheoDocGia(keyword);
                }
            }
            // KIỂM TRA ĐANG CHỌN TÌM THEO VI PHẠM
            else
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    // Nếu để trống ô tìm kiếm mà bấm nút -> Hiện TẤT CẢ vi phạm
                    LoadData();
                    MessageBox.Show("Đang hiển thị danh sách tất cả vi phạm.", "Thông báo");
                }
                else
                {
                    // Nếu có từ khóa -> Lọc vi phạm theo từ khóa
                    dgvTimkiem.DataSource = bll.TimTheoViPham(keyword);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn đóng chức năng này?", "Xác nhận",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn bỏ qua không?", "Xác nhận",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SetButtonsState(true);
                LoadData();
            }
            
        }

        private void txtTienphat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Chỉ cho nhập số
            }
        }

        private void rdoDocgia_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDocgia.Checked)
            {
                // Khi chọn tìm theo độc giả, hiện ngay toàn bộ độc giả
                dgvTimkiem.DataSource = bll.LayTatCaDocGia();
            }
            else
            {
                // Khi chọn lại vi phạm, hiện lại toàn bộ vi phạm
                LoadData();
            }
        }
    }
}
