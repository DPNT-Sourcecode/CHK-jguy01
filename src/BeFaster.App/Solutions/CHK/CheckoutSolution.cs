using BeFaster.Runner.Exceptions;
using System.Diagnostics;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        // Prices dictionary
        private static readonly Dictionary<char, int> Prices = new()
        {
            { 'A', 50 }, { 'B', 30 }, { 'C', 20 }, { 'D', 15 },
            { 'E', 40 }, { 'F', 10 }, { 'G', 20 }, { 'H', 10 },
            { 'I', 35 }, { 'J', 60 }, { 'K', 70 }, { 'L', 90 },
            { 'M', 15 }, { 'N', 40 }, { 'O', 10 }, { 'P', 50 },
            { 'Q', 30 }, { 'R', 50 }, { 'S', 20 }, { 'T', 20 },
            { 'U', 40 }, { 'V', 50 }, { 'W', 20 }, { 'X', 17 },
            { 'Y', 20 }, { 'Z', 21 },
        };

        // Special offers dictionary
        private static readonly Dictionary<char, List<(int specialQuantity, int specialPrice)>> SpecialOffers = new()
        {
            { 'A', new List<(int specialQuantity, int specialPrice)> { (5, 200), (3, 130) } },
            { 'B', new List<(int specialQuantity, int specialPrice)> { (2, 45) } },
            { 'H', new List<(int specialQuantity, int specialPrice)> { (10, 80), (5, 45) } },
            { 'K', new List<(int specialQuantity, int specialPrice)> { (2, 150) } },
            { 'P', new List<(int specialQuantity, int specialPrice)> { (5, 200) } },
            { 'Q', new List<(int specialQuantity, int specialPrice)> { (3, 80) } },
            { 'B', new List<(int specialQuantity, int specialPrice)> { (3, 130), (2, 90) } },
        };

        // "Buy X, get Y free" rules
        private static readonly List<(char buyer, int buyQuantity, char freeItem)> FreeItemOffers = new()
        {
            ('E', 2, 'B'), // 2E get 1B free
            ('F', 3, 'F'), // 2F get 1F free minimum 3Fs
            ('N', 3, 'M'), // 3N get 1M free
            ('R', 3, 'Q'), // 3R get 1Q free
            ('U', 4, 'U'), // 3U get 1U free minimum 4Us
        };

        public static int ComputePrice(string? skus)
        {
            if (string.IsNullOrWhiteSpace(skus))
            {
                return 0;
            }

            var skuCount = CountSkus(skus);
            var count = CountSkus(skus);
            if (count == null)
            {
                return -1;
            }

            ApplyFreeItemOffers(skuCount);

            

            return CalculateTotalPrice(skuCount);
        }

        // Count occurance of each SKU to get quantity purchased
        private static Dictionary<char, int> CountSkus(string skus) 
        {
            var skuCount = new Dictionary<char, int>();
            foreach (var sku in skus)
            {
                // Invalid SKU
                if (!Prices.ContainsKey(sku))
                {
                    return null;
                }

                // Add SKU to the count if does not exist
                if (!skuCount.ContainsKey(sku))
                {
                    skuCount[sku] = 0;
                }

                // Increment the SKU count by one for each occurance
                skuCount[sku]++;
            }

            return skuCount;
        }

        // Apply "Buy X, get Y free" offers
        private static void ApplyFreeItemOffers(Dictionary<char, int> skuCount)
        {
            foreach(var (buyer, buyerQuantity, freeItem) in FreeItemOffers)
            {
                if(skuCount.ContainsKey(buyer) && skuCount.ContainsKey(freeItem))
                {
                    int eligibleFreeCount = skuCount[buyer] /buyerQuantity;
                    skuCount[freeItem] = Math.Max(0, skuCount[freeItem] - eligibleFreeCount);
                }
            }
        }
    
        // Calculate total price
        private static int CalculateTotalPrice(Dictionary<char, int> skuCount)
        {
            int totalPrice = 0;

            foreach (var item in skuCount)
            {
                char sku = item.Key;
                int quantity = item.Value;

                if (SpecialOffers.ContainsKey(sku))
                {
                    // Apply special offers in desc order of quantity for maximum customer benifit
                    foreach (var (specialQuantity, specialPrice) in SpecialOffers[sku])
                    {
                        totalPrice += (quantity / specialQuantity) * specialPrice; // Apply offer
                        quantity %= specialQuantity; // Get remaining items after offer applied
                    }
                }

                // Remaining items added at full price
                totalPrice += quantity * Prices[sku];
            }

            return totalPrice;
        }
    }
}



