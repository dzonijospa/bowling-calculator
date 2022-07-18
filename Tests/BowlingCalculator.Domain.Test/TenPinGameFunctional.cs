using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace BowlingCalculator.Domain.Test
{
    public class TenPinGameFunctional
    {
        private readonly byte MAX_PINS = 10;
        private readonly byte FRAME_NUMBER = 10;

        [Theory]
        [ClassData(typeof(TenPinTestData))]
        public void TenPinFunctionalTest(TenPinTestItem tenPinTestItem)
        {
            var gameBuilder = new Domain.Services.GameBuilder();
            var game = gameBuilder.CreateDefaultGame(MAX_PINS,FRAME_NUMBER);

            foreach(byte pinsDowned in tenPinTestItem.Rolls)
            {
                game.Roll(pinsDowned);
            }

            Assert.Equal(tenPinTestItem.GameFinished, game.IsGameCompleted());
            Assert.Equal(tenPinTestItem.ExpectedRunningTotal,game.RunningTotal);
        }
    }

    public class TenPinTestData : IEnumerable<object[]>
    {
        public IEnumerator<TenPinTestItem[]> GetEnumerator()
        {
            yield return TenPinTestItem.CreateTestPinItem(expectedRunningTotal:3,finished:false,
                                                          1,2);
            yield return TenPinTestItem.CreateTestPinItem(expectedRunningTotal:0, finished:false,
                                                           5);
            yield return TenPinTestItem.CreateTestPinItem(expectedRunningTotal:300, finished: true, 
                                                            10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);//perfect game
            yield return TenPinTestItem.CreateTestPinItem(expectedRunningTotal:12, finished:false, 
                                                            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            yield return TenPinTestItem.CreateTestPinItem(expectedRunningTotal:55, finished:false, 
                                                          1, 1, 1, 1, 9, 1, 2, 8, 9, 1, 10, 10);
            yield return TenPinTestItem.CreateTestPinItem(expectedRunningTotal:0, finished: true, 
                                                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 );//gutter game
            yield return TenPinTestItem.CreateTestPinItem(expectedRunningTotal:40, finished:false,
                                                          4,5,6,2,4,6,4,5);

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

        public static TenPinTestItem[] CreateTestPinItem(short expectedRunningTotal, bool finished,params byte[] rolls)
        {
            var allRolls = new List<byte>();
            foreach(byte roll in rolls)
            {
                allRolls.Add(roll);
            }
            return new TenPinTestItem[1] { new TenPinTestItem() { ExpectedRunningTotal = expectedRunningTotal, GameFinished = finished, Rolls = allRolls } };
        }
    }

   
}
