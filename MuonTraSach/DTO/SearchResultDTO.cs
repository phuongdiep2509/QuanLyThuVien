using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.DTO
{
    public class SearchResultDTO
    {
        public string PhanLoai { get; set; } // "Tài liệu", "Tác giả", "Phiếu mượn"
        public string MaDoiTuong { get; set; } // MaTaiLieu, MaTacGia, MaPhieuMuon
        public string ThongTinChiTiet { get; set; } // Tên sách, Tên tác giả, v.v.
    }
}
