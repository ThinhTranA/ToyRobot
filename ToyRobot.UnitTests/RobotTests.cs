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

            for( int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
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

            Assert.AreEqual(false, robot.Place(6, 1, Direction.NORTH));
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
            robot.Place(5, 5, Direction.NORTH);
            robot.Move();
            Assert.AreEqual(5, robot.Y);
            Assert.AreEqual(0, string.Compare("5,5,NORTH", robot.Report()));

            robot.Place(0, 0, Direction.SOUTH);
            robot.Move();
            Assert.AreEqual(0, robot.Y);
            Assert.AreEqual(0, string.Compare("0,0,SOUTH", robot.Report()));

            robot.Place(5, 5, Direction.EAST);
            robot.Move();
            Assert.AreEqual(5, robot.X);
            Assert.AreEqual(0, string.Compare("5,5,EAST", robot.Report()));

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

        [TestMethod]
        public void PlaceBotInAvoidAreas()
        {
            var robot = new TestRobot();
            robot.Avoid(5, 5);
            robot.Avoid(2, 3);
            robot.Avoid(3, 0);
            var isPlaced = robot.Place(5, 5, Direction.NORTH);
            Assert.AreEqual(false, isPlaced);

            isPlaced = true;
            isPlaced = robot.Place(2, 3, Direction.NORTH);
            Assert.AreEqual(false, isPlaced);

            isPlaced = true;
            isPlaced = robot.Place(3, 0, Direction.NORTH);
            Assert.AreEqual(false, isPlaced);
        }

        [TestMethod]
        public void MoveBotToAvoidAreas()
        {
            var robot = new TestRobot();
            robot.Avoid(0, 1);
            robot.Avoid(1, 0);
            robot.Move();
            Assert.AreEqual(0, string.Compare("0,0,NORTH", robot.Report())); 

            robot.Place(3, 3, Direction.NORTH);
            robot.Avoid(3, 4);
            robot.Avoid(4, 3);
            robot.Avoid(2, 3);
            robot.Avoid(3, 2);
            robot.Move();
            Assert.AreEqual(0, string.Compare("3,3,NORTH", robot.Report())); 
        }

        [TestMethod]
        //Text Example, Interation 2
        public void MovesPlacesBotToAvoidAreas()
        {
            var robot = new TestRobot();

            robot.Place(1, 2, Direction.EAST);
            robot.Avoid(2, 2);
            robot.Avoid(2, 3);
            robot.Move();
            robot.Place(2, 3, Direction.EAST);
            robot.Move();
            robot.Left();
            robot.Move();

            Assert.AreEqual(0, string.Compare("1,3,NORTH", robot.Report()));
        }

        [TestMethod]
        //Text Example, Interation 3
        public void MovePlaceWithOldDirection()
        {
            var robot = new TestRobot();

            robot.Place(1, 2, Direction.EAST);
            robot.Avoid(2, 2);
            robot.Avoid(2, 3);
            robot.Move();
            robot.Place(2, 3);
            robot.Move();
            robot.Left();
            robot.Move();

            Assert.AreEqual(0, string.Compare("1,3,NORTH", robot.Report()));
        }

        [TestMethod]
        public void Interation4_A()
        {
            var robot = new TestRobot();

            robot.Place(0, 0, Direction.NORTH);
            robot.Right();
            robot.Move();

            Assert.AreEqual(0, string.Compare("1,3,NORTH", robot.Report()));
        }
        [TestMethod]
        public void Interation4_B()
        {
            var robot = new TestRobot();

            robot.Place(0, 0, Direction.NORTH);
            robot.Right();
            robot.Move();

            Assert.AreEqual(0, string.Compare("1,3,NORTH", robot.Report()));
        }
        [TestMethod]
        public void Interation4_C()
        {
            var robot = new TestRobot();

            robot.Place(0, 0, Direction.NORTH);
            robot.Right();
            robot.Move();

            Assert.AreEqual(0, string.Compare("1,3,NORTH", robot.Report()));
        }

    }
}
