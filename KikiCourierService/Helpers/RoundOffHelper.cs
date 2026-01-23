using System;
using System.Collections.Generic;
using System.Text;

namespace KikiCourierService.Helpers
{
    public static class RoundOffHelper
    {  public static decimal roundOff(decimal value)
            {
                return Math.Round(value,2, MidpointRounding.ToNegativeInfinity);
            }

        
    }
}
