using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

using ToyRobot;

namespace RobotXamarin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        const int numberOfTiles = 5;
        Image robotImage;

        Robot robot;
        public MainPage()
        {
            InitializeComponent();

            DrawSquareTableTop(numberOfTiles);
            SpawnRobot();
        }

        #region Commands
        private void Move_Clicked(object sender, EventArgs e)
        {
            robot.Move();

            // Bottom left is (0,0) and top right is (4,4)
            tableTop.Children.Add(robotImage, robot.X, numberOfTiles - 1 - robot.Y);
        }

        int curDegree = 0;
        private void Left_Clicked(object sender, EventArgs e)
        {
            robot.Left();
            curDegree -= 90;
            robotImage.RotateTo(curDegree);
        }

        private void Right_Clicked(object sender, EventArgs e)
        {
            robot.Right();
            curDegree += 90;
            robotImage.RotateTo(curDegree);
        }

        private void Report_Clicked(object sender, EventArgs e)
        {
            reportLabel.Text = robot.Report();
        }
        private void Place_Clicked(object sender, EventArgs e)
        {
            try
            {
                var x = int.Parse(xPosition.Text);
                var y = int.Parse(yPosition.Text);
                var direction = (Direction)Enum.Parse(typeof(Direction), facingDirection.SelectedItem.ToString());
                robot.Place(x, y, direction);

                var rotateDegree = GetRotateDegree(robot);
                robotImage.RotateTo(rotateDegree);
                tableTop.Children.Add(robotImage, robot.X, numberOfTiles - 1 - robot.Y);
            }
            catch(Exception ex)
            {
                DisplayAlert("Invalid inputs", "Please enter all fields before press Place", "OK");
            }
        }

        #endregion

        private void DrawSquareTableTop(int numOfTile)
        {
            for (int i = 0; i < numOfTile; i++)
            {
                for (int j = 0; j < numOfTile; j++)
                {
                    tableTop.Children.Add(new Frame { BackgroundColor = Color.Gray, BorderColor = Color.Red }, i, j);
                }
            }
        }

        private void SpawnRobot()
        {
            if (tableTop.Children.Count < 1)
                return;

            robot = new Robot();
            robotImage = new Image
            {
                Source = "robot_icon.png",
                HeightRequest = 50,
                WidthRequest = 50,
            };
            // Bottom left is (0,0) and top right is (4,4)
            tableTop.Children.Add(robotImage, 0, numberOfTiles - 1);
        }

        private int GetRotateDegree(Robot robot)
        {
            switch (robot.FacingDirection)
            {
                case Direction.NORTH:
                    return 0;
                case Direction.EAST:
                    return 90;
                case Direction.SOUTH:
                    return 180;
                case Direction.WEST:
                    return 270;
            }
            return 0;
        }

    }
}
