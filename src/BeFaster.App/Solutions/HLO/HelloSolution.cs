using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.HLO
{
    public static class HelloSolution
    {
        public static string Hello(string? friendName)
        {
            if (string.IsNullOrWhiteSpace(friendName))
            {
                throw new ArgumentException("Invalid input: friendName cannot be null, empty, or whitespace.", nameof(friendName));
            }
            return $"Hello, {friendName}!";
        }
    }
}
