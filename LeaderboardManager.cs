using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GameProject
{
    internal class LeaderboardManager
    {
        private string filename = "leaderboards.txt";
        public string username { get; set; }
        public int score { get; set; }
        public string savedTime { get; set; }

        public void SetRecord(string Username, int Score, string recordedTime)
        {
            username = Username;
            score = Score;
            savedTime = recordedTime;
            SaveRecord();
        }

        public void SaveRecord()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new NoUsernameException();
                }

                List<(string Username, int Score, string Time)> leaderboard = new List<(string, int, string)>();

                if (File.Exists(filename))
                {
                    var lines = File.ReadAllLines(filename);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(';');
                        if (parts.Length == 3 && int.TryParse(parts[1], out int parsedScore))
                        {
                            leaderboard.Add((parts[0], parsedScore, parts[2]));
                        }
                    }
                }

                leaderboard.Add((username, score, savedTime));

                leaderboard = leaderboard
                    .OrderByDescending(e => e.Score)
                    .Take(10)
                    .ToList();

                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var entry in leaderboard)
                    {
                        sw.WriteLine($"{entry.Username};{entry.Score};{entry.Time}");
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show($"Error saving leaderboard: {e.Message}");
            }
            catch (NoUsernameException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unexpected Error: {e.Message}");
            }
        }
        public string[] GetTopTenEntries()
        {
            if (!File.Exists(filename))
                return new string[0];

            string[] lines = File.ReadAllLines(filename);

            return lines;
        }

        public class NoUsernameException : Exception
        {
            public NoUsernameException() : base("There's no username saved.") { }
        }
    }
}
