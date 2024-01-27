namespace TherapyTrack.BuildingBlocks.Domain
{
    public abstract class TypedIdValueBase<T> : IEquatable<TypedIdValueBase<T>>
    {

        public T Value { get; }

        protected TypedIdValueBase(T value)
        {
            EnsureValidType();
            ValidateValue(value);
            Value = value;
        }



        public bool Equals(TypedIdValueBase<T>? other)
        {
            if (other is null || GetType() != other.GetType())
            {
                return false;
            }
            return EqualityComparer<T>.Default.Equals(Value, other.Value);

        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is TypedIdValueBase<T> other && Equals(other) && IsSameGenericType(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }


        private bool IsSameGenericType(object other)
        {
            if (other == null)
                return false;

            var type1 = GetType().GetGenericTypeDefinition();
            var type2 = other.GetType().GetGenericTypeDefinition();

            return type1 == type2;
        }

        private static void ValidateValue(T value)
        {
            if (value is Guid guidValue && guidValue == Guid.Empty)
            {
                throw new InvalidOperationException("Id value cannot be empty!");
            }
            if (value is int intValue && intValue <= 0)
            {
                throw new InvalidOperationException("Id value cannot be zero!");
            }
            if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
            {
                throw new InvalidOperationException("Id value cannot be empty or whitespace!");
            }
        }

        private static void EnsureValidType()
        {
            if (typeof(T) != typeof(int) && typeof(T) != typeof(Guid) && typeof(T) != typeof(string))
            {
                throw new InvalidOperationException("Type parameter T must be int, Guid, or string.");
            }
        }
    }
}
