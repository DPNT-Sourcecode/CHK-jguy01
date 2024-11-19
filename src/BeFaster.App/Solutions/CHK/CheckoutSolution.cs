using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string? skus)
        {
            if (string.IsNullOrWhiteSpace(skus))
            {
                return 0;
            }

            var prices = new Dictionary<char, int>()
            {
                { 'A', 50 },
                { 'B', 30 },
                { 'C', 20 },
                { 'D', 15 },
                { 'E', 40 },
                { 'F', 10 },
            };

            var specialOffers = new Dictionary<char, List<(int specialQuantity, int specialPrice)>>()
            {
                { 'A', new List<(int specialQuantity, int specialPrice)> { (5, 200), (3, 130) } },
                { 'B', new List<(int specialQuantity, int specialPrice)> { (2, 45) } },
            };

            // Count occurance of each SKU to get quantity purchased
            var skuCount = new Dictionary<char, int>();
            foreach (var sku in skus) 
            {
                // Invalid SKU
                if (!prices.ContainsKey(sku)) 
                {
                    return -1;
                }

                // Add SKU to the count if does not exist
                if (!skuCount.ContainsKey(sku))
                { 
                    skuCount[sku] = 0;
                }

                // Increment the SKU count by one for each occurance
                skuCount[sku]++;
            }

            // Handle special offer: 2E get one B free
            if (skuCount.ContainsKey('E') && skuCount.ContainsKey('B')) 
            {
                int freeBCount = skuCount['E'] / 2; // Number of B items
                skuCount['B'] = Math.Max(0, skuCount['B'] -  freeBCount); // Deduct free Bs from total B count
            }

            int totalPrice = 0;

            foreach (var item in skuCount) 
            {
                char sku = item.Key;
                int quantity = item.Value;

                if (specialOffers.ContainsKey(sku)) 
                {
                    // Apply special offers in desc order of quantity for maximum customer benifit
                    foreach (var (specialQuantity, specialPrice) in specialOffers[sku])
                    {
                        totalPrice += (quantity / specialQuantity) * specialPrice; // Apply offer
                        quantity %= specialQuantity; // Get remaining items after offer applied
                    }
                }

                // Remaining items added at full price
                totalPrice += quantity * prices[sku];
            }

            return totalPrice;
        }
    }
}



