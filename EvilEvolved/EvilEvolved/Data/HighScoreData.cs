using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EvilutionClass
{
    public class HighScoreData
    {
        public string[] PlayerName;
        public float[] Score;

        public int Count;

        public HighScoreData(int count)
        {
            PlayerName = new string[count];
            Score = new float[count];

            Count = count;
        }

        public static void SaveHighScores(HighScoreData data, string filename)
        {
            // Open the file, creating it if necessary
            Task.Run(() =>
            {
                FileStream stream = File.Open(filename, FileMode.OpenOrCreate);
                try
                {
                    // Convert the object to XML data and put it in the stream
                    XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                    serializer.Serialize(stream, data);
                }
                finally
                {
                    // Close the file
                    stream.Dispose();
                }
            }
            );
        }

        //public static HighScoreData LoadHighScores(string filename)
        //{
        //    HighScoreData data;

        //    // Get the path of the save game
        //    string fullpath = Path.Combine(@"Assets", filename);

        //    // Open the file
        //    FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate,
        //    FileAccess.Read);
        //    try
        //    {

        //        // Read the data from the file
        //        XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
        //        data = (HighScoreData)serializer.Deserialize(stream);
        //    }
        //    finally
        //    {
        //        // Close the file
        //        stream.Dispose();
        //    }

        //    return (data);
        //}

        //private void SaveHighScore(string HighScoresFilename, float score)
        //{
        //    // Create the data to save
        //    HighScoreData data = LoadHighScores(HighScoresFilename);

        //    int scoreIndex = -1;
        //    for (int i = 0; i < data.Count; i++)
        //    {
        //        if (score > data.Score[i])
        //        {
        //            scoreIndex = i;
        //            break;
        //        }
        //    }

        //    if (scoreIndex > -1)
        //    {
        //        //New high score found ... do swaps
        //        for (int i = data.Count - 1; i > scoreIndex; i--)
        //        {
        //            data.PlayerName[i] = data.PlayerName[i - 1];
        //            data.Score[i] = data.Score[i - 1];
        //        }

        //        data.PlayerName[scoreIndex] = "Player1"; //Retrieve User Name Here
        //        data.Score[scoreIndex] = score;

        //        SaveHighScores(data, HighScoresFilename);
        //    }
        //}

    }


}
