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
    public class FrameShould
    {

        [Fact]
        public void ApplyRollToFrameStateWhenRollHappens()
        {
            var mockFrameState = new Moq.Mock<IFrameState>();
            byte pinsDowned = 5;
            byte maxPins = 10;
            mockFrameState.Setup(x => x.ApplyRoll(pinsDowned, maxPins)).Returns(mockFrameState.Object);
            var frame = new Frame(1, mockFrameState.Object);

            frame.ApplyRoll(pinsDowned, maxPins);

            mockFrameState.Verify(mock => mock.ApplyRoll(pinsDowned, maxPins), Times.Once());
        }

        [Fact]
        public void ApplyFrameStateChangeWhenRollHappens()
        {
            var mockFrameState = new Moq.Mock<IFrameState>();
            var newFrameState = new Moq.Mock<IFrameState>();
            mockFrameState.Setup(x => x.ApplyRoll(It.IsAny<byte>(), It.IsAny<byte>())).Returns(newFrameState.Object);
            var frame = new Frame(1, mockFrameState.Object);
            
            frame.ApplyRoll(1, 10);

            Assert.Same(frame.FrameState, newFrameState.Object);
        }
    }
}
