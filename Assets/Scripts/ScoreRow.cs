using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ScoreRow
    {
        public ScoreRow()
        {

        }

        public ScoreRow(string playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
            PlayTime = DateTime.Now;
        }
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public int Score { get; set; }

        public DateTime PlayTime { get; set; }
    }
}
