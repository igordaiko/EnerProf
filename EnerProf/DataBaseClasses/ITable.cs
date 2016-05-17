using System;
using System.Data.SqlClient;
namespace EnerProf.DataBaseClasses
{
    public interface ITable
    {
        SqlDataAdapter Adapter { get; set; }
        string queryUpdate { get; set; }

    }
}
