using System;
using System.Collections.Generic;

namespace HL2
{
    public class Snake
    {
        public Snake()
        {
            this.Head = new Point(1, 1);
            this.Body = new List<Point>();
            this.Body.Add(new Point(-1, -1));
            this.Body.Add(new Point(-1, -1));
        }

        public Point Head { set; get; }
        public List<Point> Body { set; get; }

        private void moveBody(int aRow, int aColumn)
        {
            for (int i = 0; i < this.Body.Count; i++)
            {
                int tmpRow = this.Body[i].Row;
                int tmpColumn = this.Body[i].Column;

                this.Body[i].Row = aRow;
                this.Body[i].Column = aColumn;

                aRow = tmpRow;
                aColumn = tmpColumn;
            }
        }

        public bool IsHeadInBody()
        {
            for (int i = 0; i < this.Body.Count; i++)
            {
                Point element = this.Body[i];
                if (element.Row == this.Head.Row && element.Column == this.Head.Column)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsHeadInWall(int n, int m)
        {
            return this.Head.Row <= 0 || this.Head.Row >= n - 1 || this.Head.Column <= 0 || this.Head.Column >= m - 1;
        }

        public void move(string direction, int n, int m)
        {
            if (direction == Direction.DIRECTION_RIGHT)
            {
                int currentColumn = this.Head.Column;
                this.Head.Column = currentColumn + 1;

                if (this.Head.Column >= m - 1)
                {
                    this.Head.Column = 1;
                }

                this.moveBody(this.Head.Row, currentColumn);
            }
            else if (direction == Direction.DIRECTION_LEFT)
            {
                int currentColumn = this.Head.Column;
                this.Head.Column = currentColumn - 1;

                if (this.Head.Column <= 0)
                {
                    this.Head.Column = m - 2;
                }

                this.moveBody(this.Head.Row, currentColumn);
            }
            else if (direction == Direction.DIRECTION_UP)
            {
                int currentRow = this.Head.Row;
                this.Head.Row = currentRow - 1;

                if (this.Head.Row <= 0)
                {
                    this.Head.Row = n - 2;
                }

                this.moveBody(currentRow, this.Head.Column);
            }
            else if (direction == Direction.DIRECTION_DOWN)
            {
                int currentRow = this.Head.Row;
                this.Head.Row = currentRow + 1;

                if (this.Head.Row >= n - 1)
                {
                    this.Head.Row = 1;
                }

                this.moveBody(currentRow, this.Head.Column);
            }
        }
    }
}