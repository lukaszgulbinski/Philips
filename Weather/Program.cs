namespace Weather
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Execute());
            Console.ReadKey();
        }

        //In weather.txt you’ll find daily weather data for Morristown, NJ for June 2002. Download this text
        //file, then write a program to output the day number (column one) with the smallest temperature
        //spread (the maximum temperature is the second column, the minimum the third column).

        public static string? Execute()
        {
            var days = File
                //TODO: Clarify with the PO, the expected behavior in case when weather.txt cannot be found or accessed by any reason.
                .ReadAllLines("weather.txt")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                //TODOL Clarify with PO, that column order should be used instead of colument headers.
                .Select(s => new
                {
                    Day = s.ElementAt(0),
                    //TODO: Clarify with the PO, that '*' should be ignored.
                    Max = s.ElementAt(1)?.Replace("*", ""),
                    Min = s.ElementAt(2)?.Replace("*", "")
                })
                .Where(s => s.Day.All(s => char.IsDigit(s)))
                .Select(s => new
                {
                    s.Day,
                    //TODO: Clarify with the PO, the expected behavior in case when Max/Min wont be a number.
                    //TODO: Clarify with the PO, that the Min/Max will always be intiger value - or clarify decimal format.
                    Spread = Convert.ToInt32(s.Max) - Convert.ToInt32(s.Min)
                })
                .GroupBy(s => s.Spread)
                .OrderBy(s => s.Key)
                .FirstOrDefault()
                ?.Select(s => s.Day)
                .ToArray()
                //TODO: Get confirmation from the PO that no output should be printed if no data is found in the content of the file.
                ?? new string[] {};

            //TODO: Get confirmation from the PO that multiple days should be printed if there is more then one day with the same smallest spread.
            return string.Join(';', days);
        }
    }
}