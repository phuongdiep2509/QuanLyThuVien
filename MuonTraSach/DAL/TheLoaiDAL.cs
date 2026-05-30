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
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTheLoai, TenTheLoai, MoTa, TrangThai
                    FROM tblTheLoai
                    ORDER BY TenTheLoai";

                return conn.Query<TheLoaiDTO>(sql).ToList();
            }
        }

        public List<TheLoaiDTO> LayDangHoatDong()
        {
            using (var conn = Databasehelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTheLoai, TenTheLoai, MoTa, TrangThai
                    FROM tblTheLoai
                    WHERE TrangThai = 1
                    ORDER BY TenTheLoai";

                return conn.Query<TheLoaiDTO>(sql).ToList();
            }
        }
    }
}
