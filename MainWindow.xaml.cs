using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace MyAttemptAtQueens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public SqlConnection sqlcon;
        public Button[,] ChessBoard=new Button[8, 8] ;
        private QueenSpot[,] ChessSpot=new QueenSpot [8,8];
        public int  [] QueenOnY =  { 0, 0, 0, 0, 0, 0, 0, 0 };
        public int TotalQueen = 0;
        public Stopwatch watch = new Stopwatch();
        public string name;

        public MainWindow()
        {
            InitializeComponent();
           
             //ChessBoard=new Button[8,8] ;
                                  //=  {   { Btn0_0,Btn0_1,Btn0_2,Btn0_3,Btn0_4,Btn0_5,Btn0_6,Btn0_7},
                                   //      { Btn1_0,Btn1_1,Btn1_2,Btn1_3,Btn1_4,Btn1_5,Btn1_6,Btn1_7},
                                     //    { Btn2_0,Btn2_1,Btn2_2,Btn2_3,Btn2_4,Btn2_5,Btn2_6,Btn2_7},
                                       //  { Btn3_0,Btn3_1,Btn3_2,Btn3_3,Btn3_4,Btn3_5,Btn3_6,Btn3_7},
                                         //{ Btn4_0,Btn4_1,Btn4_2,Btn4_3,Btn4_4,Btn4_5,Btn4_6,Btn4_7},
                                         //{ Btn5_0,Btn5_1,Btn5_2,Btn5_3,Btn5_4,Btn5_5,Btn5_6,Btn5_7},
                                         //{ Btn6_0,Btn6_1,Btn6_2,Btn6_3,Btn6_4,Btn6_5,Btn6_6,Btn6_7},
                                         //{ Btn7_0,Btn7_1,Btn7_2,Btn7_3,Btn7_4,Btn7_5,Btn7_6,Btn7_7},};
            int i = 0, j = 0; int u=1;
            GridV.Children.Cast<Button>().ToList().ForEach(Chess =>
            {
                
                Chess.Content = " ";
                 ChessBoard[i, j] = Chess;

                ChessSpot[i, j]= new QueenSpot(i, j, Chess); 
                j++;
                 if (j > 7)
                {   
                    i++;
                    j = 0;
                }
                

            });
            watch.Start();


        }

        private void QueenPlacer(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            var column = Grid.GetColumn(btn);

            var row = Grid.GetRow(btn);
            
           

            // btn.Foreground=myButton.Foreground;

            //  btn.Content = "[*]";
            if (!ChessSpot[row, column].HasQueen && ChessSpot[row, column].Accessible)
            {
                if (QueenOnY[row] == 0)
                {
                    QueenOnY[row] = 1;
                    ChessSpot[row, column].HasQueen = true;
                    ChessSpot[row, column].Accessible = false;
                    TotalQueen++;


                    MarkUnAccessible(row, column);
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri("G:\\University\\Sem 5\\MyAttemptAtQueens\\MyAttemptAtQueens\\Chess Queen.png"));
                    btn.Background = brush;
                    
                    btn.Content = "   ";
                    CheckSpace(row, column);
                    textBlock1.Text = watch.ElapsedMilliseconds.ToString();

                }
            }
        }

        private void CheckSpace(int x,int y)
        {
            int flag = 0;
            int check=0;
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (ChessSpot[i, j].Accessible == false || ChessSpot[i, j].HasQueen)
                    {
                        if (TotalQueen == 8)
                        {
                            flag = 0;
                        }
                       
                    }
                    else {
                        flag = 1;
                        //break;
                    }
                    if(ChessSpot[i, j].Accessible == false)
                    {
                        check++;
                       
                    }
               

                }
            }
            if (flag == 0 && TotalQueen == 8)
            {watch.Stop();
                MessageBox.Show("YOU WON You place all 8 queens on 8*8 Grid");
                
            }
            else if (flag == 1)
            {
                textBlock3.Text = "Placed Queens" + TotalQueen;


            }
            if (check == 64 && TotalQueen < 8)
            {   watch.Stop();
                MessageBox.Show("You Lost");
                
            }
        }

        public void MarkUnAccessible(int row, int col)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("G:\\University\\Sem 5\\MyAttemptAtQueens\\MyAttemptAtQueens\\Red X.png"));
           // Background = brush;

            try
            {
                   for (int y = 0; y < 8; y++)
                   {
                          if (y == row) continue;
                          if(!ChessBoard[row, y].Content.Equals("   "))
                       ChessBoard[row, y].Background = brush; 

                    ChessSpot[row, y].Accessible = false;
                }
                for (int x = 0; x < 8; x++)
                   {
                    if (col == x)
                        continue;

                    if (!ChessBoard[x, col].Content.Equals("   "))
                    ChessBoard[x, col].Background = brush; 

                    ChessSpot[x, col].Accessible=false;
                   }
                     for (int j = 1; (row + j) <= 7 && (col + j) <= 7; j++)
                   {
                           ChessBoard[row + j, col + j].Background = brush; 
                    ChessSpot[row + j, col + j].Accessible = false;
                   }
                   for (int j = 1; row - j >= 0 && col - j >= 0; j++)
                   {
                        ChessBoard[row - j, col - j].Background = brush; 
                    ChessSpot[row - j, col - j].Accessible = false;
                }
                   for (int j = 1; row + j <= 7 && col - j >= 0; j++)
                   {
                           ChessBoard[row + j, col - j].Background = brush; 
                            ChessSpot[row + j, col - j].Accessible = false;
                   }
                   for (int j = 1; row - j >= 0 && col + j <= 7; j++)
                   {
                     ChessBoard[row - j, col + j].Background = brush; ;
                    ChessSpot[row - j, col + j].Accessible = false;
                   } 
               
            }
            catch (Exception ex) { }
        }

        private void playAgain_Click(object sender, RoutedEventArgs e)
        {
            
            
            

            this.NavigationService.Refresh();

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection sqlcon;
                string connectionstring;

                connectionstring = "Data Source = DESKTOP-I5DML83\\SQLEXPRESS; Integrated Security = True";
                sqlcon = new SqlConnection(connectionstring);
                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }
                string query = "INSERT INTO UserScore (Name,Score)  VALUES('Player' , " + textBlock1.Text + ")";
                using (SqlCommand sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.ExecuteNonQuery();
                }
                this.NavigationService.Navigate(new Uri("ExitPage.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex) {
                this.NavigationService.Navigate(new Uri("ExitPage.xaml", UriKind.RelativeOrAbsolute));
            }

        }
    }
}
