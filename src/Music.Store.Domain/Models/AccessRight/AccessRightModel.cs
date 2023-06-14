using System.Collections.Generic;

namespace Music.Store.Domain.Models.AccessRight
{
    public class AccessRightModel
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<AccessRightEndpointModel> AccessRightEndpoints { get; set; }
    }
}
