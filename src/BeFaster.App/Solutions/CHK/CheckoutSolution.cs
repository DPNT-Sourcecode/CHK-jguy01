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
            };

            var specialOffers = new Dictionary<char, (int specialQuantity, int specialPrice)>()
            {
                { 'A', (3, 130) },
                { 'B', (2, 45) },
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

            int totalPrice = 0;

            foreach (var item in skuCount) 
            {
                char sku = item.Key;
                int quantity = item.Value;

                if (specialOffers.ContainsKey(sku)) 
                {
                    var (specialQuantity, specialPrice) = specialOffers[sku];
                    totalPrice += (quantity / specialQuantity) * specialPrice; // Apply special offer
                    totalPrice += (quantity % specialQuantity) * prices[sku]; // Add remaining items at full price
                }
                else
                {
                    totalPrice += quantity * prices[sku]; // Full price without special offer
                }
            }

            return totalPrice;
        }
    }
}





