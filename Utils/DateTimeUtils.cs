namespace InventoryViewer.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetRandomDate()
        {
            Random random = new Random();
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
