using System.Collections.Generic;
using Nba.Domain.Abstract;

namespace Nba.Domain.Comparers
{
    public class ByNameEqualityComparer<T> : IEqualityComparer<T> where T: IHasName
    {
        public bool Equals(T x, T y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(T obj)
        {
            //Check whether the object is null
            if (ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = obj.Name?.GetHashCode() ?? 0;
            
            //Calculate the hash code for the product.
            return hashProductName;
        }
    }
}
