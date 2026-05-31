using System.Data;
using Dashboard_Search.DAL;

namespace Dashboard_Search.BLL
{
    public class SearchBLL
    {
        private SearchDAL dal = new SearchDAL();

        public DataTable TimKiem(string tuKhoa) { return dal.TimKiemToanHeThong(tuKhoa); }
        public DataTable LayChiTiet(string phanLoai, string ma) { return dal.LayChiTiet(phanLoai, ma); }
    }
}