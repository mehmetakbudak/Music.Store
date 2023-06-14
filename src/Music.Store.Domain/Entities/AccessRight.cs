using System.Collections.Generic;

namespace Music.Store.Domain.Entities
{
    public class AccessRight : EntityBase
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<AccessRightEndpoint> AccessRightEndpoints { get; set; }
        public List<UserAccessRight> UserAccessRights { get; set; }
    }
}
