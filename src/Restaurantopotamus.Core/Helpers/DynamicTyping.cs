using System;

namespace Restaurantopotamus.Core.Helpers
{
    public static class DynamicTyping
    {
        public static T ConvertValue<T>(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}