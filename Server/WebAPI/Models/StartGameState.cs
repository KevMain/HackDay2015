using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class StartGameState
    {
        public string opponentName { get; set; }
        public int pointsToWin { get; set; }
        public int maxRounds { get; set; }
        public int dynamiteCount { get; set; }
    }
}