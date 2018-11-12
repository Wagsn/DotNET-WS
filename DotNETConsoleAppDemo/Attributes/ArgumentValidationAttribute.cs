using System;
using System.Reflection;

namespace WS.Core.Attributes
{
    /// <summary>
    /// 用特性进行参数校验，缺点是不利于错误栈的维护
    /// </summary>
    public abstract class ArgumentValidationAttribute : Attribute
    {
        public abstract void Validate(object value, string argumentName);
    }
    /// <summary>
    /// 返回检查
    /// </summary>
    public class InRangeAttribute : ArgumentValidationAttribute
    {
        private int min;
        private int max;

        public InRangeAttribute(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public override void Validate(object value, string argumentName)
        {
            int intValue = (int)value;
            if (intValue < min || intValue > max)
            {
                throw new ArgumentOutOfRangeException(argumentName, string.Format("min={0},max={1}", min, max));
            }
        }
    }
    /// <summary>
    /// 空检查
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class NotNullAttribute : ArgumentValidationAttribute
    {
        public override void Validate(object value, string argumentName)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);
        }
    }
    public interface IInvocation
    {
        void Proceed();
        object[] Arguments { get; }
        MethodInfo Method { get; }
        object ReturnValue { get; set; }
    }

    public interface IInterceptor
    {
        void Intercept(IInvocation invocation);
    }

    public class ValidationInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            ParameterInfo[] parameters = invocation.Method.GetParameters();
            for (int index = 0; index < parameters.Length; index++)
            {
                var paramInfo = parameters[index];
                var attributes = paramInfo.GetCustomAttributes(typeof(ArgumentValidationAttribute), false);

                if (attributes.Length == 0)
                    continue;

                foreach (ArgumentValidationAttribute attr in attributes)
                {
                    attr.Validate(invocation.Arguments[index], paramInfo.Name);
                }
            }

            invocation.Proceed();
        }
    }
}
