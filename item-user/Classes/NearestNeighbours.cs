using item_user.Model;
using item_user.Similarities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace item_user.Classes
{
    class NearestNeighbours
    {
        IRecommender calculateWithPearson = new Pearson();

        List<KeyValuePair<int, double>> ListOfNeighbours_PearsonStrategy = new List<KeyValuePair<int, double>>();

        private const float SimilarityTreshold = 0.35f;

        public void getNearestNeighbours(Dictionary<int, List<UserPreference>> UsersDictionary, int TargetUser, int AmountOfNeighbours)
        {
            var TargetUserID = UsersDictionary[TargetUser];

            foreach(var OtherUser in UsersDictionary.Where(x => x.Key != TargetUser))
            {
                double SimilarityOfPearson = calculateWithPearson.CalculateSimilarity(TargetUserID, OtherUser.Value);

                CheckNeighbour(ListOfNeighbours_PearsonStrategy, OtherUser.Key, AmountOfNeighbours, SimilarityOfPearson);
            }

            Console.WriteLine("With Pearson Strategy, the Target User {0} has this amount of Nearest Neighbours {1}:\n", TargetUser, AmountOfNeighbours);
            foreach (var similar in ListOfNeighbours_PearsonStrategy)
            {
                Console.WriteLine("The user {0} has the similarity of {1}", similar.Key, similar.Value);
            }
            Console.ReadLine();
        }

        private void CheckNeighbour(List<KeyValuePair<int, double>> NeighboursList, int OtherUser, int AmountOfNeighbours, double SimilarityBetweenUser)
        {
            if(SimilarityBetweenUser >= SimilarityTreshold)
            {
                if(NeighboursList.Count < AmountOfNeighbours)
                {
                    NeighboursList.Add(new KeyValuePair<int, double>(OtherUser, SimilarityBetweenUser));
                }
                else
                {
                    var NotEnoughOfSimilarity = NeighboursList.OrderByDescending(x => x.Value).Last();
                    if (SimilarityBetweenUser > NotEnoughOfSimilarity.Value)
                    {
                        NeighboursList.Remove(NotEnoughOfSimilarity);
                        NeighboursList.Add(new KeyValuePair<int, double>(OtherUser, SimilarityBetweenUser));
                    }
                }
            }
        }
    }
}
