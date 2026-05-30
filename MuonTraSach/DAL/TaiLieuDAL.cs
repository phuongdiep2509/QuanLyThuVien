using MuonTraSach.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace MuonTraSach.DAL
{
    internal class TaiLieuDAL
    {
        public List<TaiLieuDTO> LayDanhSach(string tuKhoa, bool? trangThai, int page, int pageSize)
        {
            using (var conn = Databasehelper.GetConnection())
            {
                int offset = (page - 1) * pageSize;

                string sql = @"
                    SELECT 
                        tl.MaTaiLieu,
                        tl.TenTaiLieu,
                        tl.LoaiTaiLieu,
                        tl.MaTacGia,
                        tg.TenTacGia,
                        tl.MaTheLoai,
                        th.TenTheLoai,
                        tl.NamXuatBan,
                        tl.NhaXuatBan,
                        tl.SoLuongHienCo,
                        tl.SoLuongConLai,
                        tl.TinhTrang,
                        tl.TrangThai,
                        tl.AnhBia
                    FROM tblTaiLieu tl
                    LEFT JOIN tblTacGia tg ON tl.MaTacGia = tg.MaTacGia
                    LEFT JOIN tblTheLoai th ON tl.MaTheLoai = th.MaTheLoai
                    WHERE 
                        (@TuKhoa IS NULL OR @TuKhoa = '' 
                         OR tl.MaTaiLieu LIKE '%' + @TuKhoa + '%'
                         OR tl.TenTaiLieu LIKE '%' + @TuKhoa + '%'
                         OR tg.TenTacGia LIKE '%' + @TuKhoa + '%'
                         OR th.TenTheLoai LIKE '%' + @TuKhoa + '%')
                        AND (@TrangThai IS NULL OR tl.TrangThai = @TrangThai)
                    ORDER BY tl.MaTaiLieu
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                return conn.Query<TaiLieuDTO>(sql, new
                {
                    TuKhoa = tuKhoa,
                    TrangThai = trangThai,
                    Offset = offset,
                    PageSize = pageSize
                }).ToList();
            }
        }

        public int DemSoDong(string tuKhoa, bool? trangThai)
        {
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM tblTaiLieu tl
                    LEFT JOIN tblTacGia tg ON tl.MaTacGia = tg.MaTacGia
                    LEFT JOIN tblTheLoai th ON tl.MaTheLoai = th.MaTheLoai
                    WHERE 
                        (@TuKhoa IS NULL OR @TuKhoa = '' 
                         OR tl.MaTaiLieu LIKE '%' + @TuKhoa + '%'
                         OR tl.TenTaiLieu LIKE '%' + @TuKhoa + '%'
                         OR tg.TenTacGia LIKE '%' + @TuKhoa + '%'
                         OR th.TenTheLoai LIKE '%' + @TuKhoa + '%')
                        AND (@TrangThai IS NULL OR tl.TrangThai = @TrangThai)";

                return conn.ExecuteScalar<int>(sql, new
                {
                    TuKhoa = tuKhoa,
                    TrangThai = trangThai
                });
            }
        }

        public bool Them(TaiLieuDTO taiLieu)
        {
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO tblTaiLieu
                    (
                        MaTaiLieu,
                        TenTaiLieu,
                        LoaiTaiLieu,
                        MaTacGia,
                        MaTheLoai,
                        NamXuatBan,
                        NhaXuatBan,
                        SoLuongHienCo,
                        SoLuongConLai,
                        TinhTrang,
                        TrangThai,
                        AnhBia
                    )
                    VALUES
                    (
                        @MaTaiLieu,
                        @TenTaiLieu,
                        @LoaiTaiLieu,
                        @MaTacGia,
                        @MaTheLoai,
                        @NamXuatBan,
                        @NhaXuatBan,
                        @SoLuongHienCo,
                        @SoLuongConLai,
                        @TinhTrang,
                        @TrangThai,
                        @AnhBia
                    )";

                int result = conn.Execute(sql, taiLieu);
                return result > 0;
            }
        }

        public bool Sua(TaiLieuDTO taiLieu)
        {
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    UPDATE tblTaiLieu
                    SET
                        TenTaiLieu = @TenTaiLieu,
                        LoaiTaiLieu = @LoaiTaiLieu,
                        MaTacGia = @MaTacGia,
                        MaTheLoai = @MaTheLoai,
                        NamXuatBan = @NamXuatBan,
                        NhaXuatBan = @NhaXuatBan,
                        SoLuongHienCo = @SoLuongHienCo,
                        SoLuongConLai = @SoLuongConLai,
                        TinhTrang = @TinhTrang,
                        TrangThai = @TrangThai,
                        AnhBia = @AnhBia
                    WHERE MaTaiLieu = @MaTaiLieu";

                int result = conn.Execute(sql, taiLieu);
                return result > 0;
            }
        }

        public bool NgungHoatDong(string maTaiLieu)
        {
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    UPDATE tblTaiLieu
                    SET TrangThai = 0
                    WHERE MaTaiLieu = @MaTaiLieu";

                int result = conn.Execute(sql, new { MaTaiLieu = maTaiLieu });
                return result > 0;
            }
        }

        public bool KiemTraMaTonTai(string maTaiLieu)
        {
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM tblTaiLieu
                    WHERE MaTaiLieu = @MaTaiLieu";

                int count = conn.ExecuteScalar<int>(sql, new { MaTaiLieu = maTaiLieu });
                return count > 0;
            }
        }
    }
}
