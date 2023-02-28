namespace Game
{
    public static class IntExtensions
    {
        public static string MoneyShort(this int money)
        {
            string suf = string.Empty;
            float value = 0;

            if (money >= 1000000000) // Миллиард
            {
                suf = "ML";
                value = money / 1000000000f;
            }
            else if (money >= 1000000)   // Миллион
            {
                suf = "M";
                value = money / 1000000f;
            }
            //else if (money > 1000) // Тысяча
            //{
            //    suf = "K";
            //    value = money / 1000f;
            //}

            if (value != 0) 
            {
                return value.ToString("0.0") + suf + "$";
            }

            return money.ToString() + "$";
        }


    }
}
