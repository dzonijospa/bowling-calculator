using BowlingCalculator.Domain.FrameStates;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingCalculator.Domain.Test
{
    public class GameShould
    {
        [Fact]
        public void CalculateRunningTotalWhenInstantiated()
        {
            byte score = 20;
            byte currentFrame = 1;
            var frames = new LinkedList<Frame>();
            frames.AddFirst(CreateFrameWithScore(currentFrame, score));
            var game = CreateGame(frames, currentFrame);

            Assert.Equal(game.RunningTotal, score);
        }

        //roll

        [Fact]
        public void ApplyRollToCurrentFrameWhenRoll()
        {
            byte currentFrame = 1;
            var frames = new LinkedList<Frame>();
            var frameState = new Moq.Mock<IFrameState>();
            frameState.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState.Object);
            var frame = new Frame(currentFrame, frameState.Object);

            frames.AddFirst(frame);
            var game = CreateGame(frames, currentFrame);
            byte pinsDowned = 5;

            game.Roll(pinsDowned);

            frameState.Verify(mock => mock.ApplyRoll(pinsDowned, game.MaxPins), Times.Once());
        }

        [Fact]
        public void ApplyRollToPreviousFramesIfScoreNotCalculatedWhenRoll()
        {
            byte currentFrame = 3;
            var frames = new LinkedList<Frame>();
            //first frame
            var frameState = new Moq.Mock<IFrameState>();
            frameState.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState.Object);
            var frame = new Frame(currentFrame, frameState.Object);
            frames.AddFirst(frame);
            //second frame without score
            var frameState2 = new Moq.Mock<IFrameState>();
            frameState2.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState2.Object);
            var frame2 = new Frame(2, frameState2.Object);
            frames.AddBefore(frames.First,frame2);
            //third frame without score
            var frameState3 = new Moq.Mock<IFrameState>();
            frameState3.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState3.Object);
            var frame3 = new Frame(1, frameState3.Object);
            frames.AddBefore(frames.First, frame3);

            var game = CreateGame(frames, currentFrame);
            byte pinsDowned = 5;

            game.Roll(pinsDowned);

            frameState2.Verify(mock => mock.ApplyRoll(pinsDowned, game.MaxPins), Times.Once());
            frameState3.Verify(mock => mock.ApplyRoll(pinsDowned, game.MaxPins), Times.Once());
        }

        [Fact]
        public void NotApplyRollToPreviousFramesIfScoreCalculatedWhenRoll()
        {
            byte currentFrame = 2;
            var frames = new LinkedList<Frame>();
            //first frame
            var frameState = new Moq.Mock<IFrameState>();
            frameState.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState.Object);
            var frame = new Frame(currentFrame, frameState.Object);
            frames.AddFirst(frame);
            //second frame with score
            var frameState2 = new Moq.Mock<IFrameState>();
            frameState2.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState2.Object);
            frameState2.Setup(x => x.FrameScore).Returns(5);
            var frame2 = new Frame(1, frameState2.Object);
            frames.AddBefore(frames.First, frame2);

            var game = CreateGame(frames, currentFrame);
            byte pinsDowned = 5;

            game.Roll(pinsDowned);

            frameState2.Verify(mock => mock.ApplyRoll(pinsDowned, game.MaxPins), Times.Never());
        }

        [Fact]
        public void CalculateNewRunningTotalCorrectlyWhenRoll()
        {
            byte score = 10;
            byte currentFrame = 1;
            var frames = new LinkedList<Frame>();
            var frameState1 = new Moq.Mock<IFrameState>();
            frameState1.Setup(x => x.FrameScore).Returns(() => null);
            var frameState2 = new Moq.Mock<IFrameState>();
            frameState2.Setup(x => x.FrameScore).Returns(score);
            frameState1.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState2.Object);
            var frame = new Frame(currentFrame, frameState1.Object);
            frames.AddFirst(frame);
            var game = CreateGame(frames, currentFrame);

            game.Roll(5);

            Assert.Equal(game.RunningTotal, score);
        }

        [Fact]
        public void SetCurrentFrameToNextWhenTransitionShouldHappen()
        {
            byte currentFrame = 1;
            byte nextFrame = 2;
            var frames = new LinkedList<Frame>();
            var frameState1 = new Moq.Mock<IFrameState>();
            frameState1.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState1.Object);
            frameState1.Setup(x => x.ShouldTransitionToNextFrame()).Returns(true);
            var frame = new Frame(currentFrame, frameState1.Object);
            frames.AddFirst(frame);
            var frameState2 = new Moq.Mock<IFrameState>();
            var frame2 = new Frame(nextFrame, frameState2.Object);
            frames.AddLast(frame2);
           
            var game = CreateGame(frames, currentFrame);
            game.Roll(5);

            Assert.Equal(game.CurrentFrameNumber, nextFrame);
        }

        [Fact]
        public void NotChangeCurrentFrameWhenLastFrame()
        {
            byte currentFrame = 1;
            var frames = new LinkedList<Frame>();
            var frameState1 = new Moq.Mock<IFrameState>();
            frameState1.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState1.Object);
            frameState1.Setup(x => x.ShouldTransitionToNextFrame()).Returns(true);
            var frame = new Frame(1, frameState1.Object);
            frames.AddFirst(frame);

            var game = CreateGame(frames, currentFrame);
            game.Roll(5);

            Assert.Equal(game.CurrentFrameNumber, currentFrame);
        }

        [Fact]
        public void ReturnGameFinishedWhenScoringCompletedInLastFrame()
        {
            byte currentFrame = 1;
            var frames = new LinkedList<Frame>();
            var frameState1 = new Moq.Mock<IFrameState>();
            var frameState2 = new Moq.Mock<IFrameState>();
            frameState2.Setup(x => x.FrameScore).Returns(5);
            frameState1.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(frameState2.Object);
            
            var frame = new Frame(1, frameState1.Object);
            frames.AddFirst(frame);

            var game = CreateGame(frames, currentFrame);
            game.Roll(5);

            Assert.True(game.IsGameFinished());
        }

        private static Game CreateGame(LinkedList<Frame> frames, byte currentFrame)
        {
            return new Game(100, frames, currentFrame, 10);
        }

        private static Frame CreateFrameWithScore(byte frameNumber,byte? score)
        {
            var frameState = new Moq.Mock<IFrameState>();
            frameState.Setup(x => x.FrameScore).Returns(score);
            return new Frame(frameNumber, frameState.Object);
        }

    }
}
