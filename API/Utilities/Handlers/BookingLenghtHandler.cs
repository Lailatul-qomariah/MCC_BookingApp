namespace API.Utilities.Handlers
{
    public class BookingLenghtHandler
    {
        public static int CalculateBookingLength(DateTime startDate, DateTime endDate, DateTime today)
        {
            int bookingLength = (endDate - startDate).Days + 1;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    bookingLength--;
                }
            }
            return bookingLength;
        }

        public static bool IsWeekend(int bookingLength, DateTime today)
        {
            DateTime endDate = today.AddDays(bookingLength - 1);
            for (DateTime date = today; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
