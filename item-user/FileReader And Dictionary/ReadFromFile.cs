using item_user.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace item_user.FileReader
{
    class ReadFromFile
    {
        private string line;
        private char reggex = ' ';
        string project_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        StreamReader file;
        Dictionary<int, List<UserPreference>> DictionaryOfUsers = new Dictionary<int, List<UserPreference>>();

        public StreamReader ReadFile(int DataSet)
        {
            if (DataSet == 1)
            {
                reggex = ',';

                // Read the file and display it line by line.
                file = new StreamReader(project_path+"/Files/userItem.data");
            }
            if(DataSet == 2)
            {
                reggex = '\t';

                // Read the file and display it line by line.
                file = new StreamReader(project_path+"/Files/u100k.data");
            }
          
            while ((line = file.ReadLine()) != null)
            {
                var currentLine = line.Split(reggex);
                int userID = int.Parse(currentLine[0]);
                int article = int.Parse(currentLine[1]);
                double rating = double.Parse(currentLine[2], CultureInfo.InvariantCulture);
                List<UserPreference> listOfUserPreference;

                if (!DictionaryOfUsers.TryGetValue(userID, out listOfUserPreference))
                {
                    DictionaryOfUsers.Add(userID, new List<UserPreference>());
                }

                DictionaryOfUsers[userID].Add(new UserPreference { UserID = userID, Article = article, Rating = rating });

                //Console.WriteLine("User: " + userID + " Article: " + article + " Rating: " + rating);
            }
            file.Close();

            return file;
        }

        public Dictionary<int,List<UserPreference>> GetDictionary()
        {
            if(DictionaryOfUsers == null)
            {
                throw new Exception("DataSet is empty");
            }

                return DictionaryOfUsers;
        }
    }
}