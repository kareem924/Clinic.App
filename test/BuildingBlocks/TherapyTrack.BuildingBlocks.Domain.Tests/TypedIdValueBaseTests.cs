namespace TherapyTrack.BuildingBlocks.Domain.Tests
{
    public class TypedIdValueBaseTests
    {

        [Fact]
        public void Constructor_WithInvalidType_ShouldThrow()
        {
            var action = () => new DecimalTypedIdValue(1);
            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Constructor_WithValidIntValue_ShouldNotThrow()
        {
            var action = () => new IntTypedIdValue(1);
            action.ShouldNotThrow();
        }

        [Fact]
        public void Constructor_WithInvalidIntValue_ShouldThrow()
        {
            var action = () => new IntTypedIdValue(0);
            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Equals_WithEqualValues_ShouldReturnTrue()
        {
            var id1 = new IntTypedIdValue(1);
            var id2 = new IntTypedIdValue(1);

            id1.Equals(id2).ShouldBeTrue();
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldReturnFalse()
        {
            var id1 = new IntTypedIdValue(1);
            var id2 = new IntTypedIdValue(2);

            id1.Equals(id2).ShouldBeFalse();
        }

        [Fact]
        public void Equals_WithDifferentTypes_ShouldReturnFalse()
        {
            var id1 = new IntTypedIdValue(1);
            var id2 = new StringTypedIdValue("1");

            id1.Equals(id2).ShouldBeFalse();
        }

        [Fact]
        public void GetHashCode_WithEqualValues_ShouldReturnSameHashCode()
        {
            var id1 = new IntTypedIdValue(1);
            var id2 = new IntTypedIdValue(1);

            id1.GetHashCode().ShouldBe(id2.GetHashCode());
        }

    }
    public class IntTypedIdValue : TypedIdValueBase<int>
    {
        public IntTypedIdValue(int value) : base(value)
        {
        }
    }

    public class StringTypedIdValue : TypedIdValueBase<string>
    {
        public StringTypedIdValue(string value) : base(value)
        {
        }
    }

    public class DecimalTypedIdValue : TypedIdValueBase<decimal>
    {
        public DecimalTypedIdValue(decimal value) : base(value)
        {
        }
    }
}