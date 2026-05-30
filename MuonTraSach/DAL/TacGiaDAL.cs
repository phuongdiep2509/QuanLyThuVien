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
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTacGia, TenTacGia, GhiChu, TrangThai
                    FROM tblTacGia
                    ORDER BY TenTacGia";

                return conn.Query<TacGiaDTO>(sql).ToList();
            }
        }

        public List<TacGiaDTO> LayDangHoatDong()
        {
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTacGia, TenTacGia, GhiChu, TrangThai
                    FROM tblTacGia
                    WHERE TrangThai = 1
                    ORDER BY TenTacGia";

                return conn.Query<TacGiaDTO>(sql).ToList();
            }
        }
    }
}
