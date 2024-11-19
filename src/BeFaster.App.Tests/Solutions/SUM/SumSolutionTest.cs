using BeFaster.App.Solutions.SUM;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.SUM
{
    [TestFixture]
    public class SumSolutionTest
    {
        [TestCase(1, 1, ExpectedResult = 2)]
        public int ComputeSum(int x, int y)
        {
            return SumSolution.Sum(x, y);
        }

        [TestCase(0, 0, ExpectedResult = 0)] // Minimum value
        [TestCase(100, 100, ExpectedResult = 200)] // Maximum value
        [TestCase(50, 50, ExpectedResult = 100)]
        [TestCase(20, 90, ExpectedResult = 110)]
        public int ComputeSum_ValidInputs(int x, int y)
        {
            return SumSolution.Sum(x, y);
        }

        [TestCase(-3, 0)] // Negative value
        [TestCase(0, -22)] // Negative value
        [TestCase(103, 0)] // Value above 100
        [TestCase(0, 103)] // Value above 100
        public void ComputeSum_InvalidInputs(int x, int y)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => SumSolution.Sum(x, y));
        }
    }
}
