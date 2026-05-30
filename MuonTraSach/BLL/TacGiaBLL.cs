using MuonTraSach.DAL;
using MuonTraSach.DTO;
using System.Collections.Generic;

namespace MuonTraSach.BLL
{
    public class TacGiaBLL
    {
        private readonly TacGiaDAL tacGiaDAL = new TacGiaDAL();

        public List<TacGiaDTO> LayDangHoatDong()
        {
            return tacGiaDAL.LayDangHoatDong();
        }

        public List<TacGiaDTO> LayTatCa()
        {
            return tacGiaDAL.LayTatCa();
        }
    }
}