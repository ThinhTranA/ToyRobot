using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace ToyRobot.UnitTests
{
    [TestClass]
    public class RobotTests
    {
        [TestMethod]
        public void Place_ValidInputs()
        {
            //TableTop tableTop = new ToyRobot.TableTop();
            var robot = new TestRobot();

            for( int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                   Assert.AreEqual(true, robot.Place(i, j, Direction.WEST));
                }
            }
        }

        [TestMethod]
        public void Place_InvalidInputs()
        {
            var robot = new TestRobot();
            robot.Place(2, 3, Direction.WEST);

            Assert.AreEqual(false, robot.Place(5, 1, Direction.NORTH));
            Assert.AreEqual(0, string.Compare("2,3,WEST", robot.Report() ));
        }

        [TestMethod]
        public void Move_Valid()
        {
            var robot = new TestRobot();
            robot.Place(0, 0, Direction.NORTH);
            robot.Move();

            Assert.AreEqual(0, string.Compare("0,1,NORTH", robot.Report()));
        }
        [TestMethod]
        public void Move_InValid()
        {
            var robot = new TestRobot();
            robot.Place(4, 4, Direction.NORTH);
            robot.Move();
            Assert.AreEqual(4, robot.Y);
            Assert.AreEqual(0, string.Compare("4,4,NORTH", robot.Report()));

            robot.Place(0, 0, Direction.SOUTH);
            robot.Move();
            Assert.AreEqual(0, robot.Y);
            Assert.AreEqual(0, string.Compare("0,0,SOUTH", robot.Report()));

            robot.Place(4, 4, Direction.EAST);
            robot.Move();
            Assert.AreEqual(4, robot.X);
            Assert.AreEqual(0, string.Compare("4,4,EAST", robot.Report()));

            robot.Place(0, 0, Direction.WEST);
            robot.Move();
            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, string.Compare("0,0,WEST", robot.Report()));
        }

        [TestMethod]
        public void Left()
        {
            var robot = new TestRobot();
            robot.Place(0, 0, Direction.NORTH);
            robot.Left();
            Assert.AreEqual(0, string.Compare("0,0,WEST", robot.Report()));

            robot.Left();
            Assert.AreEqual(0, string.Compare("0,0,SOUTH", robot.Report()));

            robot.Left();
            Assert.AreEqual(0, string.Compare("0,0,EAST", robot.Report()));

            robot.Left();
            Assert.AreEqual(0, string.Compare("0,0,NORTH", robot.Report()));
        }

        [TestMethod]
        public void Right()
        {
            var robot = new TestRobot();
            robot.Place(0, 0, Direction.NORTH);
            robot.Right();
            Assert.AreEqual(0, string.Compare("0,0,EAST", robot.Report()));

            robot.Right();
            Assert.AreEqual(0, string.Compare("0,0,SOUTH", robot.Report()));

            robot.Right();
            Assert.AreEqual(0, string.Compare("0,0,WEST", robot.Report()));

            robot.Right();
            Assert.AreEqual(0, string.Compare("0,0,NORTH", robot.Report()));

        }

        [TestMethod]
        public void Report()
        {
            var robot = new TestRobot();
            //Default value
            Assert.AreEqual(0, string.Compare("0,0,NORTH", robot.Report()));

            robot.Place(0, 0, Direction.NORTH);
            Assert.AreEqual(0, string.Compare("0,0,NORTH", robot.Report()));
        }


        [TestMethod]
        public void MovesCombinedLeft()
        {
            var robot = new TestRobot();
            robot.Place(1, 2, Direction.EAST);
            robot.Move();
            robot.Move();
            robot.Left();
            robot.Move();

            Assert.AreEqual(0, string.Compare("3,3,NORTH", robot.Report()));
        }

    }
}
