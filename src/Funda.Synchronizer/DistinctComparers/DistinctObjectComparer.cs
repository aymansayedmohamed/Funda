using System.Collections.Generic;
using Funda.Data.Entities;

namespace Funda.Synchronizer.DistinctComparers
{
    class DistinctObjectComparer : IEqualityComparer<Object>
    {

        public bool Equals(Object x, Object y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Object obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
