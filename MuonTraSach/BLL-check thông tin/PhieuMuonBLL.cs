using MuonTraSach.DAL___SQL;
using MuonTraSach.DTO_chứa_dữ_liệu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using static MuonTraSach.DTO_chứa_dữ_liệu.PhieuMuonDTO;

namespace MuonTraSach.BLL_check_thông_tin
{
   
        public class PhieuMuonBLL
        {
            private readonly PhieuMuonDAL dal = new PhieuMuonDAL();

            // ═══════════════════════════════════════════════
            //  TÌM KIẾM
            // ═══════════════════════════════════════════════

            public DataTable TimKiemDocGia(string tuKhoa)
            {
                if (!UserSession.IsNhanVienOrAdmin) return null; // Độc giả không được tìm người khác
                return dal.GetDocGia(tuKhoa);
            }

            public DataTable TimKiemTaiLieu(string tuKhoa)
            {
                return dal.GetTaiLieu(tuKhoa);
            }

            public DataTable TimKiemPhieuMuon(string tuKhoa, string trangThaiFilter)
            {
                // Độc giả chỉ thấy phiếu của mình
                string maDG_Filter = UserSession.IsNhanVienOrAdmin ? null : UserSession.MaNguoiDung;
                return dal.GetPhieuMuon(tuKhoa, maDG_Filter, trangThaiFilter);
            }

            public DataTable GetChiTietPhieuMuon(string maPM)
            {
                return dal.GetChiTietPhieuMuon(maPM);
            }

            // ═══════════════════════════════════════════════
            //  LƯU PHIẾU MƯỢN MỚI
            // ═══════════════════════════════════════════════

            /// <summary>Trả về "OK:PM0001" nếu thành công, hoặc chuỗi lỗi nếu thất bại.</summary>
            public string XuLyLuuPhieu(PhieuMuonDTO pm)
            {
                // ── Validate cơ bản ─────────────────────────────────
                if (string.IsNullOrEmpty(pm.MaDG))
                    return "Vui lòng chọn độc giả!";
                if (pm.DanhSachChiTiet.Count == 0)
                    return "Chưa chọn tài liệu nào!";
                if (pm.NgayTra.Date <= pm.NgayMuon.Date)
                    return "Hạn trả phải sau ngày mượn!";

                // ── Lấy loại độc giả từ DB (tránh hardcode) ─────────
                string loaiDG = UserSession.IsNhanVienOrAdmin
                    ? dal.GetLoaiDocGia(pm.MaDG)
                    : UserSession.LoaiDocGia;

                if (string.IsNullOrEmpty(loaiDG))
                    return "Không tìm thấy thông tin độc giả!";

                bool isCBGV = loaiDG.Contains("CBGV") || loaiDG.Contains("Giảng viên");
                int maxDays = isCBGV ? 10 : 8;

                if ((pm.NgayTra.Date - pm.NgayMuon.Date).TotalDays > maxDays)
                    return $"Loại độc giả này chỉ được mượn tối đa {maxDays} ngày!";

                // ── Giới hạn số lượng ────────────────────────────────
                int maxSach = isCBGV ? 3 : 2;
                int maxTapChi = 2;

                int sachDangMuon = dal.CountTaiLieuDangMuon(pm.MaDG, isTapChi: false);
                int tapChiDangMuon = dal.CountTaiLieuDangMuon(pm.MaDG, isTapChi: true);

                int sachMoi = pm.DanhSachChiTiet
                    .Where(x => !x.LoaiTaiLieu.Contains("Tạp chí"))
                    .Sum(x => x.SoLuong);
                int tapChiMoi = pm.DanhSachChiTiet
                    .Where(x => x.LoaiTaiLieu.Contains("Tạp chí"))
                    .Sum(x => x.SoLuong);

                if (sachDangMuon + sachMoi > maxSach)
                    return $"Đã vượt giới hạn {maxSach} Giáo trình/SGK!";
                if (tapChiDangMuon + tapChiMoi > maxTapChi)
                    return $"Đã vượt giới hạn {maxTapChi} Tạp chí!";

                // ── Gán trạng thái tự động ───────────────────────────
                pm.TrangThai = UserSession.IsNhanVienOrAdmin
                    ? TrangThai.DangMuon
                    : TrangThai.ChoDuyet;

                // Nhân viên đang đăng nhập là người tạo phiếu
                if (UserSession.IsNhanVienOrAdmin && string.IsNullOrEmpty(pm.MaNV))
                    pm.MaNV = UserSession.MaNguoiDung;

                string maMoi = dal.InsertFullPhieu(pm, out string err);
                if (maMoi != null) return "OK:" + maMoi;
                return "Lỗi lưu phiếu: " + err;
            }

            // ═══════════════════════════════════════════════
            //  GIA HẠN
            // ═══════════════════════════════════════════════

            public string GiaHanPhieu(string maPM, string trangThaiHienTai, DateTime hanTraCu)
            {
                if (trangThaiHienTai == TrangThai.DaGiaHan)
                    return "Phiếu này đã được gia hạn 1 lần rồi!";
                if (trangThaiHienTai != TrangThai.DangMuon)
                    return "Chỉ được gia hạn khi đang ở trạng thái 'Đang mượn'!";
                if (hanTraCu.Date < DateTime.Today)
                    return "Sách đã quá hạn, không thể gia hạn!";

                DateTime hanMoi = hanTraCu.Date.AddDays(2);
                bool ok = dal.GiaHan(maPM, hanTraCu, hanMoi, out string err);
                if (ok) return "OK:" + hanMoi.ToString("dd/MM/yyyy");
                return "Lỗi gia hạn: " + err;
            }

            // ═══════════════════════════════════════════════
            //  HỦY PHIẾU
            // ═══════════════════════════════════════════════

            public string HuyPhieuMuon(string maPM, string trangThaiHienTai)
            {
                if (trangThaiHienTai != TrangThai.ChoDuyet && trangThaiHienTai != TrangThai.DaDuyet)
                    return "Chỉ được hủy khi phiếu đang ở trạng thái 'Chờ duyệt' hoặc 'Đã duyệt'!";

                bool ok = dal.UpdateTrangThai(maPM, TrangThai.TuChoi);
                return ok ? "OK" : "Lỗi khi hủy phiếu!";
            }

            // ═══════════════════════════════════════════════
            //  TRẢ SÁCH
            // ═══════════════════════════════════════════════

            public string TraSach(string maPM)
            {
                if (!UserSession.IsNhanVienOrAdmin)
                    return "Chỉ nhân viên/admin mới được xác nhận trả sách!";
                bool ok = dal.TraSach(maPM, UserSession.MaNguoiDung, out string err);
                return ok ? "OK" : "Lỗi trả sách: " + err;
            }

            // ═══════════════════════════════════════════════
            //  DUYỆT / TỪ CHỐI (chỉ NV/Admin)
            // ═══════════════════════════════════════════════

            public string DuyetPhieu(string maPM)
            {
                if (!UserSession.IsNhanVienOrAdmin) return "Không có quyền!";
                bool ok = dal.UpdateTrangThai(maPM, TrangThai.DangMuon, null, UserSession.MaNguoiDung);
                return ok ? "OK" : "Lỗi duyệt phiếu!";
            }

            public string TuChoiPhieu(string maPM)
            {
                if (!UserSession.IsNhanVienOrAdmin) return "Không có quyền!";
                bool ok = dal.UpdateTrangThai(maPM, TrangThai.TuChoi, null, UserSession.MaNguoiDung);
                return ok ? "OK" : "Lỗi từ chối phiếu!";
            }

            // ═══════════════════════════════════════════════
            //  CẬP NHẬT TOÀN BỘ PHIẾU (NV/Admin)
            // ═══════════════════════════════════════════════

            public string CapNhatThongTinPhieu(PhieuMuonDTO pm)
            {
                // Độc giả chỉ được cập nhật khi phiếu đang "Chờ duyệt"
                if (!UserSession.IsNhanVienOrAdmin && pm.TrangThai != TrangThai.ChoDuyet)
                    return "Bạn chỉ được cập nhật khi phiếu đang ở trạng thái 'Chờ duyệt'!";

                if (pm.NgayTra.Date <= pm.NgayMuon.Date)
                    return "Hạn trả phải sau ngày mượn!";

                bool ok = dal.UpdateToanBoPhieu(pm);
                return ok ? "OK" : "Lỗi cập nhật!";
            }
        }
}
