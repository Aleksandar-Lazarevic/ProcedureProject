using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iProcedure.Crop
{
    public class TouchActionEventArgs : EventArgs
    {
#pragma warning disable CS1591
        public TouchActionEventArgs(long id, TouchActionType type, Point location, bool isInContact)

        {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }

        public long Id { private set; get; }

        public TouchActionType Type { private set; get; }

        public Point Location { private set; get; }

        public bool IsInContact { private set; get; }
#pragma warning restore CS1591
    }
}
