using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class PropertyHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpr = propertyExpression.Body as MemberExpression;
            if (memberExpr == null)
                throw new ArgumentOutOfRangeException();
            string memberName = memberExpr.Member.Name;
            return memberName;
        }
    }
}
