using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum Priority
    {
        Low = 3,
        Medium = 2,
        High = 1
    }

    public enum BloodType
    {
        O = 0,
        A = 1,
        B = 2,
        AB = 3
    }

    public enum RhType
    {
        Negative = 1,
        Positive = 2
    }

    public enum Stage
    {
        Rejected = 0,
        Pending = 1,
        Accepted = 2
    }

    public enum ProductType
    {
        Blood = 0,
        BloodCell = 1,
        Whitecell = 2,
        Plasma = 3
    }
}
