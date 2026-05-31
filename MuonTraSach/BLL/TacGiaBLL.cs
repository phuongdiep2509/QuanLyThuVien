using MuonTraSach.DAL;
using MuonTraSach.DTO;
using System.Collections.Generic;

namespace MuonTraSach.BLL
{
    public class TacGiaBLL
    {
        private readonly TacGiaDAL tacGiaDAL = new TacGiaDAL();

        public List<TacGiaDTO> LayTatCa()
        {
            return tacGiaDAL.LayTatCa();
        }

        public List<TacGiaDTO> LayDangHoatDong()
        {
            return tacGiaDAL.LayDangHoatDong();
        }

        public List<TacGiaDTO> LayDanhSach(string tuKhoa, string chuCai, int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            return tacGiaDAL.LayDanhSach(tuKhoa, chuCai, page, pageSize);
        }

        public int DemSoDong(string tuKhoa, string chuCai)
        {
            return tacGiaDAL.DemSoDong(tuKhoa, chuCai);
        }

        public bool Them(TacGiaDTO tacGia, out string message)
        {
            message = "";

            if (tacGia == null)
            {
                message = "Dữ liệu tác giả không hợp lệ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(tacGia.MaTacGia))
            {
                message = "Mã tác giả không được bỏ trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(tacGia.TenTacGia))
            {
                message = "Tên tác giả không được bỏ trống.";
                return false;
            }

            if (tacGiaDAL.KiemTraMaTonTai(tacGia.MaTacGia))
            {
                message = "Mã tác giả đã tồn tại.";
                return false;
            }

            bool result = tacGiaDAL.Them(tacGia);

            if (result)
            {
                message = "Thêm tác giả thành công.";
                return true;
            }

            message = "Thêm tác giả thất bại.";
            return false;
        }

        public bool Sua(TacGiaDTO tacGia, out string message)
        {
            message = "";

            if (tacGia == null)
            {
                message = "Dữ liệu tác giả không hợp lệ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(tacGia.MaTacGia))
            {
                message = "Vui lòng chọn tác giả cần sửa.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(tacGia.TenTacGia))
            {
                message = "Tên tác giả không được bỏ trống.";
                return false;
            }

            bool result = tacGiaDAL.Sua(tacGia);

            if (result)
            {
                message = "Cập nhật tác giả thành công.";
                return true;
            }

            message = "Cập nhật tác giả thất bại.";
            return false;
        }

        public bool NgungHoatDong(string maTacGia, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTacGia))
            {
                message = "Vui lòng chọn tác giả cần ngừng hoạt động.";
                return false;
            }

            bool result = tacGiaDAL.NgungHoatDong(maTacGia);

            if (result)
            {
                message = "Ngừng hoạt động tác giả thành công.";
                return true;
            }

            message = "Ngừng hoạt động tác giả thất bại.";
            return false;
        }

        public bool HoatDong(string maTacGia, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTacGia))
            {
                message = "Vui lòng chọn tác giả cần kích hoạt lại.";
                return false;
            }

            bool result = tacGiaDAL.HoatDong(maTacGia);

            if (result)
            {
                message = "Kích hoạt lại tác giả thành công.";
                return true;
            }

            message = "Kích hoạt lại tác giả thất bại.";
            return false;
        }
        public bool Xoa(string maTacGia, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTacGia))
            {
                message = "Vui lòng chọn tác giả cần xóa.";
                return false;
            }

            if (tacGiaDAL.KiemTraTacGiaDangDuocDung(maTacGia))
            {
                message = "Không thể xóa tác giả này vì đang có tài liệu thuộc tác giả. Vui lòng dùng chức năng Ngừng hoạt động.";
                return false;
            }

            bool result = tacGiaDAL.Xoa(maTacGia);

            if (result)
            {
                message = "Xóa tác giả thành công.";
                return true;
            }

            message = "Xóa tác giả thất bại.";
            return false;
        }
    }
}