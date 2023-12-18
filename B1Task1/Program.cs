namespace B1Task1
{
    internal class Program
    {

        private static string folderPath = "D:\\education\\.net\\B1Task1\\GeneratedFiles";
        private static string connectionString = "server=.; database=B1Database; Integrated Security=true";
        private static string mergedFileName = "merged_file.txt";

        enum Command
        {
            Generate,
            Merge,
            Populate,
            Sum,
            Median,
            Exit,
            Unknown
        }

        static Command ParseCommand(string input)
        {
            if (Enum.TryParse(input, true, out Command command))
            {
                return command;
            }
            return Command.Unknown;
        }

        static void Main(string[] args)
        {
            var dbCaller = new DatabaseProcedureCaller(connectionString);
            while (true)
            {
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                var splitedInput = input.Split(' ');
                var command = ParseCommand(splitedInput[0]);

                switch (command)
                {
                    case Command.Generate:
                        FileGenerator.GenerateFiles(folderPath);
                        break;
                    case Command.Merge:
                        var toDelete = (splitedInput.Length > 1) ? splitedInput[1] : "";
                        FileMerger.MergeFiles(folderPath, mergedFileName, toDelete);
                        break;
                    case Command.Populate:
                        var dbLoader = new DatabaseLoader(connectionString);
                        dbLoader.LoadDataToDbFromFile(mergedFileName);
                        break;
                    case Command.Sum:
                        dbCaller.GetTotalSum();
                        break;
                    case Command.Median:
                        dbCaller.CalculateMedian();
                        break;
                    case Command.Exit:
                        return;
                    case Command.Unknown:
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }
    }
    
}