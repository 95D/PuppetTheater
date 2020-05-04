using System;

namespace Viento.PuppetTheater.Utility
{
    public static class ObjectStatementsExtension
    {
        public static T SmartCast<T>(this object mine) 
        {
            if(mine.GetType().Equals(typeof(T)))
            {
                return (T) mine;
            }
            throw new InvalidCastException("Object.SmartCase<"+typeof(T).ToString()+"> failed: object is " + mine.GetType().ToString());
        }
        
        public static R AsDoing<T, R>(this object mine, Func<T, R> doing)
        {
            return doing(SmartCast<T>(mine));
        }
    }
}
