using System;

namespace OneAttributeRule
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class OneAttributeRuleAttribute : Attribute
    {
        public OneAttributeRuleAttribute()
        {
        }
    }
}