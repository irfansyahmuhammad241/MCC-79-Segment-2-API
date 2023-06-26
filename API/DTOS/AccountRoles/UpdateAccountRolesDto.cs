namespace API.DTOS.AccountRoles
{
    public class UpdateAccountRolesDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }

        public Guid RoleGuid { get; set; }
    }
}
