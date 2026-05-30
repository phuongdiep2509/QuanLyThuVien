using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MuonTraSach.DTO
{
    public class TheLoaiDTO
    {
        public string MaTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }
    }
}
