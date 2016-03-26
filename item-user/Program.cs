using item_user.Classes;
using item_user.FileReader;
using item_user.Model;
using item_user.Similarities;
using System;
using System.Collections.Generic;

namespace user_item
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFromFile FileReader = new ReadFromFile();
            ChooseSimilarityStrategy(DataSetToDictionary(FileReader));
        }

        private static Dictionary<int, List<UserPreference>> DataSetToDictionary(ReadFromFile readFile)
        {
            Dictionary<int, List<UserPreference>> DictionaryOfUsers;

            Console.WriteLine("Please choose a dataset to set them in a Dictionary:\n" + "\nOption 1: Small DataSet" + "\nOption 2: Big DataSet");
            int userInput = int.Parse(Console.ReadLine());

            if (userInput == 1)
            {
                readFile.ReadFile(1);
                DictionaryOfUsers = readFile.GetDictionary();
            }
            else
            {
                readFile.ReadFile(2);
                DictionaryOfUsers = readFile.GetDictionary();
            }
                return DictionaryOfUsers;
        }

        private static void ChooseSimilarityStrategy(Dictionary<int, List<UserPreference>> UsersDictionary)
        {
            Console.Clear();
            Console.WriteLine("Please choose a Strategy:\n" + "\nOption 1: Similarities" + "\nOption 2: Nearest Neighbours\n" + "Option 3: Predict Ratings\n");
            int userInput = int.Parse(Console.ReadLine());

            switch (userInput)
            {
                case 1:

                    IRecommender PearsonStrategy = new Pearson();
                    IRecommender EuclideanStrategy = new Euclidean();
                    IRecommender CosineStrategy = new Cosine();

                    Console.Clear();
                    Console.WriteLine("Please choose a Similarity Strategy:\n" + "\nOption 1: Pearson" + "\nOption 2: Euclidean\n" + "Option 3: Cosine\n");
                    int userInput2 = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select a User:");
                    int User1_ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select the second User:");
                    int User2_ID = int.Parse(Console.ReadLine());

                    var User1 = UsersDictionary[User1_ID];
                    var User2 = UsersDictionary[User2_ID];

                    if(userInput2 == 1)
                    {
                        Console.WriteLine("The similarity between user {0} and user {1} = {2}", User1_ID, User2_ID, PearsonStrategy.CalculateSimilarity(User1, User2));
                    }
                    else if(userInput2 == 2){
                        Console.WriteLine("The similarity between user {0} and user {1} = {2}", User1_ID, User2_ID, EuclideanStrategy.CalculateSimilarity(User1, User2));
                    }
                    else
                    {
                        Console.WriteLine("The similarity between user {0} and user {1} = {2}", User1_ID, User2_ID, CosineStrategy.CalculateSimilarity(User1, User2));
                    }
                    Console.ReadLine();

                    break;

                case 2:
                    NearestNeighbours neighbours = new NearestNeighbours();

                    Console.Clear();
                    Console.WriteLine("Please select the Target User:");
                    int TargetUser = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select the amount of neighbours:");
                    int Neighbours = int.Parse(Console.ReadLine());

                    neighbours.getNearestNeighbours(UsersDictionary, TargetUser, Neighbours);

                    break;

                case 3:

                    Console.Clear();
                    Console.WriteLine("Please select the Target User");
                    int TargetUserPredictingRatings = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select the amount of neighbours");
                    int AmountOfNeighbours = int.Parse(Console.ReadLine());

                    Console.WriteLine("Select the range of highest recommendations");

                    int topRecommendations = int.Parse(Console.ReadLine());

                    var ratingsPredictor = new PredictRatings();
                    ratingsPredictor.HighestRecommendations = topRecommendations;
                    ratingsPredictor.PredictTheRating(UsersDictionary, TargetUserPredictingRatings, AmountOfNeighbours);

                    break;
            }
        }
    }
}
