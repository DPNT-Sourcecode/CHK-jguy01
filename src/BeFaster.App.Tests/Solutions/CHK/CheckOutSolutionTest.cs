using BeFaster.App.Solutions.CHK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    public class CheckOutSolutionTest
    {
        [TestCase("S", ExpectedResult = 20)]
        [TestCase("STX", ExpectedResult = 45)]
        [TestCase("STXS", ExpectedResult = 65)]
        [TestCase("SSSSSTTTT", ExpectedResult = 90)]
        [TestCase("KK", ExpectedResult = 120)]
        [TestCase("KKK", ExpectedResult = 190)]
        [TestCase("KKKK", ExpectedResult = 240)]
        public int ComputePrice_ReturnsCorrectTotal(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}


