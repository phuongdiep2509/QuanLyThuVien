using MuonTraSach.DAL;
using MuonTraSach.DTO;
using System.Collections.Generic;

namespace MuonTraSach.BLL
{
    public class TheLoaiBLL
    {
        private readonly TheLoaiDAL theLoaiDAL = new TheLoaiDAL();

        public List<TheLoaiDTO> LayTatCa()
        {
            return theLoaiDAL.LayTatCa();
        }

        public List<TheLoaiDTO> LayDangHoatDong()
        {
            return theLoaiDAL.LayDangHoatDong();
        }

        public List<TheLoaiDTO> LayDanhSach(string tuKhoa, int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            return theLoaiDAL.LayDanhSach(tuKhoa, page, pageSize);
        }

        public int DemSoDong(string tuKhoa)
        {
            return theLoaiDAL.DemSoDong(tuKhoa);
        }

        public bool Them(TheLoaiDTO theLoai, out string message)
        {
            message = "";

            if (theLoai == null)
            {
                message = "Dữ liệu thể loại không hợp lệ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(theLoai.MaTheLoai))
            {
                message = "Mã thể loại không được bỏ trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(theLoai.TenTheLoai))
            {
                message = "Tên thể loại không được bỏ trống.";
                return false;
            }

            if (theLoaiDAL.KiemTraMaTonTai(theLoai.MaTheLoai))
            {
                message = "Mã thể loại đã tồn tại.";
                return false;
            }

            bool result = theLoaiDAL.Them(theLoai);

            if (result)
            {
                message = "Thêm thể loại thành công.";
                return true;
            }

            message = "Thêm thể loại thất bại.";
            return false;
        }

        public bool Sua(TheLoaiDTO theLoai, out string message)
        {
            message = "";

            if (theLoai == null)
            {
                message = "Dữ liệu thể loại không hợp lệ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(theLoai.MaTheLoai))
            {
                message = "Vui lòng chọn thể loại cần sửa.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(theLoai.TenTheLoai))
            {
                message = "Tên thể loại không được bỏ trống.";
                return false;
            }

            bool result = theLoaiDAL.Sua(theLoai);

            if (result)
            {
                message = "Cập nhật thể loại thành công.";
                return true;
            }

            message = "Cập nhật thể loại thất bại.";
            return false;
        }

        public bool NgungHoatDong(string maTheLoai, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTheLoai))
            {
                message = "Vui lòng chọn thể loại cần ngừng hoạt động.";
                return false;
            }

            bool result = theLoaiDAL.NgungHoatDong(maTheLoai);

            if (result)
            {
                message = "Ngừng hoạt động thể loại thành công.";
                return true;
            }

            message = "Ngừng hoạt động thể loại thất bại.";
            return false;
        }

        public bool HoatDong(string maTheLoai, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTheLoai))
            {
                message = "Vui lòng chọn thể loại cần kích hoạt lại.";
                return false;
            }

            bool result = theLoaiDAL.HoatDong(maTheLoai);

            if (result)
            {
                message = "Kích hoạt lại thể loại thành công.";
                return true;
            }

            message = "Kích hoạt lại thể loại thất bại.";
            return false;
        }

        public bool Xoa(string maTheLoai, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTheLoai))
            {
                message = "Vui lòng chọn thể loại cần xóa.";
                return false;
            }

            if (theLoaiDAL.KiemTraTheLoaiDangDuocDung(maTheLoai))
            {
                message = "Không thể xóa thể loại này vì đang có tài liệu thuộc thể loại. Vui lòng dùng chức năng Ngừng hoạt động.";
                return false;
            }

            bool result = theLoaiDAL.Xoa(maTheLoai);

            if (result)
            {
                message = "Xóa thể loại thành công.";
                return true;
            }

            message = "Xóa thể loại thất bại.";
            return false;
        }
    }
}