using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot
{
    public class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private const int minPositionValue = 0;
        private const int maxPositionValue = 4;
        public Direction FacingDirection { get; private set; }

        //Default spawn location of robot.
        public Robot()
        {
            X = 0;
            Y = 0;
            FacingDirection = Direction.NORTH;
        }

        public bool Place(int x, int y, Direction direction )
        {
            if (IsWithinTable(x) && IsWithinTable(y))
            {
                X = x;
                Y = y;
                FacingDirection = direction;
                return true;
            }
            else
            {
                return false;
            }
         
        }

        public void Move()
        {
            switch(FacingDirection)
            {
                case Direction.NORTH:
                    if(IsWithinTable(Y + 1))
                        Y++;
                    break;
                case Direction.SOUTH:
                    if (IsWithinTable(Y - 1))
                        Y--;
                    break;
                case Direction.EAST:
                    if(IsWithinTable(X + 1))
                        X++;
                    break;
                case Direction.WEST:
                    if (IsWithinTable(X - 1))
                        X--;
                    break;
            }
        }
        public void Left()
        {
            switch(FacingDirection)
            {
                case Direction.NORTH:
                    FacingDirection = Direction.WEST;
                    break;
                case Direction.SOUTH:
                    FacingDirection = Direction.EAST;
                    break;
                case Direction.EAST:
                    FacingDirection = Direction.NORTH;
                    break;
                case Direction.WEST:
                    FacingDirection = Direction.SOUTH;
                    break;
            }
        }
        public void Right()
        {
            switch (FacingDirection)
            {
                case Direction.NORTH:
                    FacingDirection = Direction.EAST;
                    break;
                case Direction.SOUTH:
                    FacingDirection = Direction.WEST;
                    break;
                case Direction.EAST:
                    FacingDirection = Direction.SOUTH;
                    break;
                case Direction.WEST:
                    FacingDirection = Direction.NORTH;
                    break;
            }
        }

        public string Report()
        {
            return string.Format("{0},{1},{2}" , X, Y, FacingDirection.ToString());
        }

        private bool IsWithinTable(int value)
        {
            return value >= minPositionValue && value <= maxPositionValue;
        }
    }

    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }
}
