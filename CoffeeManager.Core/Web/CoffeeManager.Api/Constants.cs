namespace CoffeeManager.Api
{
    public static class Constants
    {
        public const float ShiftRate = 0.25f;
        public const decimal MoneyRatePercent = 1;
        public const int MaxShiftAmountOversight = 500;
        public const int MinHoursForValidShift = 5;

        public const string AutoOrderMessage = "Добрый день!\nЗаказ на кофейню по адресу {0}\n{1}\nСпасибо";
    }
}