using MuonTraSach.DAL;
using MuonTraSach.DTO;
using System.Collections.Generic;

namespace MuonTraSach.BLL
{
    public class TheLoaiBLL
    {
        private readonly TheLoaiDAL theLoaiDAL = new TheLoaiDAL();

        public List<TheLoaiDTO> LayDangHoatDong()
        {
            return theLoaiDAL.LayDangHoatDong();
        }

        public List<TheLoaiDTO> LayTatCa()
        {
            return theLoaiDAL.LayTatCa();
        }
    }
}