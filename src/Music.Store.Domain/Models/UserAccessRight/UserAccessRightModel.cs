using System.Collections.Generic;

namespace Music.Store.Domain.Models.UserAccessRight
{
    public class UserAccessRightModel
    {
        public UserAccessRightModel()
        {
            AccessRightIds = new List<int>();
        }

        public int UserId { get; set; }
        public List<int> AccessRightIds { get; set; }
    }
}
