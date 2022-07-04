namespace Soccer
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Execute());
            Console.ReadKey();
        }

        //The file football.txt contains the results from the English Premier League for 2001/2. The columns
        //labeled ‘F’ and ‘A’ contain the total number of goals scored for and against each team in that season
        //(so Arsenal scored 79 goals against opponents and had 36 goals scored against them). Write a
        //program to print the name of the team with the smallest difference in ‘for’ and ‘against’ goals.

        public static string Execute()
        {
            //TODO: Clarify with the PO, the expected behavior in case when soccer.txt cannot be found or accessed by any reason.
            var lines = File.ReadAllLines("soccer.txt");
            //TODO: Clarify with the PO, the expected behavior in case when not column header as specified will be found in the file.
            var header = new
            {
                Team = lines[0].IndexOf("Team"),
                F = lines[0].IndexOf("F"),
                A = lines[0].IndexOf("A")
            };
            var teams = lines
                .Select(s => new
                {
                    Team = s.GetField(header.Team),
                    F = s.GetField(header.F),
                    A = s.GetField(header.A),
                })
                .Where(s => s.Team != "Team")
                .Where(s => !s.Team.All(s => s == '-'))
                //TODO: Clarify with the PO, the expected behavior in case when F or A field is not a number.
                .Select(s => new
                {
                    s.Team,
                    F = Convert.ToInt32(s.F),
                    A = Convert.ToInt32(s.A),
                })
                .Select(s => new
                {
                    Team = s.Team,
                    Difference = Math.Abs(s.F - s.A)
                })
                .GroupBy(s => s.Difference)
                .OrderBy(s => s.Key)
                .FirstOrDefault()
                ?.Select(s => s.Team)
                .ToArray()
                //TODO: Get confirmation from the PO that no output should be printed if no team data is found in the content of the file.
                ?? Array.Empty<string>();

            //TODO: Get confirmation from the PO that multiple teams should be printed if there is more then one team with the same smallest difference.
            return string.Join(';', teams);
        }

        private static string GetField(this string line, int headerIndex)
        {
            //TODO: Clarify with the PO, the expected behavior in case when data will be found in the line as header indicating.
            return new string(line.Substring(headerIndex)
                .TakeWhile(s => !char.IsWhiteSpace(s))
                .ToArray());
        }
    }
}