using System;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MuonTraSach.BLL;
using MuonTraSach.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MuonTraSach.GUI
{
    public partial class frmDashboard : Form
    {
        private DashboardBLL bll = new DashboardBLL();

        public frmDashboard()
        {
            InitializeComponent();
            this.Load += (s, e) => {
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;
                LoadDuLieu();
            };
            btnLoc.Click += (s, e) => LoadDuLieu();
            btnInBaoCao.Click += BtnInBaoCao_Click;
        }

        private void LoadDuLieu()
        {
            // Load 4 thẻ màu
            DashboardDTO tk = bll.LaySoLieuThongKe(dtpTuNgay.Value, dtpDenNgay.Value);
            lblTongSach.Text = tk.TongSach.ToString();
            lblTongDocGia.Text = tk.TongDocGia.ToString();
            lblDangMuon.Text = tk.SachDangMuon.ToString();
            lblQuaHan.Text = tk.SachQuaHan.ToString();

            // Load Bảng
            dgvPhieuMuonMoi.DataSource = bll.LayDanhSachMoi(dtpTuNgay.Value, dtpDenNgay.Value);
            dgvQuaHan.DataSource = bll.LayDanhSachQuaHan(dtpTuNgay.Value, dtpDenNgay.Value);

            // ... (Phần code load 4 thẻ màu và bảng giữ nguyên) ...

            // ==========================================
            // ĐOẠN MỚI: XỬ LÝ VẼ BIỂU ĐỒ CHUẨN 7 NGÀY
            // ==========================================

            // 1. Tính toán ra đúng 7 ngày lùi lại từ ngày được chọn ở ô Đến Ngày
            DateTime ngayKetThuc = dtpDenNgay.Value.Date;
            DateTime ngayBatDau = ngayKetThuc.AddDays(-6);

            // Lấy dữ liệu từ BLL trong đúng 7 ngày đó
            DataTable dtChart = bll.LayDuLieuBieuDo(ngayBatDau, ngayKetThuc);
            chartThongKe.Series["SoLuongMuon"].Points.Clear();

            // 2. Ép biểu đồ luôn hiện rõ từng vạch ngày, không bị gộp chữ
            chartThongKe.ChartAreas[0].AxisX.Interval = 1;

            // 3. Vòng lặp vẽ ĐỦ 7 CỘT (Nếu ngày nào không có trong SQL thì gán bằng 0)
            for (int i = 0; i <= 6; i++)
            {
                DateTime ngayDangXet = ngayBatDau.AddDays(i);
                string ngayChu = ngayDangXet.ToString("dd/MM");
                int soLuong = 0; // Mặc định ban đầu = 0

                // Dò xem ngày đang xét có nằm trong kết quả SQL trả về không
                foreach (DataRow row in dtChart.Rows)
                {
                    if (Convert.ToDateTime(row["Ngay"]).Date == ngayDangXet)
                    {
                        soLuong = Convert.ToInt32(row["SoLuong"]);
                        break;
                    }
                }

                // Vẽ cột lên biểu đồ
                chartThongKe.Series["SoLuongMuon"].Points.AddXY(ngayChu, soLuong);
            }
        }

        private void BtnInBaoCao_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Documents (*.pdf)|*.pdf";
            sfd.FileName = "BaoCaoThongKe_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Chụp ảnh form hiện tại
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(this.Width, this.Height);
                    this.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));

                    using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            bmp.Save(ms, ImageFormat.Png);
                            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(ms.GetBuffer());
                            pdfImage.ScaleToFit(pdfDoc.PageSize.Width - 20f, pdfDoc.PageSize.Height - 20f);
                            pdfImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                            pdfDoc.Add(pdfImage);
                        }
                        pdfDoc.Close();
                    }
                    MessageBox.Show("Xuất báo cáo PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
