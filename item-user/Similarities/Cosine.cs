using System;
using System.Collections.Generic;
using item_user.Model;
using System.Linq;

namespace item_user.Similarities
{
    class Cosine : IRecommender
    {
        public double CalculateSimilarity(List<UserPreference> User1, List<UserPreference> User2)
        {
            double SumOfUser1MultipliedByUser2 = 0;
            double SumOfUser1Pow2 = 0;
            double SumOfUser2Pow2 = 0;

            foreach (UserPreference User1_Target in User1)
            {
                foreach (UserPreference User2_Article in User2.Where(x => x.Article == User1_Target.Article))
                {
                    SumOfUser1MultipliedByUser2 += User1_Target.Rating * User2_Article.Rating;
                    SumOfUser1Pow2 += Math.Pow(User1_Target.Rating, 2);
                    SumOfUser2Pow2 += Math.Pow(User2_Article.Rating, 2);
                }
            }

            double Top = SumOfUser1MultipliedByUser2;
            double Bottom = Math.Sqrt((SumOfUser1Pow2)*(SumOfUser2Pow2));

            double Result = Top / Bottom;
            return Result;
        }
    }
}
