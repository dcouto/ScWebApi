namespace ScWebApi.Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullAndNotEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}