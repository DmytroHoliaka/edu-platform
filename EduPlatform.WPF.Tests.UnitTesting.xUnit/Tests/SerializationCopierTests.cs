using EduPlatform.WPF.Service.Utilities;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class SerializationCopierTests
{
    [Fact]
    public void DeepCopy_ShallowCopy_ReturnsСopiedObjectsDiffer()
    {
        // Arrange
        Outer original = new()
        {
            ValueType = 0
        };

        // Act
        Outer cloned = SerializationCopier.DeepCopy(original)!;
        cloned.ValueType = 1;

        // Assert
        Assert.NotEqual(original.ValueType, cloned.ValueType);
    }

    [Fact]
    public void DeepCopy_DeepCopy_ReturnsСopiedObjectsDiffer()
    {
        // Arrange
        Inner inner = new()
        {
            InnerValueType = 0
        };

        Outer original = new()
        {
            ReferenceType = inner
        };

        // Act
        Outer cloned = SerializationCopier.DeepCopy(original)!;
        cloned.ReferenceType!.InnerValueType = 1;

        // Assert
        Assert.NotEqual(original.ReferenceType.InnerValueType, cloned.ReferenceType.InnerValueType);
    }

    [Fact]
    public void DeepCopy_InfinityCycle_ReturnsObjectWithNullField()
    {
        // Arrange
        Outer parent = new();
        Outer original = new()
        {
            Parent = parent
        };
        parent.Parent = original;

        // Act
        Outer cloned = SerializationCopier.DeepCopy(original)!;

        // Assert
        Assert.Null(cloned.Parent!.Parent);
    }

    private class Outer
    {
        public int ValueType;
        public Inner? ReferenceType;
        public Outer? Parent;
    }

    private class Inner
    {
        public int InnerValueType;
    }
}