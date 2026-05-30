using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.DTO_chứa_dữ_liệu
{
    public static class TrangThai
    {
        public const string ChoDuyet = "Chờ duyệt";
        public const string DaDuyet = "Đã duyệt";
        public const string DangMuon = "Đang mượn";
        public const string DaGiaHan = "Đã gia hạn";
        public const string QuaHan = "Quá hạn";
        public const string DaTra = "Đã trả";
        public const string TuChoi = "Từ chối";

        public static readonly string[] TatCa =
        {
            ChoDuyet, DaDuyet, DangMuon, DaGiaHan, DaTra, TuChoi
        };
    }
}
