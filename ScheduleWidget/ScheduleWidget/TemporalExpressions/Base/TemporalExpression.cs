using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleWidget.TemporalExpressions.Base
{
    public abstract class TemporalExpression
    {
        public abstract bool Includes(DateTime aDate);

        internal IEnumerable<Enum> GetFlags(Enum input)
        {
            return Enum.GetValues(input.GetType()).Cast<Enum>().Where(input.HasFlag);
        }
    }
}
