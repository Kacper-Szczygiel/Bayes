namespace Bayes;

public class Data
{
    public List<string> Attributes;
    public Data(List<string> attributes)
    {
        Attributes = attributes;
    }

    public void PrintAttributes()
    {
        foreach (var element in Attributes)
        {
            Console.Write(element + "; ");
        }
    }
}