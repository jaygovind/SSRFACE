

namespace SMP.BAL.DTO
{

    using SMP.DATA.Models;

    public class UsersDTO : Users
    {

        public string UserIdString { get; set; }
        public string CreatedDatestring { get; set; }

        public int serialno { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }



        public bool? IsAdminApproved { get; set; }
        public long rownumber { get; set; }

    }
}
