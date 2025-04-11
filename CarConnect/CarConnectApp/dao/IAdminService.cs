using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.entity;

namespace CarConnect.dao
{
    public interface IAdminService
    {
        Admin GetAdminById(int adminId);
        Admin GetAdminByUsername(string username);
        void RegisterAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(int adminId);
    }
}
