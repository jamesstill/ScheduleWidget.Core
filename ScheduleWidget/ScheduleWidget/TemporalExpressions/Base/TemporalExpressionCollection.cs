using System.Collections.Generic;

namespace ScheduleWidget.TemporalExpressions.Base
{
    public abstract class TemporalExpressionCollection : TemporalExpression
    {
        protected TemporalExpressionCollection()
        {
            Expressions = new List<TemporalExpression>();
        }

        protected ICollection<TemporalExpression> Expressions { get; set; }

        /// <summary>
        /// Adds a temporal expression to the list
        /// </summary>
        /// <param name="expression">Temporal expression to add to the collection</param>
        public void Add(TemporalExpression expression)
        {
            Expressions.Add(expression);
        }
    }
}
