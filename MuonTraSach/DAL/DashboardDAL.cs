using System;
using System.Data;
using System.Data.SqlClient;
using Dashboard_Search.DTO;

namespace Dashboard_Search.DAL
{
    public class DashboardDAL
    {
        public DashboardDTO LaySoLieuThongKe(DateTime tuNgay, DateTime denNgay)
        {
            DashboardDTO tk = new DashboardDTO();
            using (SqlConnection conn = new SqlConnection(DBConnection.strConn))
            {
                conn.Open();
                // Tổng sách
                tk.TongSach = (int)new SqlCommand("SELECT COUNT(*) FROM tblTaiLieu", conn).ExecuteScalar();
                // Tổng độc giả
                tk.TongDocGia = (int)new SqlCommand("SELECT COUNT(*) FROM tblDocGia", conn).ExecuteScalar();

                // Sách đang mượn
                SqlCommand cmdDangMuon = new SqlCommand("SELECT COUNT(*) FROM tblPhieuMuon WHERE TrangThai = N'Đang mượn' AND NgayMuon >= @TuNgay AND NgayMuon <= @DenNgay", conn);
                cmdDangMuon.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                cmdDangMuon.Parameters.AddWithValue("@DenNgay", denNgay.Date.AddDays(1).AddTicks(-1));
                tk.SachDangMuon = (int)cmdDangMuon.ExecuteScalar();

                // Sách quá hạn
                SqlCommand cmdQuaHan = new SqlCommand("SELECT COUNT(*) FROM tblPhieuMuon WHERE TrangThai = N'Quá hạn' AND NgayMuon >= @TuNgay AND NgayMuon <= @DenNgay", conn);
                cmdQuaHan.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                cmdQuaHan.Parameters.AddWithValue("@DenNgay", denNgay.Date.AddDays(1).AddTicks(-1));
                tk.SachQuaHan = (int)cmdQuaHan.ExecuteScalar();
            }
            return tk;
        }

        public DataTable LayDanhSach(string loai, DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            string query = loai == "Mới"
                ? "SELECT MaPhieuMuon, MaDocGia, NgayMuon FROM tblPhieuMuon WHERE NgayMuon >= @TuNgay AND NgayMuon <= @DenNgay ORDER BY NgayMuon DESC"
                : "SELECT MaPhieuMuon, MaDocGia, HanTra FROM tblPhieuMuon WHERE TrangThai = N'Quá hạn' AND NgayMuon >= @TuNgay AND NgayMuon <= @DenNgay";

            using (SqlConnection conn = new SqlConnection(DBConnection.strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                da.SelectCommand.Parameters.AddWithValue("@DenNgay", denNgay.Date.AddDays(1).AddTicks(-1));
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable LayDuLieuBieuDo(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            // Bỏ TOP 7 đi, lấy bình thường
            string query = @"SELECT CONVERT(date, NgayMuon) AS Ngay, COUNT(*) AS SoLuong 
                     FROM tblPhieuMuon 
                     WHERE NgayMuon >= @TuNgay AND NgayMuon <= @DenNgay
                     GROUP BY CONVERT(date, NgayMuon)";

            using (SqlConnection conn = new SqlConnection(DBConnection.strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                da.SelectCommand.Parameters.AddWithValue("@DenNgay", denNgay.Date.AddDays(1).AddTicks(-1));
                da.Fill(dt);
            }
            return dt;
        }
    }
}