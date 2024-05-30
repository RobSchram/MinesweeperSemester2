//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace UnitTestProject
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//            public void MinePlacer_CorrectMinePercent_AddsExpectedNumberOfMines()
//            {
//                int horizontal = 10;
//                int vertical = 10;
//                decimal minePercent = 0.2m;
//                var field = new Cell[horizontal, vertical];
//                var placer = new AddMines();

//                placer.MinePlacer(field, minePercent);

//                int totalCells = horizontal * vertical;
//                int expectedMines = (int)(totalCells * minePercent);
//                int actualMines = 0;
//                foreach (var cell in field)
//                {
//                    if (cell.IsMine == 1)
//                    {
//                        actualMines++;
//                    }
//                }
//                Assert.AreEqual(expectedMines, actualMines);
//        }
//    }
//}
