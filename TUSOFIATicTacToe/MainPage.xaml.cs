using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TUSOFIATicTacToe.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int _currentPlayer = 1;
        private bool _gameEnd = false;
        private int _player1 = 0;
        private int _player2 = 0;
        private int[][] _matrix = new int[3][];
        private Button[] _buttons = new Button[9];

        public MainPage()
        {
            this.InitializeComponent();
            ResetMatrix();
        }

        private void X0Y0_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 0, 0, 0);
        }

        private void X0Y1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 0, 1, 1);
        }

        private void X0Y2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 0, 2, 2);
        }

        private void X1Y0_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 1, 0, 3);
        }

        private void X1Y1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 1, 1, 4);
        }

        private void X1Y2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 1, 2, 5);
        }

        private void X2Y0_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 2, 0, 6);
        }

        private void X2Y1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 2, 1, 7);
        }

        private void X2Y2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BoardClick(sender, 2, 2, 8);
        }

        private void ResetMatrix()
        {
            for (int i = 0; i < 3; i++)
            {
                _matrix[i] = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    _matrix[i][j] = 0;
                }
            }
        }

        private void BoardClick(object sender, int x, int y, int b)
        {
            if (_gameEnd) return;
            var button = sender as Button;
            if (button.Content.ToString() != ".")
            {
                return;
            }

            button.Content = _currentPlayer == 1 ? "O" : "X";
            _matrix[x][y] = _currentPlayer;
            _buttons[b] = button;
            if (CheckWinner() != 0)
            {
                _gameEnd = true;
            }

            SwapPlayer();
        }

        private void SwapPlayer()
        {
            _currentPlayer = _currentPlayer == 1 ? 2 : 1;
            ChangeFieldValue("CurrentPlayer", _currentPlayer.ToString());
        }

        private void ChangeFieldValue(string field, string value)
        {
            var f = (TextBlock)this.FindName(field);
            if (f == null) throw new ArgumentNullException(nameof(f));
            f.Text = value;
        }

        private int CheckWinner()
        {
            if (CheckRows() || CheckCols() || CheckDiagonals())
            {
                _gameEnd = true;
                if (_currentPlayer == 1)
                {
                    _player1++;
                    ChangeFieldValue("Player" + _currentPlayer, _player1.ToString());
                }
                else
                {
                    _player2++;
                    ChangeFieldValue("Player" + _currentPlayer, _player2.ToString());

                }

                return _currentPlayer;
            }

            return 0;
        }

        private bool CheckDiagonals()
        {
            if (_matrix[0][0] == _currentPlayer && _matrix[1][1] == _currentPlayer && _matrix[2][2] == _currentPlayer)
            {
                SetWinLineColor(0, 4, 8);
                return true;
            }

            if (_matrix[0][2] == _currentPlayer && _matrix[1][1] == _currentPlayer && _matrix[2][0] == _currentPlayer)
            {
                SetWinLineColor(2, 4, 6);
                return true;
            }

            return false;
        }

        private void SetWinLineColor(int p1, int p2, int p3)
        {
            _buttons[p1].Background = new SolidColorBrush(Colors.DodgerBlue);
            _buttons[p2].Background = new SolidColorBrush(Colors.DodgerBlue);
            _buttons[p3].Background = new SolidColorBrush(Colors.DodgerBlue);
        }

        private bool CheckCols()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_matrix[0][i] == _currentPlayer && _matrix[1][i] == _currentPlayer && _matrix[2][i] == _currentPlayer)
                {
                    SetWinLineColor(i, i + 3, i + 6);
                    return true;
                }
            }

            return false;
        }

        private bool CheckRows()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_matrix[i][0] != _currentPlayer || _matrix[i][1] != _currentPlayer || _matrix[i][2] != _currentPlayer) continue;
                if (i == 0)
                {
                    SetWinLineColor(0, 1, 2);
                }
                else if (i == 1)
                {
                    SetWinLineColor(3, 4, 5);
                }
                else if (i == 2)
                {
                    SetWinLineColor(6, 7, 8);
                }

                return true;
            }

            return false;
        }

        private void StartNewGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            _gameEnd = false;
            ResetMatrix();
            for (int i = 0; i < 9; i++)
            {
                try
                {
                    _buttons[i].Content = ".";
                    _buttons[i].Background = new SolidColorBrush(Colors.LightGray);
                }
                catch (Exception exception)
                {
                }
            }
        }

        private void Reset_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _player1 = 0;
            _player2 = 0;

            ChangeFieldValue("Player1", _player1.ToString());
            ChangeFieldValue("Player2", _player2.ToString());

            NewGame();
        }
    }
}
