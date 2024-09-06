using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HL2
{
   
    class Program
    {
        static bool isGameOver = false;

        static int N = 15;
        static int M = 30;
        static int speed = 500;
        static int score = 0;

        static string SKIN = "*";
        static string BRICK = "#";
        static string SPACE = " ";
        static string APPLE = "@";

        static string direction = Direction.DIRECTION_RIGHT;

        static string[,] board = new string[N, M];
        static Snake snake = new Snake();
        static Food food = new Food();

        /// <summary>
        /// Tính toán vị trí của tường
        /// </summary>
        private static void calcWall()
        {
            for (int i = 0; i < N; i++)
            {
                for(int j = 0; j < M; j++)
                {
                    if(i == 0 || i == N - 1 || j == 0 || j == M - 1)
                    {
                        board[i, j] = BRICK;
                    }
                }
            }
        }

        private static void calcSnake()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    //đầu rắn
                    int row = snake.Head.Row;
                    int colmn = snake.Head.Column;
                    if (i == row && j == colmn)
                    {
                        board[i, j] = SKIN;
                    }


                    //thân rắn
                    List<Point> body = snake.Body;
                    for (int k = 0; k < body.Count; k++)
                    {
                        Point element = body[k];
                        if (i == element.Row && j == element.Column)
                        {
                            board[i, j] = SKIN;
                        }
                    }
                }
            }
        }

        private static void calcFood()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (i == food.Point.Row && j == food.Point.Column)
                    {
                        board[i, j] = APPLE;
                    }
                }
            }
        }

        /// <summary>
        /// Khởi tạo giá trị ban đầu cho board là các dấu space
        /// </summary>
        private static void resetBoard()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    board[i, j] = SPACE;
                }
            }
        }

        /// <summary>
        /// In dữ liệu trong board ra màn hình
        /// </summary>
        private static void printBoard()
        {
            Console.WriteLine($"Score: {score}, speed: {speed}");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    string value = board[i, j];

                    //Nếu là wall thì có màu đỏ
                    if (value.Equals(BRICK))
                    { 
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(value);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(value);
                    }
                }

                Console.WriteLine();
            }
        }

        static void ListenKey()
        {
            while (!isGameOver)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (direction != Direction.DIRECTION_DOWN)
                    {
                        direction = Direction.DIRECTION_UP;
                    }
                       
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (direction != Direction.DIRECTION_UP)
                    {
                        direction = Direction.DIRECTION_DOWN;
                    }
                    
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (direction != Direction.DIRECTION_RIGHT)
                    {
                        direction = Direction.DIRECTION_LEFT;
                    }
                   
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if(direction != Direction.DIRECTION_LEFT)
                    {

                        direction = Direction.DIRECTION_RIGHT;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Thread thread = new Thread(Program.ListenKey);
            thread.Start();

            food.RandomPoint(snake, N, M);

            while (true)
            {
                Console.Clear();

                resetBoard();

                //Tính toán vị trí của tường
                calcWall();

                //Tính toán vị trí của thức ăn
                calcFood();

                //Tính toán vị trí của rắn
                calcSnake();

                //In ra dữ liệu đã được tính toán
                printBoard();

                if(snake.Head.Row == food.Point.Row && snake.Head.Column == food.Point.Column)
                {
                    //rắn dài ra
                    snake.Body.Add(new Point(-1, -1));

                    //điểm và tốc độ tăng lên
                    score++;
                    speed -= 50;
                    speed = speed < 100 ? 100 : speed;

                    //thức ăn đổi vị trí
                    food.RandomPoint(snake, N, M);
                }

                //đầu va vào thân thì chết
                if (snake.IsHeadInBody())
                {
                    Console.WriteLine("Game Over");
                    isGameOver = true;
                    Console.Clear();
                    printBoard();
                    break;
                }
                 
                snake.move(direction, N, M); 

                Task.Delay(speed).Wait();
            }

            Console.WriteLine("Press any key to exit this game.....");
        } 
    }
}