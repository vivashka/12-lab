using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12_3
{
    public class Point<T> where T: IComparable
    {
        public T? Data { get; set; }

        public sbyte Height { get; set; }
        
        public Point<T> Left { get; set; }
        
        public Point<T> Right { get; set; }

        public Point()
        {
            Data = default(T);
            Left = null;
            Right = null;
        }

        public Point(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public override string ToString()
        {
            return Data == null ? "" : Data.ToString();
        }

        public int CompareTo(Point<T> other)
        {
            return Data.CompareTo(other.Data);
        }
    }
}
