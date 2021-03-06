using BowlingCalculator.Domain.Exceptions;
using BowlingCalculator.Domain.FrameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingCalculator.Domain.Test.FrameStates
{
    public class OpenFrameShould
    {
        #region firstThrow
        private readonly byte max_pin = 10; 
      
        [Fact]
        public void ApplyFirstRoll()
        {

            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            openFrame.ApplyRoll(firstRoll,max_pin);

            Assert.Equal(firstRoll, openFrame.FirstRoll.Value);
        }

        [Fact]
        public void StayInSameStateWhenFirstRollLessThenMax()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            IFrameState newFrameState = openFrame.ApplyRoll(firstRoll, max_pin);

            Assert.Same(openFrame, newFrameState);
        }

        [Fact]
        public void TransitionToStrikeWhenFirstRollEqualsMaxPin()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = max_pin;
            IFrameState newFrameState = openFrame.ApplyRoll(firstRoll, max_pin);

            Assert.IsType<Strike>(newFrameState);
        }

        [Fact]
        public void CalculateScoreAfterFirstRoll()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            openFrame.ApplyRoll(firstRoll, max_pin);

            Assert.Equal(openFrame.FrameScore.Value,firstRoll);
        }

        [Fact]
        public void ReturnScoringNotCompletedAfterFirstRoll()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            openFrame.ApplyRoll(firstRoll, max_pin);

            Assert.False(openFrame.IsScoringCompleted());
        }

        [Fact]
        public void NotTransitionToNextFrameAfterFirstRoll()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            openFrame.ApplyRoll(firstRoll, max_pin);

            Assert.False(openFrame.ShouldTransitionToNextFrame());
        }

        #endregion

        #region secondThrow

        [Fact]
        public void ApplySecondRoll()
        {

            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            byte secondRoll = 2;
            openFrame.ApplyRoll(firstRoll, max_pin);
            openFrame.ApplyRoll(secondRoll, max_pin);

            Assert.Equal(secondRoll, openFrame.SecondRoll.Value);
        }

        [Fact]
        public void StayInSameStateWhenFirstPlusSecondRollLessThenMax()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            byte secondRoll = 2;
            openFrame.ApplyRoll(firstRoll, max_pin);
            IFrameState newFrameState = openFrame.ApplyRoll(secondRoll, max_pin);

            Assert.Same(openFrame, newFrameState);
        }

        [Fact]
        public void TransitionToSpareWhenFirstPlusSecondRollEqualsMaxPin()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            byte secondRoll = (byte)(max_pin - firstRoll);
            openFrame.ApplyRoll(firstRoll, max_pin);
            IFrameState newFrameState = openFrame.ApplyRoll(secondRoll, max_pin);

            Assert.IsType<Spare>(newFrameState);
        }

        [Fact]
        public void CorrectlyCalculateScoreAfterSecondRoll()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            byte secondRoll = 2;
            openFrame.ApplyRoll(firstRoll, max_pin);
            openFrame.ApplyRoll(secondRoll, max_pin);

            Assert.Equal(openFrame.FrameScore.Value,firstRoll + secondRoll);
        }

        [Fact]
        public void TransitionToNextFrameAfterSecondRoll()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            byte secondRoll = 2;
            openFrame.ApplyRoll(firstRoll, max_pin);
            openFrame.ApplyRoll(secondRoll, max_pin);

            Assert.True(openFrame.ShouldTransitionToNextFrame());
        }

        [Fact]
        public void ReturnScoringCompletedAfterSecondRoll()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 1;
            byte secondRoll = 2;
            openFrame.ApplyRoll(firstRoll, max_pin);
            openFrame.ApplyRoll(secondRoll, max_pin);

            Assert.True(openFrame.IsScoringCompleted());
        }

        #endregion
       
        [Fact]
        public void ThrowInvalidNumberOfPinExceptionIfTotalMoreThanMax()
        {
            OpenFrame openFrame = CreateOpenFrame();

            byte firstRoll = 5;
            byte secondRoll = 6; //5+6 > max_pin
            openFrame.ApplyRoll(firstRoll, max_pin);
            

            Assert.Throws<InvalidNumberOfPinsException>(() =>openFrame.ApplyRoll(secondRoll, max_pin));
        }

        private OpenFrame CreateOpenFrame()
        {
            return new OpenFrame(null, null, null);
        }
    }
}
