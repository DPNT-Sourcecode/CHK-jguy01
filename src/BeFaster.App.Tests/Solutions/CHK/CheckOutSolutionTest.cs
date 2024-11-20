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
        public int ComputePrice_ReturnsCorrectTotal(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}
