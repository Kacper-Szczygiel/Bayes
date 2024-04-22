using Bayes;

public class Program
{
    public static void Main(string[] args)
    {
        var trainSetName = ReadTrainingFileFromUser();
        var testSetName = ReadTestFileFromUser();

        List<Data> trainSet = FileToList(trainSetName);
        List<Data> testSet = FileToList(testSetName);

        Classifier classifier = new Classifier(trainSet);

        foreach (var data in testSet)
        {
            data.PrintAttributes();
            Console.WriteLine("->" + classifier.Classify(data));
        }
        
        string answer;
        do
        {
            Console.WriteLine("Would you like to add a new record? (yes/no)");
            answer = Console.ReadLine();
            if (answer == "no") { Environment.Exit(0); }
            Console.WriteLine("Enter record");
            String record = Console.ReadLine();
            Data testData = new Data(SplitLine(record));
            testData.PrintAttributes();
            Console.WriteLine("->" + classifier.Classify(testData));
        } while (answer == "yes");
    }
    
    private static string ReadTrainingFileFromUser()
    {
        Console.WriteLine("Enter file name for training set");
        var trainingFile = Console.ReadLine();
        try
        {
            if (!File.Exists(trainingFile))
            {
                throw new FileNotFoundException("File doesn't exist");
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }

        return trainingFile;
    }

    private static string ReadTestFileFromUser()
    {
        Console.WriteLine("Enter file name for test set");
        var testFile = Console.ReadLine();
        try
        {
            if (!File.Exists(testFile))
            {
                throw new FileNotFoundException("File doesn't exist");
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }

        return testFile;
    }
    
    private static List<string> SplitLine(string line)
    {
        List<string> returnList = new List<string>(line.Split(","));
        return returnList;
    }
    
    private static List<Data> FileToList(string fileName)
    {
        List<Data> returnList = new List<Data>();
        
        var lines = File.ReadLines(fileName);
        foreach (var line in lines)
        {
            List<string> attributes = SplitLine(line);
            Data data = new Data(attributes);
            returnList.Add(data);
        }

        return returnList;
    }
}