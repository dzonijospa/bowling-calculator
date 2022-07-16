using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingCalculator.Domain.Test
{
    public class TenPinGameFunctional
    {
        [Theory]
        [ClassData(typeof(TenPinTestData))]
        public void TenPinFunctionalTest(TenPinTestItem tenPinTestItem)
        {
            var random = new Random();
            long gameId = random.Next();

            var game = CreateDefaultTenPinGame(gameId);

            foreach(byte roll in tenPinTestItem.Rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(game.IsGameFinished(), tenPinTestItem.GameFinished);
            Assert.Equal(game.RunningTotal, tenPinTestItem.ExpectedRunningTotal);
        }


        private static Game CreateDefaultTenPinGame(long gameId)
        {
            byte maxPins = 10;

            byte currentFrame = 1;

            var frames = new LinkedList<Frame>();

            var firstFrame = Frame.CreateDefaultFrame(1);

            frames.AddFirst(firstFrame);

            for(byte i= 2; i<=10; i++)
            {
                frames.AddLast(Frame.CreateDefaultFrame(i));
            }

            return new Game(gameId, frames, currentFrame, maxPins);
        }
    }

    public class TenPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<TenPinTestItem[]> GetEnumerator()
        {
            yield return TenPinTestItem.CreateTestPinItem(3,false, 1,2);
            yield return TenPinTestItem.CreateTestPinItem(300, true, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);//perfect game
            yield return TenPinTestItem.CreateTestPinItem(12, false, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            yield return TenPinTestItem.CreateTestPinItem(55, false, 1, 1, 1, 1, 9, 1, 2, 8, 9, 1, 10, 10);

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TenPinTestItem
    {
        public short ExpectedRunningTotal { get; set; }

        public List<byte> Rolls { get; set; }

        public bool GameFinished { get; set; }

        public static TenPinTestItem[] CreateTestPinItem(short expected ,bool finished,params byte[] rolls)
        {
            var allRolls = new List<byte>();
            foreach(byte roll in rolls)
            {
                allRolls.Add(roll);
            }
            return new TenPinTestItem[1] { new TenPinTestItem() { ExpectedRunningTotal = expected, GameFinished = finished, Rolls = allRolls } };
        }
    }

   
}
