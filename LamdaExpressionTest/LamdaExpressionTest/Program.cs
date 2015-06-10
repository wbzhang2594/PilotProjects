using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LamdaExpressionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass pr = new TestClass();
            pr.run();
        }

    }
    public class TestClass
    {
        public void run()
        {
            LE.ExtractPropertyName(() => this.TestBool);

        }
        public bool TestBool { get; set; }
    }
    public class LE
    {
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }
            MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("propertyExpression");
            }
            PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("propertyExpression");
            }
            MethodInfo getMethod = propertyInfo.GetGetMethod(true);
            if (getMethod.IsStatic)
            {
                throw new ArgumentException("propertyExpression");
            }
            return memberExpression.Member.Name;
        }
    }
}
