using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class Point<T>
    {
        public T? Data { get; set; }
        public Point<T> Prev { get; set; }
        public Point<T> Next { get; set; }

        public Point()
        {
            Data = default(T);
            Prev = null;
            Next = null;
        }

        public Point(T data)
        {
            Data = data;
            Prev = null;
            Next = null;
        }

        public override string ToString()
        {
            return Data == null ? "": Data.ToString();
        }
    }
}
