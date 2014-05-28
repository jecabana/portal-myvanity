using System;

namespace MyVanity.Common.Autofac
{
    class Binding
    {
        public Type Interface { get; set; }
        public Type Implementation { get; set; }
        public LifetimeAbbr Lifetime { get; set; }
    }
}