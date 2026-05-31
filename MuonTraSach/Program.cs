using MuonTraSach.GUI___Giao_diện;
using MuonTraSach.DTO_chứa_dữ_liệu;
using MuonTraSach.DTO;
using MuonTraSach.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuonTraSach
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // === TEST NHANH: Gán phiên đăng nhập giả để chạy thử form ===
            // Xóa đoạn này khi tích hợp với form đăng nhập thật
            /*UserSession.MaNguoiDung = "NV001";
            UserSession.LoaiTaiKhoan = "NhanVien"; // Thử với "DocGia", "Admin"
            UserSession.LoaiDocGia = null;        // Đặt "SinhVien" hoặc "CBGV" nếu LoaiTaiKhoan = "DocGia"
            // ============================================================*/
            Application.Run(new FrmQLTacGia());
        }
    }
}
