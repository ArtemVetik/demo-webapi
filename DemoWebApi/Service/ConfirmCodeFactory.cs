namespace Service
{
    internal static class ConfirmCodeFactory
    {
        private static readonly byte Length = 4;
        private static readonly string Numbers = "0123456789";
        private static readonly Random Random = new Random();

        public static string CreateNew()
        {
            string otp = string.Empty;

            for (byte i = 0; i < Length; i++)
                otp += Numbers[Random.Next(0, Numbers.Length)];

            return otp;
        }
    }
}
