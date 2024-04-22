using System.Diagnostics.CodeAnalysis;

namespace Bayes;

public class Classifier
{
    public List<Data> TrainSet;
    public List<int> AttributesCounter;
    public List<string> DecisionList;

    public Classifier(List<Data> trainSet)
    {
        TrainSet = trainSet;
        AttributesCounter = AssignAttributesCounter(trainSet);
        DecisionList = AssignDecisionList(trainSet);
    }

    private List<int> AssignAttributesCounter(List<Data> input)
    {
        List<int> returnList = new List<int>();

        for (int i = 0; i < input.ElementAt(i).Attributes.Count - 1; i++)
        {
            List<string> attributesWithoutRepetition = new List<string>();

            for (int j = 0; j < input.Count; j++)
            {
                if (!attributesWithoutRepetition.Contains(input[j].Attributes[i]))
                {
                    attributesWithoutRepetition.Add(input[j].Attributes[i]);
                }
            }

            returnList.Add(attributesWithoutRepetition.Count);
        }

        return returnList;
    }

    private List<string> AssignDecisionList(List<Data> input)
    {
        List<string> decisionList = new List<string>();

        foreach (var data in input)
        {
            if (!decisionList.Contains(data.Attributes.Last()))
            {
                decisionList.Add(data.Attributes.Last());
            }
        }

        return decisionList;
    }

    private double CountAttributeWithDecision(string attribute, int index, string decision)
    {
        double counter = 0;

        for (int i = 0; i < TrainSet.Count; i++)
        {
            if (TrainSet.ElementAt(i).Attributes.ElementAt(index).Equals(attribute)
                && TrainSet.ElementAt(i).Attributes.Last().Equals(decision))
            {
                counter++;
            }
        }

        return counter;
    }
    
    private double CountAttribute(string attribute, int index)
    {
        double counter = 0;

        for (int i = 0; i < TrainSet.Count; i++)
        {
            if (TrainSet.ElementAt(i).Attributes.ElementAt(index).Equals(attribute))
            {
                counter++;
            }
        }

        return counter;
    }

    public string Classify(Data testData)
    {
        Dictionary<string, double> probabilityDict = new Dictionary<string, double>();
        foreach (var decision in DecisionList)
        {
            double probability = 1;

            for (int i = 0; i < testData.Attributes.Count; i++)
            {
                double numerator = CountAttributeWithDecision(testData.Attributes.ElementAt(i), i, decision);
                double denominator = CountAttribute(decision, TrainSet.ElementAt(0).Attributes.Count - 1);
                
                if (numerator == 0)
                {
                    numerator += 1;
                    denominator += AttributesCounter.ElementAt(i);
                }
                
                probability *= numerator / denominator;
            }

            probability *= CountAttribute(decision, TrainSet.ElementAt(0).Attributes.Count - 1) / TrainSet.Count;

            probabilityDict[decision] = probability;
        }
        
        double max = 0;
        string mostProbabilityDecision = "";

        foreach (var entry in probabilityDict)
        {
            if (entry.Value > max)
            {
                mostProbabilityDecision = entry.Key;
                max = entry.Value;
            }
        }

        return mostProbabilityDecision;
    }
}