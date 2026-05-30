using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.DTO_chứa_dữ_liệu
{
    public class ViPhamDTO
    {
        public string MaViPham { get; set; }
        public string MaDocGia { get; set; }
        public string NoiDungViPham { get; set; }
        public string HinhThucXuLy { get; set; }
        public DateTime ThoiGianXuLy { get; set; }
        public string TrangThaiXuLy { get; set; }
        public decimal TienPhat { get; set; }
    }
}
