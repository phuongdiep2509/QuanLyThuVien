using MuonTraSach.DAL__SQL;
using MuonTraSach.DTO_chứa_dữ_liệu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.BLL_check_thông_tin
{
    
        public class ViPhamBLL
        {
        ViPhamDAL dal = new ViPhamDAL();

        public DataTable LayDanhSach() => dal.GetAllViPham();

        public string SinhMaTuDong()
        {
            string lastID = dal.GetLastID();
            if (string.IsNullOrEmpty(lastID)) return "VP001";
            // Cắt chuỗi "VP001" lấy "001", chuyển sang số, cộng 1, rồi định dạng lại
            int num = int.Parse(lastID.Substring(2)) + 1;
            return "VP" + num.ToString("D3"); // Kết quả: VP002, VP003...
        }

        public bool XuLyLuu(ViPhamDTO vp, bool laThemMoi)
        {
            if (laThemMoi) return dal.Insert(vp);
            return dal.Update(vp);
        }

        public bool XuLyXoa(string ma) => dal.Delete(ma);
        public DataTable TimTheoDocGia(string key)
        {
            return dal.SearchByDocGia(key);
        }
        public DataTable TimTheoViPham(string key) => dal.SearchByViPham(key);
        public DataTable LayTatCaDocGia() => dal.GetAllDocGia();
    }
    
}
