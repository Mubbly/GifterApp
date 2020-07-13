using System;

namespace BLL.App.Helpers
{
    public class Enums
    {
        public Enums()
        {
            
        }
        
        public enum Status
        {
            Active = 1,
            Reserved = 2,
            Archived = 3
        }

        public enum ActionType
        {
            Activate = 1,
            Reserve = 2,
            Archive = 3
        }

        /**
         * Returns Guid id in string format from the db, corresponding to given param.
         * Needs to be kept in sync with db
         */
        public string GetActionTypeId(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Activate:
                    return "00000000-0000-0000-0000-000000000001";
                case ActionType.Reserve:
                    return "00000000-0000-0000-0000-000000000002";
                case ActionType.Archive:
                    return "00000000-0000-0000-0000-000000000003";
                default:
                    throw new NotSupportedException($"No such ActionType found in enum: {actionType}");
            }
        }
        
        /**
         * Returns Guid id in string format from the db, corresponding to given param.
         * Needs to be kept in sync with db
         */
        public string GetStatusId(Status status)
        {
            switch (status)
            {
                case Status.Active:
                    return "00000000-0000-0000-0000-000000000001";
                case Status.Reserved:
                    return "00000000-0000-0000-0000-000000000002";
                case Status.Archived:
                    return "00000000-0000-0000-0000-000000000003";
                default:
                    throw new NotSupportedException($"No such Status found in enum: {status}");
            }
        }
    }
}