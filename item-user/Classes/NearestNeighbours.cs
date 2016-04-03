using item_user.Model;
using item_user.Similarities;
using System.Collections.Generic;
using System.Linq;
using System;
using item_user.FileReader;

namespace item_user.Classes
{
    class NearestNeighbours
    {
        static Pearson calculatePearson = new Pearson();
        static Euclidean calculateEuclidean = new Euclidean();
        static Cosine calculateCosine = new Cosine();

        static List<KeyValuePair<double, int>> Neighbours = new List<KeyValuePair<double, int>>();

        const double similarityTreshold = 0.35;

        public static List<KeyValuePair<double, int>> getCoefficient(Dictionary<int, List<UserPreference>> dataset, int targetUserID, int maximumNeighbours, bool predictRating)
        {
            var targetUser = dataset[targetUserID];

            double pearsonSimilarity = 0;
            double euclideanSimilarity = 0;
            double cosineSimilarity = 0;

            Console.Clear();
            Console.WriteLine("Please apply a NearestNeighbours to a Similarity:\n" + "\nOption 1: Euclidean" + "\nOption 2: Cosine\n" + "Option 3: Pearson\n");
            int userInput = int.Parse(Console.ReadLine());

            switch (userInput)
            {
                case 1:

                    foreach (var secondUser in dataset.Where(x => x.Key != targetUserID))
                    {
                        euclideanSimilarity = calculateEuclidean.CalculateSimilarity(targetUser, secondUser.Value);
                        if (!predictRating)
                        {
                            Console.WriteLine("User:" + targetUserID + " against User: " + secondUser.Key + " has a similarity of: " + euclideanSimilarity);
                        }
                        else
                        {
                            getNearestNeighbours(euclideanSimilarity, secondUser.Key, maximumNeighbours);
                        }                   
                    }

                    break;
                case 2:

                    foreach (var secondUser in dataset.Where(x => x.Key != targetUserID))
                    {
                        cosineSimilarity = calculateCosine.CalculateSimilarity(targetUser, secondUser.Value);
                        if (!predictRating)
                        {
                            Console.WriteLine("User:" + targetUserID + " against User: " + secondUser.Key + " has a similarity of: " + cosineSimilarity);
                        }
                        else
                        {
                            getNearestNeighbours(cosineSimilarity, secondUser.Key, maximumNeighbours);
                        }
                    }

                    break;
                case 3:

                    foreach (var secondUser in dataset.Where(x => x.Key != targetUserID))
                    {
                        pearsonSimilarity = calculatePearson.CalculateSimilarity(targetUser, secondUser.Value);
                        if (!predictRating)
                        {
                            Console.WriteLine("User:" + targetUserID + " against User: " + secondUser.Key + " has a similarity of: " + pearsonSimilarity);
                        }
                        else
                        {
                            getNearestNeighbours(pearsonSimilarity, secondUser.Key, maximumNeighbours);
                        }
                    }

                    break;
            }

            return Neighbours;
        }

        static public void getNearestNeighbours(double Similarity, int secondUser, int maxNeighbours)
        {
            if (Similarity >= similarityTreshold)
            {
                if (Neighbours.Count < maxNeighbours)
                {
                    Neighbours.Add(new KeyValuePair<double, int>(Similarity, secondUser));
                }
                else
                {
                    var lowestSimilarityPair = Neighbours.OrderByDescending(x => x.Key).Last();
                    if (Similarity > lowestSimilarityPair.Key)
                    {
                        Neighbours.Remove(lowestSimilarityPair);
                        Neighbours.Add(new KeyValuePair<double, int>(Similarity, secondUser));
                    }
                }
            }
        }
    }
}
