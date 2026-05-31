using MuonTraSach.DAL;
using MuonTraSach.DTO;
using System.Collections.Generic;

namespace MuonTraSach.BLL
{
    public class TaiLieuBLL
    {
        private readonly TaiLieuDAL taiLieuDAL = new TaiLieuDAL();

        public List<TaiLieuDTO> LayDanhSach(string tuKhoa, bool? trangThai, string maTheLoai, int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            return taiLieuDAL.LayDanhSach(tuKhoa, trangThai, maTheLoai, page, pageSize);
        }

        public int DemSoDong(string tuKhoa, bool? trangThai, string maTheLoai)
        {
            return taiLieuDAL.DemSoDong(tuKhoa, trangThai, maTheLoai);
        }

        public bool Them(TaiLieuDTO taiLieu, out string message)
        {
            message = "";

            if (taiLieu == null)
            {
                message = "Dữ liệu tài liệu không hợp lệ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiLieu.MaTaiLieu))
            {
                message = "Mã tài liệu không được bỏ trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiLieu.TenTaiLieu))
            {
                message = "Tên tài liệu không được bỏ trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiLieu.MaTacGia))
            {
                message = "Vui lòng chọn tác giả.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiLieu.MaTheLoai))
            {
                message = "Vui lòng chọn thể loại.";
                return false;
            }

            if (taiLieu.SoLuongHienCo < 0)
            {
                message = "Số lượng hiện có không được nhỏ hơn 0.";
                return false;
            }

            if (taiLieu.SoLuongConLai < 0)
            {
                message = "Số lượng còn lại không được nhỏ hơn 0.";
                return false;
            }

            if (taiLieu.SoLuongConLai > taiLieu.SoLuongHienCo)
            {
                message = "Số lượng còn lại không được lớn hơn số lượng hiện có.";
                return false;
            }

            if (taiLieu.NamXuatBan.HasValue && taiLieu.NamXuatBan.Value < 0)
            {
                message = "Năm xuất bản không hợp lệ.";
                return false;
            }

            if (taiLieuDAL.KiemTraMaTonTai(taiLieu.MaTaiLieu))
            {
                message = "Mã tài liệu đã tồn tại.";
                return false;
            }

            bool result = taiLieuDAL.Them(taiLieu);

            if (result)
            {
                message = "Thêm tài liệu thành công.";
                return true;
            }

            message = "Thêm tài liệu thất bại.";
            return false;
        }

        public bool Sua(TaiLieuDTO taiLieu, out string message)
        {
            message = "";

            if (taiLieu == null)
            {
                message = "Dữ liệu tài liệu không hợp lệ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiLieu.MaTaiLieu))
            {
                message = "Vui lòng chọn tài liệu cần sửa.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiLieu.TenTaiLieu))
            {
                message = "Tên tài liệu không được bỏ trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiLieu.MaTacGia))
            {
                message = "Vui lòng chọn tác giả.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiLieu.MaTheLoai))
            {
                message = "Vui lòng chọn thể loại.";
                return false;
            }

            if (taiLieu.SoLuongHienCo < 0)
            {
                message = "Số lượng hiện có không được nhỏ hơn 0.";
                return false;
            }

            if (taiLieu.SoLuongConLai < 0)
            {
                message = "Số lượng còn lại không được nhỏ hơn 0.";
                return false;
            }

            if (taiLieu.SoLuongConLai > taiLieu.SoLuongHienCo)
            {
                message = "Số lượng còn lại không được lớn hơn số lượng hiện có.";
                return false;
            }

            if (taiLieu.NamXuatBan.HasValue && taiLieu.NamXuatBan.Value < 0)
            {
                message = "Năm xuất bản không hợp lệ.";
                return false;
            }

            bool result = taiLieuDAL.Sua(taiLieu);

            if (result)
            {
                message = "Cập nhật tài liệu thành công.";
                return true;
            }

            message = "Cập nhật tài liệu thất bại.";
            return false;
        }

        public bool NgungHoatDong(string maTaiLieu, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTaiLieu))
            {
                message = "Vui lòng chọn tài liệu cần ngừng hoạt động.";
                return false;
            }

            bool result = taiLieuDAL.NgungHoatDong(maTaiLieu);

            if (result)
            {
                message = "Ngừng hoạt động tài liệu thành công.";
                return true;
            }

            message = "Ngừng hoạt động tài liệu thất bại.";
            return false;
        }
        public bool HoatDong(string maTaiLieu, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTaiLieu))
            {
                message = "Vui lòng chọn tài liệu cần kích hoạt lại.";
                return false;
            }

            bool result = taiLieuDAL.HoatDong(maTaiLieu);

            if (result)
            {
                message = "Kích hoạt lại tài liệu thành công.";
                return true;
            }

            message = "Kích hoạt lại tài liệu thất bại.";
            return false;
        }
        public bool Xoa(string maTaiLieu, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(maTaiLieu))
            {
                message = "Vui lòng chọn tài liệu cần xóa.";
                return false;
            }

            if (taiLieuDAL.KiemTraTaiLieuDangDuocMuon(maTaiLieu))
            {
                message = "Không thể xóa tài liệu này vì đã phát sinh trong phiếu mượn. Vui lòng dùng chức năng Ngừng hoạt động.";
                return false;
            }

            bool result = taiLieuDAL.Xoa(maTaiLieu);

            if (result)
            {
                message = "Xóa tài liệu thành công.";
                return true;
            }

            message = "Xóa tài liệu thất bại.";
            return false;
        }
    }
}