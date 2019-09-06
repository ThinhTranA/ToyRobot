
using System.Collections.Generic;

namespace ToyRobot
{
    public class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public List<Obstruction> Obstructions { get; set; } = new List<Obstruction>();

        private const int minPositionValue = 0;
        private const int maxPositionValue = 5;
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
            if (IsPositionObstructed(x, y))
                return false;  //obstructed, can't go there.

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
        public bool Place(int x, int y)
        {
            if (IsPositionObstructed(x, y))
                return false;  //obstructed, can't go there.
            if (IsWithinTable(x) && IsWithinTable(y))
            {
                X = x;
                Y = y;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Move()
        {
            if(IsPositionObstructedToMove(X,Y))
                return;  //obstructed, can't go there.

            switch (FacingDirection)
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

        //Inform robot about obstruction on X,Y
        public void Avoid(int x, int y)
        {
            if( IsWithinTable(x) && IsWithinTable(y)) 
                Obstructions.Add(new Obstruction { X = x, Y = y });
        }

        public string Report()
        {
            return string.Format("{0},{1},{2}" , X, Y, FacingDirection.ToString());
        }

        private bool IsWithinTable(int value)
        {
            return value >= minPositionValue && value <= maxPositionValue;
        }
        private bool IsPositionObstructed(int x, int y)
        {
            bool _obstructed = false;
            //Check if there is any obstructions in the position
            foreach (var obs in Obstructions)
            {
                if (obs.X == x && obs.Y == y)
                {
                    _obstructed = true; //TODO: escape for loop when found obstructed.
                }
            }

            return _obstructed;
        }

        private bool IsPositionObstructedToMove(int x, int y)
        {
            //Make sure it pass value, not reference, try this for now.
            int tryX = x;
            int tryY = y;

            switch (FacingDirection)
            {
                case Direction.NORTH:
                    tryY++;
                    break;
                case Direction.SOUTH:
                    tryY--;
                    break;
                case Direction.EAST:
                    tryX++;
                    break;
                case Direction.WEST:
                    tryX--;
                    break;
            }

            return IsPositionObstructed(tryX, tryY);
        }
    }

    public enum Direction
    {
        NORTH,
        NORTH_EAST,
        NORTH_WEST,
        SOUTH,
        SOUTH_EAST,
        SOUTH_WEST,
        EAST,
        WEST
    }

    public class Obstruction
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
