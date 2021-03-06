using BowlingCalculator.Domain.FrameStates;
using System;
using System.Collections.Generic;

namespace BowlingCalculator.Domain.Services
{
    public class GameBuilder
    {
        public Game CreateDefaultGame(byte maxPins,byte numberOfFrames)
        {

            byte currentFrame = 1;

            var frames = new LinkedList<Frame>();

            var firstFrame = CreateDefaultFrame(frameNumber: currentFrame);

            frames.AddFirst(firstFrame);

            for (byte i = 2; i <= numberOfFrames; i++)
            {
                frames.AddLast(CreateDefaultFrame(i));
            }

            return new Game(Guid.NewGuid(), frames, currentFrame, maxPins, GameStatus.NotStarted, default(short));
        }

        private Frame CreateDefaultFrame(byte frameNumber)
        {
            return new Frame(frameNumber, CreateDefaultOpenFrame());
        }

        private OpenFrame CreateDefaultOpenFrame()
        {
            return new OpenFrame(frameScore: null, firstRoll: null, secondRoll: null);
        }
    }
}
