using System;
using System.Linq;

namespace ScheduleWidget.TemporalExpressions.Base
{
    public class TemporalExpressionUnion : TemporalExpressionCollection
    {
        /// <summary>
        /// Returns true if the inclusive expression is true and the exclusive expression is false
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            return Expressions.Any(e => e.Includes(aDate));
        }
    }
}
