using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MuonTraSach.DAL
{
    public class Databasehelper
    {
        private static readonly string connectionString =
            @"Data Source=DESKTOP-RCTQ0AM\SQLEXPRESS01;Initial Catalog=QuanLyThuVien;Integrated Security=True;Encrypt=False";

        public static IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

    }
}
