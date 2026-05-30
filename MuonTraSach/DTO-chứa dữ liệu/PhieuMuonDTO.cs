using System;
using System.Collections.Generic;
using System.Globalization;

namespace MuonTraSach.DTO_chứa_dữ_liệu
{
    public class ChiTietPhieuMuonDTO
    {
        public string MaTaiLieu { get; set; }
        public string TenTaiLieu { get; set; }
        public string LoaiTaiLieu { get; set; }
        /// <summary>Tình trạng vật lý: Mới | Cũ | Hỏng...</summary>
        public string TinhTrang { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongConLai { get; set; }
    }

    public class PhieuMuonDTO
    {
        /// <summary>Dạng "PM0001". Để null/rỗng khi tạo mới — DAL tự sinh.</summary>
        public string MaPM { get; set; }
        public string MaDG { get; set; }
        /// <summary>Null khi độc giả tự tạo phiếu (chờ duyệt).</summary>
        public string MaNV { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
        /// <summary>Chuỗi khớp hằng số TrangThai.*</summary>
        public string TrangThai { get; set; }

        public List<ChiTietPhieuMuonDTO> DanhSachChiTiet { get; set; }
            = new List<ChiTietPhieuMuonDTO>();
    }

    public class DocGiaDTO
    {
        public string MaDocGia { get; set; }
        public string HoTen { get; set; }
        /// <summary>SinhVien | CBGV</summary>
        public string LoaiDocGia { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
    }
}