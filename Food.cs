using System;
using System.Collections.Generic;

namespace HL2
{
    public class Food
    {
        public Food()
        {
            this.Point = new Point();
        }

        //normal, big
        public int Size;
        public Point Point { get; set; }

        public void RandomPoint(Snake snake, int n, int m)
        {
            Random random = new Random();
            do
            {
                this.Point.Row = random.Next(n);
                this.Point.Column = random.Next(m);
            } while (this.Point.Row >= n - 1 || this.Point.Column >= m - 1 || this.Point.Row <= 0 || this.Point.Column <= 0 || IsFoodInHeadOrBodyOfTheSnake(snake));
        }

        private bool IsFoodInHeadOrBodyOfTheSnake(Snake snake)
        {
            List<Point> points = new List<Point>();
            points.Add(snake.Head);
            points.AddRange(snake.Body);

            for(int i = 0; i < points.Count; i++)
            {
                Point element = points[i];
                if(element.Row == this.Point.Row && element.Column == this.Point.Column)
                {
                    return true;
                }
            }

            return false;
        }
    }
}