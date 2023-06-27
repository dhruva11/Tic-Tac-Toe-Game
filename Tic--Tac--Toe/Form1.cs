using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToeForm : Form
    {
        private Button[] buttons;
        private bool currentPlayer; // true for "X", false for "O"
        private int moveCount;
        private bool gameOver;
        private bool isAgainstComputer;
        private int player1Score;
        private int player2Score;

        public TicTacToeForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[] { button0, button1, button2, button3, button4, button5, button6, button7, button8 };
            currentPlayer = true;
            moveCount = 0;
            gameOver = false;
            isAgainstComputer = false;
            player1Score = 0;
            player2Score = 0;
            UpdateScoreboard();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (gameOver)
                return;

            Button button = (Button)sender;

            if (button.Text != "")
                return;

            button.Text = currentPlayer ? "X" : "O";
            currentPlayer = !currentPlayer;
            moveCount++;

            if (CheckWinCondition())
            {
                string winner = currentPlayer ? "O" : "X";
                MessageBox.Show("Player " + winner + " wins!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gameOver = true;

                if (currentPlayer) // Player 2 wins
                    player2Score++;
                else // Player 1 wins
                    player1Score++;

                UpdateScoreboard();
            }
            else if (moveCount == 9)
            {
                MessageBox.Show("It's a draw!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gameOver = true;
            }

            if (isAgainstComputer && !gameOver && !currentPlayer)
            {
                MakeComputerMove();
            }
        }

        private bool CheckWinCondition()
        {
            List<int[]> winConditions = new List<int[]>
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            foreach (int[] condition in winConditions)
            {
                if (buttons[condition[0]].Text != "" && buttons[condition[0]].Text == buttons[condition[1]].Text && buttons[condition[1]].Text == buttons[condition[2]].Text)
                    return true;
            }

            return false;
        }

        private void MakeComputerMove()
        {
            // Simple random move for the computer player
            Random random = new Random();
            int index = random.Next(0, 9);

            while (buttons[index].Text != "")
            {
                index = random.Next(0, 9);
            }

            buttons[index].Text = currentPlayer ? "X" : "O";
            currentPlayer = !currentPlayer;
            moveCount++;

            if (CheckWinCondition())
            {
                string winner = currentPlayer ? "O" : "X";
                MessageBox.Show("Player " + winner + " wins!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gameOver = true;

                if (currentPlayer) // Player 2 wins
                    player2Score++;
                else // Player 1 wins
                    player1Score++;

                UpdateScoreboard();
            }
            else if (moveCount == 9)
            {
                MessageBox.Show("It's a draw!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gameOver = true;
            }
        }

        private void UpdateScoreboard()
        {
            labelPlayer1Score.Text = player1Score.ToString();
            labelPlayer2Score.Text = player2Score.ToString();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Text = "";
            }

            currentPlayer = true;
            moveCount = 0;
            gameOver = false;

            if (isAgainstComputer && !currentPlayer)
            {
                MakeComputerMove();
            }
        }

        private void checkBoxComputer_CheckedChanged(object sender, EventArgs e)
        {
            isAgainstComputer = checkBoxComputer.Checked;

            if (isAgainstComputer && !currentPlayer)
            {
                MakeComputerMove();
            }
        }
    }
}
