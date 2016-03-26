using System;
using System.Collections.Generic;
using item_user.Model;
using System.Linq;

namespace item_user.Similarities
{
    class Euclidean : IRecommender
    {
        public double CalculateSimilarity(List<UserPreference> User1, List<UserPreference> User2)
        {
            double SumOfUser1MinusUser2 = 0;
            double SquareRootOfTotal = 0;

            foreach (UserPreference User1_Target in User1)
            {
                foreach (UserPreference User2_Article in User2.Where(x => x.Article == User1_Target.Article))
                {
                    SumOfUser1MinusUser2 += Math.Pow(User1_Target.Rating - User2_Article.Rating, 2);
                }
            }

            SquareRootOfTotal = Math.Sqrt(SumOfUser1MinusUser2);
            double Result = SquareRootOfTotal;
            return Result;
        }
    }
}
