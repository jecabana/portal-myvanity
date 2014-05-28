using System;
using System.Linq.Expressions;

namespace MyVanity.Views.Filters
{
    public class TypedFilter<T> : FilterInformation 
    {
        public TypedFilter(int pageSize, int pageNumb)
            : base(pageSize, pageNumb)
        { }

        public TypedFilter(Expression<Func<T, bool>> filter, int pageSize, int pageNumb)
            : base(pageSize, pageNumb)
        {
            Filter = filter;
        }

        public Expression<Func<T, bool>> Filter { get; set; }
    }
}
