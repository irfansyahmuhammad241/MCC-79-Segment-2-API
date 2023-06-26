namespace API.DTOS.AccountRoles
{
    public class GetAccountRolesDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }

        public Guid RoleGuid { get; set; }
    }
}
