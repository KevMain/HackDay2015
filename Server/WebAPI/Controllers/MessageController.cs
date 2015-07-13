using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using API.Models;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    public class MessageController : ApiController
    {
        private static string _oppenentName;
        private static int _pointsToWin;
        private static int _maxRounds;
        private static int _dynamiteCount;
        private static int _dynamiteUsed;
        private static string LastOurMove;
        private static string LastTheirMove;
        private static int _pointsTotal;
        private static int _oppPointsTotal;
        private static int _drawCount;
        private static int _roundsPlayed;
        private static int _waterbombs;
        
        private static readonly Dictionary<int, string> Messages = new Dictionary<int, string>
        {
            {1, "ROCK"},
            {2, "PAPER"},
            {3, "SCISSORS"},
            {4, "WATERBOMB"},
            {5, "DYNAMITE"},
        };

        [HttpGet]
        [Route("Move")]
        public HttpResponseMessage Get()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            var rnd = new Random();

            if (LastOurMove != null && LastOurMove == LastTheirMove && _dynamiteUsed <= _dynamiteCount)
            {
                if (_oppenentName == "FATBOTSLIM")
                {
                    if (LastOurMove == Messages[5])
                    {
                        LastOurMove = Messages[4];
                        _waterbombs++;
                    }
                    else
                    {
                        LastOurMove = Messages[5];
                        _dynamiteUsed++;
                    }

                }
                else
                {
                    LastOurMove = Messages[5];
                    _dynamiteUsed++;

                }
            }
            else
            {
                if (_dynamiteUsed <= _dynamiteCount)
                {
                    LastOurMove = Messages[rnd.Next(1, 5)];

                    if (LastOurMove == Messages[4])
                    {
                        LastOurMove = Messages[rnd.Next(1, 5)];
                    }
                    if (LastOurMove == Messages[4])
                    {
                        LastOurMove = Messages[rnd.Next(1, 5)];
                    }

                    if (LastOurMove == Messages[5])
                        _dynamiteUsed++;

                    if (LastOurMove == Messages[4])
                        _waterbombs++;
                }
                else
                    LastOurMove = Messages[rnd.Next(1, 4)];

            }

            resp.Content = new StringContent(LastOurMove, Encoding.UTF8, "text/plain");
            return resp;
        }

        [HttpPost]
        [Route("Move")]
        public HttpResponseMessage PostOppMove(LastOpponentMove lastOpponentMove)
        {
            _roundsPlayed++;

            LastTheirMove = lastOpponentMove.lastOpponentMove;

            string result = "LOST";

            if (LastOurMove != LastTheirMove)
            {
                // ROCK
                if (LastOurMove == Messages[1])
                {
                    if (LastTheirMove == Messages[2] ||
                        LastTheirMove == Messages[3] ||
                        LastTheirMove == Messages[4])
                    {
                        _pointsTotal++;
                        result = "WIN";
                    }
                    else
                    {
                        _oppPointsTotal++;
                    }
                }
                // PAPER
                else if (LastOurMove == Messages[2])
                {
                    if (LastTheirMove == Messages[1] ||
                        LastTheirMove == Messages[4])
                    {
                        _pointsTotal++;
                        result = "WIN";
                    }
                    else
                    {
                        _oppPointsTotal++;
                    }
                }
                // SCISSORS
                else if (LastOurMove == Messages[3])
                {
                    if (LastTheirMove == Messages[2] ||
                        LastTheirMove == Messages[4])
                    {
                        _pointsTotal++;
                        result = "WIN";
                    }
                    else
                    {
                        _oppPointsTotal++;
                    }
                }
                // WATER
                else if (LastOurMove == Messages[4])
                {
                    if (LastTheirMove == Messages[5])
                    {
                        _pointsTotal++;
                        result = "WIN";
                    }
                    else
                    {
                        _oppPointsTotal++;
                    }
                }
                // DYNAMITE
                else if (LastOurMove == Messages[5])
                {
                    if (LastTheirMove != Messages[4])
                    {
                        _pointsTotal++;
                        result = "WIN";
                    }
                    else
                    {
                        _oppPointsTotal++;
                    }
                }
            }
            else
            {
                _drawCount++;
                result = "DRAW";
            }

            string filePath = "C:\\Temp\\" + _oppenentName + ".txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                string line = String.Format("{0}.  Their move {1}, Our move {2}, Used Dynamite {3}, Used Waterbombs {4}, Our Points {5}, Opp Points {6}, Draw Count {7} RESULT {8}",
                    _roundsPlayed, lastOpponentMove.lastOpponentMove, LastOurMove, _dynamiteUsed, _waterbombs, _pointsTotal, _oppPointsTotal, _drawCount, result);

                file.WriteLine(line);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("Start")]
        public HttpResponseMessage Post(StartGameState gameState)
        {
            _roundsPlayed = 0;
            _oppenentName = gameState.opponentName;
            _pointsToWin = gameState.pointsToWin;
            _maxRounds = gameState.maxRounds;
            _dynamiteCount = gameState.dynamiteCount;
            _dynamiteUsed = 0;
            _drawCount = 0;
            _waterbombs = 0;
            
            string filePath = "C:\\Temp\\" + _oppenentName + ".txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                string line = "----------------------------------------------------------------------------------";
                string line2 = String.Format("OppName {0} and PointsToWin {1} and MaxRounds {2} and DynamiteCount {3}", _oppenentName, _pointsToWin, _maxRounds, _dynamiteCount);
                file.WriteLine(line);
                file.WriteLine(line2);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
