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
            //xPosition.Text;
            //yPosition.Text;
            //facingDirection.;
        }

        #endregion

        void DrawSquareTableTop(int numOfTile)
        {
            for (int i = 0; i < numOfTile; i++)
            {
                for (int j = 0; j < numOfTile; j++)
                {
                    tableTop.Children.Add(new Frame { BackgroundColor = Color.Gray, BorderColor = Color.Red }, i, j);
                }
            }
        }

        void SpawnRobot()
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

       
    }
}
