using item_user.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace item_user.Similarities
{
    interface IRecommender
    {
        double CalculateSimilarity(List<UserPreference> User1, List<UserPreference> User2);
    }
}
