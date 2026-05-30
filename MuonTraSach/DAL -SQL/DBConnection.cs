using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MuonTraSach.DAL___SQL
{
    public class DBConnection
    {
        protected SqlConnection conn =
            new SqlConnection(
                @"Data Source=PC\PHAMHUE;Initial Catalog=QuanLyThuVien;Integrated Security=True;Encrypt=False");
    }
}
