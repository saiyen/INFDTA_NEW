using item_user.Model;
using item_user.Similarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace item_user.Classes
{
    class PredictRatings : Dictionary<int, List<UserPreference>>
    {
        IRecommender Pearson = new Pearson();
        List<KeyValuePair<int, double >> pearsonNeighbours = new List<KeyValuePair<int, double>>();

        public int HighestRecommendations { get; set; }

        public void PredictTheRating(Dictionary<int, List<UserPreference>> DataSet, int TargetUser, int AmountOfNeighbours)
        {

        }
    }
}
