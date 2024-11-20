using BeFaster.Runner.Exceptions;

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

        public static int ComputePrice(string? skus)
        {
            if (string.IsNullOrWhiteSpace(skus))
            {
                return 0;
            }

         

            var specialOffers = new Dictionary<char, List<(int specialQuantity, int specialPrice)>>()
            {
                
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

            // Handle special offer: 2F get one F free
            if (skuCount.ContainsKey('F'))
            {
                int fCount = skuCount['F'];
                int chargeableCount = (fCount / 3) * 2 + (fCount % 3); // For every 3 Fs, charge only for 2 and the remaining Fs charge full price
                skuCount['F'] = chargeableCount; // Update the new F count for price calculation
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

