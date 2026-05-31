using Dapper;
using MuonTraSach.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MuonTraSach.DAL
{
    public class TacGiaDAL
    {
        public List<TacGiaDTO> LayTatCa()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTacGia, TenTacGia, GhiChu, TrangThai
                    FROM tblTacGia
                    ORDER BY MaTacGia";

                return conn.Query<TacGiaDTO>(sql).ToList();
            }
        }

        public List<TacGiaDTO> LayDangHoatDong()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTacGia, TenTacGia, GhiChu, TrangThai
                    FROM tblTacGia
                    WHERE TrangThai = 1
                    ORDER BY TenTacGia";

                return conn.Query<TacGiaDTO>(sql).ToList();
            }
        }

        public List<TacGiaDTO> LayDanhSach(string tuKhoa, string chuCai, int page, int pageSize)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                int offset = (page - 1) * pageSize;

                string sql = @"
            SELECT MaTacGia, TenTacGia, GhiChu, TrangThai
            FROM tblTacGia
            WHERE
                (@TuKhoa IS NULL OR @TuKhoa = ''
                 OR MaTacGia LIKE '%' + @TuKhoa + '%'
                 OR TenTacGia LIKE '%' + @TuKhoa + '%'
                 OR GhiChu LIKE '%' + @TuKhoa + '%')
                AND
                (@ChuCai IS NULL OR @ChuCai = ''
                 OR LTRIM(TenTacGia) COLLATE Vietnamese_CI_AI LIKE @ChuCai + '%')
            ORDER BY TenTacGia
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                return conn.Query<TacGiaDTO>(sql, new
                {
                    TuKhoa = tuKhoa,
                    ChuCai = chuCai,
                    Offset = offset,
                    PageSize = pageSize
                }).ToList();
            }
        }

        public int DemSoDong(string tuKhoa, string chuCai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
            SELECT COUNT(*)
            FROM tblTacGia
            WHERE
                (@TuKhoa IS NULL OR @TuKhoa = ''
                 OR MaTacGia LIKE '%' + @TuKhoa + '%'
                 OR TenTacGia LIKE '%' + @TuKhoa + '%'
                 OR GhiChu LIKE '%' + @TuKhoa + '%')
                AND
                (@ChuCai IS NULL OR @ChuCai = ''
                 OR LTRIM(TenTacGia) COLLATE Vietnamese_CI_AI LIKE @ChuCai + '%')";

                return conn.ExecuteScalar<int>(sql, new
                {
                    TuKhoa = tuKhoa,
                    ChuCai = chuCai
                });
            }
        }

        public bool Them(TacGiaDTO tacGia)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO tblTacGia
                    (
                        MaTacGia,
                        TenTacGia,
                        GhiChu,
                        TrangThai
                    )
                    VALUES
                    (
                        @MaTacGia,
                        @TenTacGia,
                        @GhiChu,
                        @TrangThai
                    )";

                int result = conn.Execute(sql, tacGia);
                return result > 0;
            }
        }

        public bool Sua(TacGiaDTO tacGia)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    UPDATE tblTacGia
                    SET
                        TenTacGia = @TenTacGia,
                        GhiChu = @GhiChu,
                        TrangThai = @TrangThai
                    WHERE MaTacGia = @MaTacGia";

                int result = conn.Execute(sql, tacGia);
                return result > 0;
            }
        }

        public bool NgungHoatDong(string maTacGia)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    UPDATE tblTacGia
                    SET TrangThai = 0
                    WHERE MaTacGia = @MaTacGia";

                int result = conn.Execute(sql, new { MaTacGia = maTacGia });
                return result > 0;
            }
        }

        public bool HoatDong(string maTacGia)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    UPDATE tblTacGia
                    SET TrangThai = 1
                    WHERE MaTacGia = @MaTacGia";

                int result = conn.Execute(sql, new { MaTacGia = maTacGia });
                return result > 0;
            }
        }

        public bool KiemTraMaTonTai(string maTacGia)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM tblTacGia
                    WHERE MaTacGia = @MaTacGia";

                int count = conn.ExecuteScalar<int>(sql, new { MaTacGia = maTacGia });
                return count > 0;
            }
        }
        public bool KiemTraTacGiaDangDuocDung(string maTacGia)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
            SELECT COUNT(*)
            FROM tblTaiLieu
            WHERE MaTacGia = @MaTacGia";

                int count = conn.ExecuteScalar<int>(sql, new { MaTacGia = maTacGia });
                return count > 0;
            }
        }

        public bool Xoa(string maTacGia)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
            DELETE FROM tblTacGia
            WHERE MaTacGia = @MaTacGia";

                int result = conn.Execute(sql, new { MaTacGia = maTacGia });
                return result > 0;
            }
        }
    }
}