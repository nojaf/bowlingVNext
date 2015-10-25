using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BowlingVNext.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BowlingGameTests
    {
        readonly BowlingGame _game;

        public BowlingGameTests()
        {
            _game = new BowlingGame();
        }


        [Fact]
        public void given_all_rolls_are_zero_total_score_should_be_zero()
        {
            RollPinsTimes(0, 20);
            Assert.Equal(0, _game.Score());
        }

        private void RollPinsTimes(int pins, int rolls)
        {
            for (int i = 0; i < rolls; i++)
            {
                _game.Roll(pins);
            }
        }

        [Fact]
        public void given_one_pin_in_each_roll_for_all_rolls_should_return_a_score_of_twenty()
        {
            RollPinsTimes(1, 20);
            Assert.Equal(20, _game.Score());
        }

        [Fact]
        public void given_one_strike_should_add_next_two_rolls()
        {
            _game.Roll(10);
            _game.Roll(2);
            _game.Roll(3);
           RollPinsTimes(0,17);
            Assert.Equal(20, _game.Score());
        }

        [Fact]
        public void given_a_perfect_game_should_score_300()
        {
            RollPinsTimes(10,12);
            Assert.Equal(300, _game.Score());
        }

        [Fact]
        public void given_one_spare_should_score_point_from_next_roll()
        {
            _game.Roll(5);
            _game.Roll(5);
            _game.Roll(1);
            RollPinsTimes(0,17);
            Assert.Equal(12, _game.Score());
        }

        [Fact]
        public void given_full_game_should_return_correct_score()
        {
            // https://www.bowlingindex.com/instruction/scoring.htm
            // Example one
            _game.Roll(9);
            _game.Roll(0);

            _game.Roll(3);
            _game.Roll(5);

            _game.Roll(6);
            _game.Roll(1);

            _game.Roll(3);
            _game.Roll(6);

            _game.Roll(8);
            _game.Roll(1);

            _game.Roll(5);
            _game.Roll(3);

            _game.Roll(2);
            _game.Roll(5);

            _game.Roll(8);
            _game.Roll(0);

            _game.Roll(7);
            _game.Roll(1);

            _game.Roll(8);
            _game.Roll(1);

            Assert.Equal(82, _game.Score());
        }

        [Fact]
        public void given_full_game_with_spare_should_return_correct_score()
        {
            //Example 2
            // 9-0  
            _game.Roll(9);
            _game.Roll(0);

            // 3 /
            _game.Roll(3);
            _game.Roll(7);

            // 6 -1
            _game.Roll(6);
            _game.Roll(1);

            // 3 /  
            _game.Roll(3);
            _game.Roll(7);

            // 8-1
            _game.Roll(8);
            _game.Roll(1);

            // 5 /   
            _game.Roll(5);
            _game.Roll(5);

            // 0 /
            _game.Roll(0);
            _game.Roll(10);

            // 8 -0  
            _game.Roll(8);
            _game.Roll(0);

            // 7 /    
            _game.Roll(7);
            _game.Roll(3);

            // 8 /
            _game.Roll(8);
            _game.Roll(2);

            // 8
            _game.Roll(8);

            Assert.Equal(131, _game.Score());
        }

        [Fact]
        public void given_full_game_with_strike_should_return_correct_score()
        {
            //  X   
             _game.Roll(10);

            // 3 /
            _game.Roll(3);
            _game.Roll(7);

            // 6 -1
            _game.Roll(6);
            _game.Roll(1);

            // X
            _game.Roll(10);

            // X
            _game.Roll(10);

            // X
            _game.Roll(10);

            // 2 /
            _game.Roll(2);
            _game.Roll(8);

            // 9 -0
            _game.Roll(9);
            _game.Roll(0);

            // 7 /
            _game.Roll(7);
            _game.Roll(3);

            //XXX
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);

            Assert.Equal(193, _game.Score());

        }

        [Fact]
        public void given_spare_with_first_roll_zero_should_not_be_considered_to_be_a_strike()
        {
            _game.Roll(0);
            _game.Roll(10);
            _game.Roll(1);
            _game.Roll(1);
            RollPinsTimes(0,16);

            Assert.Equal(13,_game.Score());
        }
    }
}
