using item_user.Model;
using item_user.Similarities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace item_user.Classes
{
    internal class RatingsPredictor
    {
        List<KeyValuePair<double, int>> Neighbours;
        public int topOfRecommendations { get; set; }

        public void predictRating(Dictionary<int, List<UserPreference>> DictionaryOfUsers, int targetUserId, int maxNeighbours)
        {
            Neighbours = NearestNeighbours.getCoefficient(DictionaryOfUsers, targetUserId, maxNeighbours, true);

            var targetOfUserArticles = DictionaryOfUsers[targetUserId];
            var unratedArticlesOfTargetUser = DictionaryOfUsers.SelectMany(x => x.Value).Select(x => x.Article).Distinct().Where(x => !targetOfUserArticles.Any(y => y.Article == x));
            List<KeyValuePair<int, double>> recommendations = new List<KeyValuePair<int, double>>();
            foreach (var article in unratedArticlesOfTargetUser)
            {
                double multiplyRatingWithCoefficient = 0;
                double sumOfCoefficient = 0;

                var ratingsNeighboursOfArticle = DictionaryOfUsers.Where(x => Neighbours.Any(y => y.Value == x.Key)).SelectMany(x => x.Value).Where(x => x.Article == article);
                if (ratingsNeighboursOfArticle.Count() > 3)
                {
                    foreach (var item in ratingsNeighboursOfArticle)
                    {
                        var neighbour = Neighbours.FirstOrDefault(x => x.Value == item.UserID);
                        multiplyRatingWithCoefficient += neighbour.Key * item.Rating;
                        sumOfCoefficient += neighbour.Key;
                    }

                    double result = multiplyRatingWithCoefficient / sumOfCoefficient;

                    if (!Double.IsNaN(result))
                    {
                        recommendations.Add(new KeyValuePair<int, double>(article, result));
                        //Console.WriteLine("Predicted ratings for user {0}, article {1} : {2} ", targetUserId, article, result);
                    }
                }

            }
            foreach (var recommendation in recommendations.OrderByDescending(x => x.Value).Take(topOfRecommendations == 0 ? recommendations.Count() : topOfRecommendations))
            {
                Console.WriteLine("The predicted ratings for User: {0} of the Article: {1} is: {2}", targetUserId, recommendation.Key, recommendation.Value);
            }
        }
    }
}
