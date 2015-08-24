//Please note: This application is purely for my own education, to run through coding 
//examples by following tutorials, and to just tinker around with coding.  
//I know it’s bad practice to heavily comment code (code smell), but comments in all of my 
//exercises will largely be left intact as this serves me 2 purposes:
//    I want to retain what my original thought process was at the time
//    I want to be able to look back in 1..5..10 years to see how far I’ve come
//    And I enjoy commenting on things, however redundant this may be . . . 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Math_Quiz
{
    public partial class Form1 : Form
    {
        // Create a random object to generate random numbers
        Random randomizer = new Random();

        // Integer variables to store the numbers for the addition problems
        private int addend1;
        private int addend2;

        // Integer variables to store the numbers for the subtraction problems
        private int minuend;
        private int subtrahend;

        // Integer variables to store the numbers for the multiplicatin problems
        private int multiplicand;
        private int multiplier;

        // Integer variables to store the numbers for the division problems
        private int dividend;
        private int divisor;

        // Variable to keep track of the time remaining
        private int timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        public void StartTheQuiz()
        {
            // The Addition line
            // Varables addend1 and addend2 get assigned random values from 0-50
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            // The value held in the new addend variables above are converted to strings to populate the form
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            // Initializes the result of the formula to 0
            sum.Value = 0;

            // The subtraction line
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // The multiplication line
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // The division line
            divisor = randomizer.Next(2, 11);
            // Using a method to ensure that the divedend is always a multiplier of the divisor for whole numbers
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer
            timeLeft = 30;
            timeLabel.Text = timeLeft + " seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
            timeLabel.BackColor = SystemColors.Control;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // Checks for all the correct answers and if true congratulates the user
                timer1.Stop();
                MessageBox.Show("You got all the answers right!", "Gratz!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // If the time is greater than 0, than the clock will count down and display in the timer box
                // same as:
                // timeLeft = timeLeft - 1;
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
                // color.control

                if (timeLeft < 6)
                    timeLabel.BackColor = Color.Red;
            }
            else
            {
                // This executes once the clock hits 0, shows a message and displays the answers
                timer1.Stop();
                timeLabel.Text = "Time's Up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else return false;
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lenghOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lenghOfAnswer);
            }
        }
    }
}
