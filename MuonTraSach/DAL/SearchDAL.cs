using System;
using System.Data;
using System.Data.SqlClient;

namespace Dashboard_Search.DAL
{
    public class SearchDAL
    {
        public DataTable TimKiemToanHeThong(string tuKhoa)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT N'Tài liệu' AS PhanLoai, MaTaiLieu AS MaDoiTuong, TenTaiLieu AS ThongTin 
                FROM tblTaiLieu WHERE TenTaiLieu LIKE @TuKhoa OR MaTaiLieu LIKE @TuKhoa
                UNION ALL
                SELECT N'Độc giả' AS PhanLoai, MaDocGia AS MaDoiTuong, HoTen AS ThongTin 
                FROM tblDocGia WHERE HoTen LIKE @TuKhoa OR MaDocGia LIKE @TuKhoa OR SoDienThoai LIKE @TuKhoa
                UNION ALL
                SELECT N'Tác giả' AS PhanLoai, MaTacGia AS MaDoiTuong, TenTacGia AS ThongTin 
                FROM tblTacGia WHERE TenTacGia LIKE @TuKhoa OR MaTacGia LIKE @TuKhoa
                UNION ALL
                SELECT N'Thể loại' AS PhanLoai, MaTheLoai AS MaDoiTuong, TenTheLoai AS ThongTin 
                FROM tblTheLoai WHERE TenTheLoai LIKE @TuKhoa OR MaTheLoai LIKE @TuKhoa
                UNION ALL
                SELECT N'Phiếu mượn' AS PhanLoai, MaPhieuMuon AS MaDoiTuong, N'Mã độc giả: ' + MaDocGia AS ThongTin 
                FROM tblPhieuMuon WHERE MaPhieuMuon LIKE @TuKhoa OR MaDocGia LIKE @TuKhoa";

            using (SqlConnection conn = new SqlConnection(DBConnection.strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable LayChiTiet(string phanLoai, string maDoiTuong)
        {
            DataTable dt = new DataTable();
            string query = "";
            if (phanLoai == "Tài liệu") query = "SELECT * FROM tblTaiLieu WHERE MaTaiLieu = @Ma";
            else if (phanLoai == "Độc giả") query = "SELECT * FROM tblDocGia WHERE MaDocGia = @Ma";
            else if (phanLoai == "Tác giả") query = "SELECT * FROM tblTacGia WHERE MaTacGia = @Ma";
            else if (phanLoai == "Thể loại") query = "SELECT * FROM tblTheLoai WHERE MaTheLoai = @Ma";
            else if (phanLoai == "Phiếu mượn") query = "SELECT * FROM tblPhieuMuon WHERE MaPhieuMuon = @Ma";

            using (SqlConnection conn = new SqlConnection(DBConnection.strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@Ma", maDoiTuong);
                da.Fill(dt);
            }
            return dt;
        }
    }
}