using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleWidget.TemporalExpressions.Base
{
    public class TemporalExpressionIntersection : TemporalExpressionCollection
    {
        public TemporalExpressionIntersection() 
        {
            Expressions = new List<TemporalExpression>();
        }

        /// <summary>
        /// Returns true if the inclusive expression is true and the exclusive expression is false
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            return Expressions.All(e => e.Includes(aDate));
        }
    }
}
