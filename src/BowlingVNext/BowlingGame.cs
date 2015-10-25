using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingVNext
{
    public class BowlingGame
    {
        private const int MaxScore = 10;
        private readonly List<int> _rolls;

        public BowlingGame()
        {
            _rolls = new List<int>(22);
        }

        public void Roll(int pins)
        {
            _rolls.Add(pins);

            if (pins == MaxScore && IsFirstRoll())
            {
                _rolls.Add(0);
            }
        }

        private bool IsFirstRoll()
        {
            return _rolls.Count % 2 == 1;
        }

        public int Score()
        {
            int score = 0;
            for (int frame = 1; frame < 11; frame++)
            {
                score += GetScoreForFrame(frame);
            }

            return score;
        }

        private int GetScoreForFrame(int frame)
        {
            int pinsFirstRoll = GetPinsForFirstRoll(frame);
            int pinsSecondRoll = GetPinsForSecondRoll(frame);

            if (IsStrike(pinsFirstRoll))
            {
                return GetStrikeScore(frame);
            }

            if (IsSpare(pinsFirstRoll, pinsSecondRoll))
            {
                return GetSpareScore(frame);
            }

            return pinsFirstRoll + pinsSecondRoll;
        }

        private bool IsSpare(int pinsFirstRoll, int pinsSecondRoll)
        {
            return (pinsFirstRoll + pinsSecondRoll) == MaxScore;
        }

        private int GetSpareScore(int frame)
        {
            return GetPinsForFirstRoll(frame) + GetPinsForSecondRoll(frame) +
                   GetPinsForFirstRoll(frame + 1);
        }

        private int GetStrikeScore(int frame)
        {
            int pinsFirstRollNextFrame = GetPinsForFirstRoll(frame + 1);
            int pinsSecondRollNextFrame = GetPinsForFirstRoll(frame + 2);

            if (IsStrike(pinsFirstRollNextFrame))
            {
                return MaxScore + MaxScore + pinsSecondRollNextFrame;
            }
            return MaxScore + pinsFirstRollNextFrame + GetPinsForSecondRoll(frame + 1);
        }

        private bool IsStrike(int pinsFirstRoll)
        {
            return (pinsFirstRoll == MaxScore);
        }

        private int GetPinsForFirstRoll(int frame)
        {
            return _rolls[(frame - 1) * 2];
        }

        private int GetPinsForSecondRoll(int frame)
        {
            return _rolls[(frame * 2) - 1];
        }
    }
}
