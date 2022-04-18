using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace SudokuHani
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>RG 
    public partial class MainWindow : Window
    {
        int indices = 0;
        static string[] validChars = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        static string[,] defaultBoard = new string[,]
        {
            {"-","A","-","-","-","3","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","3","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","7","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","B","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"},
            {"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-"}
        };
        public string[,] board = new string[,] {};
        public string[,] savedBoard = new string[,] {};


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            outputBlock.Text = "";
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.ShowDialog();
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Title = "Browse Text Files";
                fileBlock.Text = "Chosen File: " + openFileDialog1.FileName;
                string file = openFileDialog1.FileName;
                string fileText = File.ReadAllText(file);
                createBoardFromFile(fileText);
                printBoard(board,outputBlock);
                uButton.IsEnabled = true;
                
            }
            catch (Exception ee)
            {
                errorBlock.Text = ee.Message;
            }

        }

        public void createBoardFromFile(string fileText)
        {
            fileText = fileText.Replace("\n", String.Empty).Replace("\r", String.Empty).Replace("\t", String.Empty);

            if (fileText.Length != 256)
            {
                outputBlock.Text = "Sudoku file must have exactly 256 characters, default board will be used instead. \n";
                return;
            }

            char[] chars = fileText.ToCharArray();
            int count = 0;
            board = (string[,])defaultBoard.Clone();
            for (int row = 0; row < 16; row++)
            {
                for (int col = 0; col < 16; col++)
                {
                    board[row, col] = chars[count].ToString();
                    count++;
                }
            }

        }

        public void printBoard(string[,] board, TextBlock t)
        {
            for (int row = 0; row < 16; row++)
            {
                for (int column = 0; column < 16; column++)
                {
                    if (column % 4 == 0 & column != 0)
                    {
                        t.Text += "|";
                    }
                    t.Text += (board[row, column]);
                }
                t.Text += ("\n");
                if (row % 4 == 3 && row != 15)
                {
                    t.Text += "-------------------\n";
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            outputBlock.Text = "";
            board = (string[,])defaultBoard.Clone();
            printBoard(board,outputBlock);
            uButton.IsEnabled = true;
        }

        public bool uninformedSolve(string[,] board)
        {
            indices++;
            for (int row = 0; row < 16; row++)
            {
                for (int col = 0; col < 16; col++)
                {
                    if (board[row, col] == "-")
                    {
                        for (int index = 0; index < validChars.Length; index++)
                        {
                            if (isValidMove(board, row, col, validChars[index]))
                            {
                                board[row, col] = validChars[index];
                                if (uninformedSolve(board))
                                {
                                    return true;
                                }
                                else
                                {
                                    board[row, col] = "-";
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }


        static bool isValidMove(string[,] board, int row, int column, string value)
        {
            for (int i =0; i <16; i++)
            {
                if (board[row,i].Equals(value) && i != column)
                {
                    return false;
                }
                if (board[i,column].Equals(value) && i!= row)
                {
                    return false;
                }
            }

            int sectionRow = 4 * (row / 4);
            int sectionColumn = 4 * (column / 4);
            for (int i = sectionRow; i < sectionRow + 4; i++)
            {
                for (int j = sectionColumn; j < sectionColumn + 4; j++)
                {
                    if (board[i,j].Equals(value) && i != row && j != column)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void uButton_Click(object sender, RoutedEventArgs e)
        {
            outputBlock_Copy.Text = "";
            if (uninformedSolve(board))
            {
                printBoard(board, outputBlock_Copy);
            }
            else
            {
                outputBlock_Copy.Text = "No valid solution found.";
            }
            indicesLabel.Content = "Indices Accessed: ";
            indicesLabel.Content += indices.ToString();
            indices = 0;
            resetBoard();
        }
        public void resetBoard()
        {
            if (savedBoard.Length != 0)
            {
                board = (string[,])savedBoard.Clone();
            }
            else
            {
                board = (string[,])defaultBoard.Clone();
            }
        }
    }
}
