using System;
using System.Web.Mvc;

namespace MyVanity.Common.Autofac
{
    public static class Helpers
    {
        public static Func<T> PerHttpSafeResolve<T>()
        {
            return () => DependencyResolver.Current.GetService<T>();
        } 
    }
}
