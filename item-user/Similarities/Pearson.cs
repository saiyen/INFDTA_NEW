using System;
using System.Collections.Generic;
using item_user.Model;
using System.Linq;

namespace item_user.Similarities
{
    class Pearson : IRecommender
    {
        public double CalculateSimilarity(List<UserPreference> User1, List<UserPreference> User2)
        {
            double SumOfUser1MultipliedByUser2 = 0;
            double SumOfUser1 = 0;
            double SumOfUser2 = 0;
            double SumOfUser1Square = 0;
            double SumOfUser2Square = 0;
            int ArticleCount = 0;

            foreach (UserPreference User1_Target in User1)
            {
                foreach (UserPreference User2_Article in User2.Where(x => x.Article == User1_Target.Article))
                {
                    SumOfUser1MultipliedByUser2 += User1_Target.Rating * User2_Article.Rating;
                    SumOfUser1 += User1_Target.Rating;
                    SumOfUser2 += User2_Article.Rating;
                    SumOfUser1Square += Math.Pow(User1_Target.Rating, 2);
                    SumOfUser2Square += Math.Pow(User2_Article.Rating, 2);
                    ArticleCount++;
                }
            }

            double Top = SumOfUser1MultipliedByUser2 - ((SumOfUser1 * SumOfUser2 / ArticleCount));
            double Bottom = Math.Sqrt((SumOfUser1Square - Math.Pow(SumOfUser1, 2) / ArticleCount) * (SumOfUser2Square - Math.Pow(SumOfUser2,2) / ArticleCount));

            double Result = Top / Bottom;
            return Result;
        }
    }
}
