using Dapper;
using MuonTraSach.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MuonTraSach.DAL
{
    public class TheLoaiDAL
    {
        public List<TheLoaiDTO> LayTatCa()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTheLoai, TenTheLoai, MoTa, TrangThai
                    FROM tblTheLoai
                    ORDER BY MaTheLoai";

                return conn.Query<TheLoaiDTO>(sql).ToList();
            }
        }

        public List<TheLoaiDTO> LayDangHoatDong()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTheLoai, TenTheLoai, MoTa, TrangThai
                    FROM tblTheLoai
                    WHERE TrangThai = 1
                    ORDER BY TenTheLoai";

                return conn.Query<TheLoaiDTO>(sql).ToList();
            }
        }

        public List<TheLoaiDTO> LayDanhSach(string tuKhoa, int page, int pageSize)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                int offset = (page - 1) * pageSize;

                string sql = @"
                    SELECT MaTheLoai, TenTheLoai, MoTa, TrangThai
                    FROM tblTheLoai
                    WHERE
                        @TuKhoa IS NULL OR @TuKhoa = ''
                        OR MaTheLoai LIKE '%' + @TuKhoa + '%'
                        OR TenTheLoai LIKE '%' + @TuKhoa + '%'
                        OR MoTa LIKE '%' + @TuKhoa + '%'
                    ORDER BY MaTheLoai
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                return conn.Query<TheLoaiDTO>(sql, new
                {
                    TuKhoa = tuKhoa,
                    Offset = offset,
                    PageSize = pageSize
                }).ToList();
            }
        }

        public int DemSoDong(string tuKhoa)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM tblTheLoai
                    WHERE
                        @TuKhoa IS NULL OR @TuKhoa = ''
                        OR MaTheLoai LIKE '%' + @TuKhoa + '%'
                        OR TenTheLoai LIKE '%' + @TuKhoa + '%'
                        OR MoTa LIKE '%' + @TuKhoa + '%'";

                return conn.ExecuteScalar<int>(sql, new { TuKhoa = tuKhoa });
            }
        }

        public bool Them(TheLoaiDTO theLoai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO tblTheLoai
                    (
                        MaTheLoai,
                        TenTheLoai,
                        MoTa,
                        TrangThai
                    )
                    VALUES
                    (
                        @MaTheLoai,
                        @TenTheLoai,
                        @MoTa,
                        @TrangThai
                    )";

                int result = conn.Execute(sql, theLoai);
                return result > 0;
            }
        }

        public bool Sua(TheLoaiDTO theLoai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    UPDATE tblTheLoai
                    SET
                        TenTheLoai = @TenTheLoai,
                        MoTa = @MoTa,
                        TrangThai = @TrangThai
                    WHERE MaTheLoai = @MaTheLoai";

                int result = conn.Execute(sql, theLoai);
                return result > 0;
            }
        }

        public bool NgungHoatDong(string maTheLoai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    UPDATE tblTheLoai
                    SET TrangThai = 0
                    WHERE MaTheLoai = @MaTheLoai";

                int result = conn.Execute(sql, new { MaTheLoai = maTheLoai });
                return result > 0;
            }
        }

        public bool HoatDong(string maTheLoai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    UPDATE tblTheLoai
                    SET TrangThai = 1
                    WHERE MaTheLoai = @MaTheLoai";

                int result = conn.Execute(sql, new { MaTheLoai = maTheLoai });
                return result > 0;
            }
        }

        public bool KiemTraMaTonTai(string maTheLoai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM tblTheLoai
                    WHERE MaTheLoai = @MaTheLoai";

                int count = conn.ExecuteScalar<int>(sql, new { MaTheLoai = maTheLoai });
                return count > 0;
            }
        }

        public bool KiemTraTheLoaiDangDuocDung(string maTheLoai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM tblTaiLieu
                    WHERE MaTheLoai = @MaTheLoai";

                int count = conn.ExecuteScalar<int>(sql, new { MaTheLoai = maTheLoai });
                return count > 0;
            }
        }

        public bool Xoa(string maTheLoai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    DELETE FROM tblTheLoai
                    WHERE MaTheLoai = @MaTheLoai";

                int result = conn.Execute(sql, new { MaTheLoai = maTheLoai });
                return result > 0;
            }
        }
    }
}