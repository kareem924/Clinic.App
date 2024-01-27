using System.Reflection;

namespace TherapyTrack.BuildingBlocks.Domain
{
    public  abstract class ValueObject : IEquatable<ValueObject>
    {
      
        public override bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }
            ValueObject other = (ValueObject)obj;
            var isPropertiesAreEqual = GetAtomicValues().SequenceEqual(other.GetAtomicValues());

            return isPropertiesAreEqual;
        }

        public bool Equals(ValueObject? other)
        {
            if (other is null)
            {
                return false;
            }
            return Equals(other as object);
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        protected virtual IEnumerable<object> GetAtomicValues()
        {
            var proprties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.GetGetMethod() != null);
            foreach (var property in proprties)
            {
                yield return property.GetValue(this);
            }
        }
    }
}
