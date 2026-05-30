using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.DTO_chứa_dữ_liệu
{
    public class UserSession
    {
        /// <summary>MaDocGia hoặc MaNhanVien tùy LoaiTaiKhoan.</summary>
        public static string MaNguoiDung { get; set; }
        /// <summary>DocGia | NhanVien | Admin</summary>
        public static string LoaiTaiKhoan { get; set; }
        /// <summary>SinhVien | CBGV — chỉ có giá trị khi LoaiTaiKhoan == "DocGia"</summary>
        public static string LoaiDocGia { get; set; }

        public static bool IsNhanVienOrAdmin =>
            LoaiTaiKhoan == "NhanVien" || LoaiTaiKhoan == "Admin";

        public static void ClearSession()
        {
            MaNguoiDung = null;
            LoaiTaiKhoan = null;
            LoaiDocGia = null;
        }
    }

}
