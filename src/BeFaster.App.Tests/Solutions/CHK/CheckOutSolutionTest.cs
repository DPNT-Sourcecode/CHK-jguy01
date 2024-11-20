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
        [TestCase("SSSSSTTTT", ExpectedResult = 135)]
        [TestCase("KK", ExpectedResult = 120)]
        [TestCase("KKK", ExpectedResult = 190)]
        [TestCase("KKKK", ExpectedResult = 240)]
        [TestCase("SSSZ", ExpectedResult = 65)]
        [TestCase("ZZZS", ExpectedResult = 65)]
        [TestCase("STXZ", ExpectedResult = 62)]
        public int ComputePrice_ReturnsCorrectTotal(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}




