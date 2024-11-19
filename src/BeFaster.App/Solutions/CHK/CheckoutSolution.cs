using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string? skus)
        {
            if (string.IsNullOrWhiteSpace(skus))
            {
                return -1;
            }

            var prices = new Dictionary<char, int>()
            {
                { 'A', 50 },
                { 'B', 30 },
                { 'C', 20 },
                { 'D', 15 },
            };

            var specialOffers = new Dictionary<char, (int quantity, int specialPrice)>()
            {
                { 'A', (3, 130) },
                { 'B', (2, 45) },
            };
        }
    }
}

