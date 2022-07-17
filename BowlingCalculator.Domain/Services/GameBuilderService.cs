using BowlingCalculator.Domain.FrameStates;
using System.Collections.Generic;

namespace BowlingCalculator.Domain
{
    public class GameBuilderService
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

            return new Game(frames, currentFrame, maxPins, GameStatus.NotStarted,0);
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
