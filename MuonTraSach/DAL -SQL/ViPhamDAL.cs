using MuonTraSach.DAL___SQL;
using MuonTraSach.DTO_chứa_dữ_liệu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.DAL__SQL
{
    public class ViPhamDAL : DBConnection
    {
        public DataTable GetAllViPham()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblViPham", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable GetAllDocGia()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MaDocGia, HoTen, SoDienThoai FROM tblDocGia", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool Insert(ViPhamDTO vp)
        {
            string sql = "INSERT INTO tblViPham VALUES (@ma, @madg, @nd, @ht, @tg, @tt, @tien)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ma", vp.MaViPham);
            cmd.Parameters.AddWithValue("@madg", vp.MaDocGia);
            cmd.Parameters.AddWithValue("@nd", vp.NoiDungViPham);
            cmd.Parameters.AddWithValue("@ht", vp.HinhThucXuLy);
            cmd.Parameters.AddWithValue("@tg", vp.ThoiGianXuLy);
            cmd.Parameters.AddWithValue("@tt", vp.TrangThaiXuLy);
            cmd.Parameters.AddWithValue("@tien", vp.TienPhat);

            if (conn.State == ConnectionState.Closed) conn.Open();
            int res = cmd.ExecuteNonQuery();
            conn.Close();
            return res > 0;
        }

        public bool Update(ViPhamDTO vp)
        {
            string sql = "UPDATE tblViPham SET MaDocGia=@madg, NoiDungViPham=@nd, HinhThucXuLy=@ht, ThoiGianXuLy=@tg, TrangThaiXuLy=@tt, TienPhat=@tien WHERE MaViPham=@ma";
            SqlCommand cmd = new SqlCommand(sql, conn);
            // ... (Add parameters tương tự Insert)
            cmd.Parameters.AddWithValue("@ma", vp.MaViPham);
            cmd.Parameters.AddWithValue("@madg", vp.MaDocGia);
            cmd.Parameters.AddWithValue("@nd", vp.NoiDungViPham);
            cmd.Parameters.AddWithValue("@ht", vp.HinhThucXuLy);
            cmd.Parameters.AddWithValue("@tg", vp.ThoiGianXuLy);
            cmd.Parameters.AddWithValue("@tt", vp.TrangThaiXuLy);
            cmd.Parameters.AddWithValue("@tien", vp.TienPhat);

            if (conn.State == ConnectionState.Closed) conn.Open();
            int res = cmd.ExecuteNonQuery();
            conn.Close();
            return res > 0;
        }

        public bool Delete(string ma)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM tblViPham WHERE MaViPham=@ma", conn);
            cmd.Parameters.AddWithValue("@ma", ma);
            if (conn.State == ConnectionState.Closed) conn.Open();
            int res = cmd.ExecuteNonQuery();
            conn.Close();
            return res > 0;
        }

        public string GetLastID()
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 MaViPham FROM tblViPham ORDER BY MaViPham DESC", conn);
            if (conn.State == ConnectionState.Closed) conn.Open();
            object res = cmd.ExecuteScalar();
            conn.Close();
            return res?.ToString();
        }
        public DataTable SearchByDocGia(string key)
        {
            string sql = "SELECT MaDocGia, HoTen, SoDienThoai FROM tblDocGia WHERE MaDocGia LIKE @key OR HoTen LIKE @key";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@key", "%" + key + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable SearchByViPham(string key)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblViPham WHERE MaViPham LIKE '%" + key + "%'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
