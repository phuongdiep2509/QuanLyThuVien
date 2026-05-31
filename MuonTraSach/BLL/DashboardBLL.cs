using System;
using System.Data;
using MuonTraSach.DAL;
using MuonTraSach.DTO;

namespace MuonTraSach.BLL
{
    public class DashboardBLL
    {
        private DashboardDAL dal = new DashboardDAL();

        public DashboardDTO LaySoLieuThongKe(DateTime tuNgay, DateTime denNgay) { return dal.LaySoLieuThongKe(tuNgay, denNgay); }
        public DataTable LayDanhSachMoi(DateTime tuNgay, DateTime denNgay) { return dal.LayDanhSach("Mới", tuNgay, denNgay); }
        public DataTable LayDanhSachQuaHan(DateTime tuNgay, DateTime denNgay) { return dal.LayDanhSach("Quá hạn", tuNgay, denNgay); }
        public DataTable LayDuLieuBieuDo(DateTime tuNgay, DateTime denNgay) { return dal.LayDuLieuBieuDo(tuNgay, denNgay); }
    }
}
