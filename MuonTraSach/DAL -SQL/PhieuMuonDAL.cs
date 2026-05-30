using MuonTraSach.DTO_chứa_dữ_liệu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuonTraSach.DAL___SQL
{
    public class PhieuMuonDAL : DBConnection
    {
        // ═══════════════════════════════════════════════
        //  HELPER
        // ═══════════════════════════════════════════════

        private DataTable ExecQuery(string sql, List<SqlParameter> prms = null)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                if (prms != null) da.SelectCommand.Parameters.AddRange(prms.ToArray());
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // ═══════════════════════════════════════════════
        //  TÌM KIẾM
        // ═══════════════════════════════════════════════

        public DataTable GetDocGia(string tuKhoa)
        {
            string sql = @"SELECT MaDocGia, HoTen, NgaySinh, LoaiDocGia, SoDienThoai, Email, DiaChi
                           FROM tblDocGia
                           WHERE TrangThai = 1
                             AND (MaDocGia LIKE @key OR HoTen LIKE @key)";
            return ExecQuery(sql, new List<SqlParameter>
            {
                new SqlParameter("@key", "%" + tuKhoa + "%")
            });
        }

        public DataTable GetTaiLieu(string tuKhoa)
        {
            string sql = @"SELECT tl.MaTaiLieu, tl.TenTaiLieu, ta.TenTacGia,
                                  tl.LoaiTaiLieu, tl.NamXuatBan,
                                  tl.SoLuongConLai, tl.TinhTrang
                           FROM tblTaiLieu tl
                           LEFT JOIN tblTacGia ta ON tl.MaTacGia = ta.MaTacGia
                           WHERE tl.TrangThai = 1
                             AND tl.SoLuongConLai > 0
                             AND (tl.TenTaiLieu LIKE @key OR tl.MaTaiLieu LIKE @key)";
            return ExecQuery(sql, new List<SqlParameter>
            {
                new SqlParameter("@key", "%" + tuKhoa + "%")
            });
        }

        /// <summary>
        /// Tìm phiếu mượn. NếumaDG_Filter != null thì chỉ lấy phiếu của độc giả đó.
        /// tuKhoa tìm theo MaPhieuMuon hoặc MaDocGia.
        /// trangThaiFilter: null = tất cả, có giá trị = lọc theo trạng thái.
        /// </summary>
        public DataTable GetPhieuMuon(string tuKhoa, string maDG_Filter, string trangThaiFilter)
        {
            string sql = @"SELECT pm.MaPhieuMuon, pm.MaDocGia, dg.HoTen AS TenDocGia,
                                  pm.MaNhanVien, pm.NgayMuon, pm.HanTra, pm.TrangThai
                           FROM tblPhieuMuon pm
                           LEFT JOIN tblDocGia dg ON pm.MaDocGia = dg.MaDocGia
                           WHERE 1=1";

            var prms = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(maDG_Filter))
            {
                sql += " AND pm.MaDocGia = @filter";
                prms.Add(new SqlParameter("@filter", maDG_Filter));
            }
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                sql += " AND (pm.MaPhieuMuon LIKE @key OR pm.MaDocGia LIKE @key)";
                prms.Add(new SqlParameter("@key", "%" + tuKhoa + "%"));
            }
            if (!string.IsNullOrEmpty(trangThaiFilter))
            {
                sql += " AND pm.TrangThai = @tt";
                prms.Add(new SqlParameter("@tt", trangThaiFilter));
            }

            sql += " ORDER BY pm.NgayMuon DESC";
            return ExecQuery(sql, prms);
        }

        public DataTable GetChiTietPhieuMuon(string maPM)
        {
            string sql = @"SELECT ct.MaCTPM, ct.MaTaiLieu, tl.TenTaiLieu,
                                  tl.LoaiTaiLieu, ct.SoLuongMuon, ct.TinhTrangTaiLieu
                           FROM tblChiTietPhieuMuon ct
                           JOIN tblTaiLieu tl ON ct.MaTaiLieu = tl.MaTaiLieu
                           WHERE ct.MaPhieuMuon = @mapm";
            return ExecQuery(sql, new List<SqlParameter>
            {
                new SqlParameter("@mapm", maPM)
            });
        }

        /// <summary>Lấy LoaiDocGia từ DB dựa trên mã độc giả (dùng cho NV mượn hộ).</summary>
        public string GetLoaiDocGia(string maDG)
        {
            string sql = "SELECT LoaiDocGia FROM tblDocGia WHERE MaDocGia = @madg";
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@madg", maDG);
                object result = cmd.ExecuteScalar();
                return result?.ToString() ?? "";
            }
            catch { return ""; }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }

        // ═══════════════════════════════════════════════
        //  ĐẾM SỐ LƯỢNG ĐANG MƯỢN
        // ═══════════════════════════════════════════════

        /// <summary>
        /// Đếm tổng số lượng tài liệu đang mượn (TrangThai = Đang mượn hoặc Đã gia hạn).
        /// isTapChi = true: đếm tạp chí; false: đếm giáo trình/SGK.
        /// </summary>
        public int CountTaiLieuDangMuon(string maDG, bool isTapChi)
        {
            string loai = isTapChi ? "Tạp chí" : "Giáo trình";
            string sql = @"
                SELECT ISNULL(SUM(ct.SoLuongMuon), 0)
                FROM tblChiTietPhieuMuon ct
                JOIN tblPhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                JOIN tblTaiLieu   tl ON ct.MaTaiLieu   = tl.MaTaiLieu
                WHERE pm.MaDocGia  = @madg
                  AND pm.TrangThai IN (@tt1, @tt2)
                  AND tl.LoaiTaiLieu LIKE '%' + @loai + '%'";
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@madg", maDG);
                cmd.Parameters.AddWithValue("@tt1", TrangThai.DangMuon);
                cmd.Parameters.AddWithValue("@tt2", TrangThai.DaGiaHan);
                cmd.Parameters.AddWithValue("@loai", loai);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return 0; }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }

        // ═══════════════════════════════════════════════
        //  SINH MÃ TỰ ĐỘNG (an toàn với transaction)
        // ═══════════════════════════════════════════════

        /// <summary>Sinh mã phiếu mượn mới — gọi BÊN TRONG transaction đang mở.</summary>
        private string TaoMaPhieuMuon(SqlTransaction trans)
        {
            string sql = "SELECT TOP 1 MaPhieuMuon FROM tblPhieuMuon ORDER BY MaPhieuMuon DESC";
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            object result = cmd.ExecuteScalar();
            if (result == null) return "PM0001";
            int so = int.Parse(result.ToString().Substring(2));
            return "PM" + (so + 1).ToString("D4");
        }

        /// <summary>Sinh mã CTPM mới — gọi BÊN TRONG transaction, truyền offset để tránh trùng trong vòng lặp.</summary>
        private string TaoMaCTPM(SqlTransaction trans, int offset)
        {
            string sql = "SELECT TOP 1 MaCTPM FROM tblChiTietPhieuMuon ORDER BY MaCTPM DESC";
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            object result = cmd.ExecuteScalar();
            int so = (result == null) ? 0 : int.Parse(result.ToString().Substring(2));
            return "CT" + (so + 1 + offset).ToString("D4");
        }

        private string TaoMaGiaHan(SqlTransaction trans)
        {
            string sql = "SELECT TOP 1 MaGiaHan FROM tblLichSuGiaHan ORDER BY MaGiaHan DESC";
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            object result = cmd.ExecuteScalar();
            if (result == null) return "GH0001";
            int so = int.Parse(result.ToString().Substring(2));
            return "GH" + (so + 1).ToString("D4");
        }

        // ═══════════════════════════════════════════════
        //  INSERT PHIẾU MƯỢN + CHI TIẾT (transaction)
        // ═══════════════════════════════════════════════

        /// <summary>
        /// Trả về mã phiếu mượn mới nếu thành công, null nếu thất bại.
        /// Nội dung lỗi được trả qua tham số out errorMsg.
        /// </summary>
        public string InsertFullPhieu(PhieuMuonDTO pm, out string errorMsg)
        {
            errorMsg = null;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                string maPMMoi = TaoMaPhieuMuon(trans);

                // ── INSERT tblPhieuMuon ──────────────────────────
                string sqlPM = @"
                    INSERT INTO tblPhieuMuon
                        (MaPhieuMuon, MaDocGia, MaNhanVien, NgayMuon, HanTra, TrangThai)
                    VALUES
                        (@mapm, @madg, @manv, @ngaymuon, @ngaytra, @trangthai)";

                SqlCommand cmdPM = new SqlCommand(sqlPM, conn, trans);
                cmdPM.Parameters.AddWithValue("@mapm", maPMMoi);
                cmdPM.Parameters.AddWithValue("@madg", pm.MaDG);
                // MaNhanVien NOT NULL trong DB: nếu độc giả tự tạo thì để chuỗi rỗng
                // (hoặc bạn có thể cho phép NULL bằng cách ALTER TABLE — xem ghi chú)
                cmdPM.Parameters.AddWithValue("@manv",
                    string.IsNullOrEmpty(pm.MaNV) ? (object)"" : pm.MaNV);
                cmdPM.Parameters.AddWithValue("@ngaymuon", pm.NgayMuon.Date);
                cmdPM.Parameters.AddWithValue("@ngaytra", pm.NgayTra.Date);
                cmdPM.Parameters.AddWithValue("@trangthai", pm.TrangThai);
                cmdPM.ExecuteNonQuery();

                // ── INSERT tblChiTietPhieuMuon ───────────────────
                for (int i = 0; i < pm.DanhSachChiTiet.Count; i++)
                {
                    var item = pm.DanhSachChiTiet[i];
                    string maCT = TaoMaCTPM(trans, i); // offset i tránh trùng mã trong vòng lặp

                    string sqlCT = @"
                        INSERT INTO tblChiTietPhieuMuon
                            (MaCTPM, MaPhieuMuon, MaTaiLieu, SoLuongMuon, TinhTrangTaiLieu)
                        VALUES
                            (@mact, @mapm, @matl, @sl, @tt)";

                    SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                    cmdCT.Parameters.AddWithValue("@mact", maCT);
                    cmdCT.Parameters.AddWithValue("@mapm", maPMMoi);
                    cmdCT.Parameters.AddWithValue("@matl", item.MaTaiLieu);
                    cmdCT.Parameters.AddWithValue("@sl", item.SoLuong);
                    cmdCT.Parameters.AddWithValue("@tt", item.TinhTrang ?? "Mới");
                    cmdCT.ExecuteNonQuery();

                    // ── Trừ SoLuongConLai ────────────────────────
                    string sqlTru = @"
                        UPDATE tblTaiLieu
                        SET SoLuongConLai = SoLuongConLai - @sl
                        WHERE MaTaiLieu = @matl AND SoLuongConLai >= @sl";
                    SqlCommand cmdTru = new SqlCommand(sqlTru, conn, trans);
                    cmdTru.Parameters.AddWithValue("@sl", item.SoLuong);
                    cmdTru.Parameters.AddWithValue("@matl", item.MaTaiLieu);
                    int affected = cmdTru.ExecuteNonQuery();
                    if (affected == 0)
                        throw new Exception($"Tài liệu {item.MaTaiLieu} không đủ số lượng!");
                }

                trans.Commit();
                return maPMMoi;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                errorMsg = ex.Message;
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // ═══════════════════════════════════════════════
        //  CẬP NHẬT TRẠNG THÁI
        // ═══════════════════════════════════════════════

        /// <summary>Cập nhật TrangThai (và tùy chọn HanTra, MaNhanVien) cho phiếu mượn.</summary>
        public bool UpdateTrangThai(string maPM, string trangThaiMoi,
                                    DateTime? hanTraMoi = null, string maNV = null)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string sql = "UPDATE tblPhieuMuon SET TrangThai = @tt";
                if (hanTraMoi.HasValue) sql += ", HanTra = @ht";
                if (!string.IsNullOrEmpty(maNV)) sql += ", MaNhanVien = @nv";
                sql += " WHERE MaPhieuMuon = @mapm";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tt", trangThaiMoi);
                cmd.Parameters.AddWithValue("@mapm", maPM);
                if (hanTraMoi.HasValue) cmd.Parameters.AddWithValue("@ht", hanTraMoi.Value.Date);
                if (!string.IsNullOrEmpty(maNV)) cmd.Parameters.AddWithValue("@nv", maNV);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }

        /// <summary>Cập nhật toàn bộ thông tin phiếu (dùng cho nút Cập nhật của NV).</summary>
        public bool UpdateToanBoPhieu(PhieuMuonDTO pm)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string sql = @"UPDATE tblPhieuMuon
                               SET NgayMuon = @nm, HanTra = @ht, TrangThai = @tt, MaNhanVien = @nv
                               WHERE MaPhieuMuon = @mapm";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nm", pm.NgayMuon.Date);
                cmd.Parameters.AddWithValue("@ht", pm.NgayTra.Date);
                cmd.Parameters.AddWithValue("@tt", pm.TrangThai);
                cmd.Parameters.AddWithValue("@nv", pm.MaNV ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@mapm", pm.MaPM);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }

        // ═══════════════════════════════════════════════
        //  GIA HẠN — ghi log tblLichSuGiaHan + cập nhật HanTra
        // ═══════════════════════════════════════════════

        public bool GiaHan(string maPM, DateTime hanTraCu, DateTime hanTraMoi, out string errorMsg)
        {
            errorMsg = null;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                // 1. Cập nhật phiếu mượn
                string sqlUpdate = @"UPDATE tblPhieuMuon
                                     SET TrangThai = @tt, HanTra = @ht
                                     WHERE MaPhieuMuon = @mapm";
                SqlCommand cmdU = new SqlCommand(sqlUpdate, conn, trans);
                cmdU.Parameters.AddWithValue("@tt", TrangThai.DaGiaHan);
                cmdU.Parameters.AddWithValue("@ht", hanTraMoi.Date);
                cmdU.Parameters.AddWithValue("@mapm", maPM);
                cmdU.ExecuteNonQuery();

                // 2. Ghi log lịch sử gia hạn
                string maGH = TaoMaGiaHan(trans);
                string sqlLog = @"INSERT INTO tblLichSuGiaHan
                                      (MaGiaHan, MaPhieuMuon, NgayGiaHan, HanTraCu, HanTraMoi, SoLanGiaHan)
                                  VALUES (@magh, @mapm, @ngaygh, @cu, @moi, 1)";
                SqlCommand cmdL = new SqlCommand(sqlLog, conn, trans);
                cmdL.Parameters.AddWithValue("@magh", maGH);
                cmdL.Parameters.AddWithValue("@mapm", maPM);
                cmdL.Parameters.AddWithValue("@ngaygh", DateTime.Today);
                cmdL.Parameters.AddWithValue("@cu", hanTraCu.Date);
                cmdL.Parameters.AddWithValue("@moi", hanTraMoi.Date);
                cmdL.ExecuteNonQuery();

                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                errorMsg = ex.Message;
                return false;
            }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }

        // ═══════════════════════════════════════════════
        //  TRẢ SÁCH — cộng lại SoLuongConLai + cập nhật TrangThai
        // ═══════════════════════════════════════════════

        public bool TraSach(string maPM, string maNV, out string errorMsg)
        {
            errorMsg = null;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                // 1. Cập nhật trạng thái phiếu
                string sqlPM = @"UPDATE tblPhieuMuon
                                 SET TrangThai = @tt, MaNhanVien = @nv
                                 WHERE MaPhieuMuon = @mapm";
                SqlCommand cmdPM = new SqlCommand(sqlPM, conn, trans);
                cmdPM.Parameters.AddWithValue("@tt", TrangThai.DaTra);
                cmdPM.Parameters.AddWithValue("@nv", maNV ?? (object)DBNull.Value);
                cmdPM.Parameters.AddWithValue("@mapm", maPM);
                cmdPM.ExecuteNonQuery();

                // 2. Cộng lại số lượng từng tài liệu
                string sqlItems = @"SELECT MaTaiLieu, SoLuongMuon
                                    FROM tblChiTietPhieuMuon WHERE MaPhieuMuon = @mapm";
                SqlCommand cmdItems = new SqlCommand(sqlItems, conn, trans);
                cmdItems.Parameters.AddWithValue("@mapm", maPM);
                SqlDataReader reader = cmdItems.ExecuteReader();
                var dsTL = new List<(string ma, int sl)>();
                while (reader.Read())
                    dsTL.Add((reader["MaTaiLieu"].ToString(), Convert.ToInt32(reader["SoLuongMuon"])));
                reader.Close();

                foreach (var (ma, sl) in dsTL)
                {
                    string sqlCong = @"UPDATE tblTaiLieu
                                       SET SoLuongConLai = SoLuongConLai + @sl
                                       WHERE MaTaiLieu = @matl";
                    SqlCommand cmdC = new SqlCommand(sqlCong, conn, trans);
                    cmdC.Parameters.AddWithValue("@sl", sl);
                    cmdC.Parameters.AddWithValue("@matl", ma);
                    cmdC.ExecuteNonQuery();
                }

                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                errorMsg = ex.Message;
                return false;
            }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }
    }
}
