using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueData
{
    public enum TargetingType
    {
        Invalid = -1,
        Self = 0,
        Target = 1,
        Area = 2,
        Cone = 3,
        SelfAOE = 4,
        TargetOrLocation = 5,
        Location = 6,
        Direction = 7,
        DragDirection = 8,
        LineTargetToCaster = 9,
    };

}
