using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.FindPath
{
    internal class MyPoint:IComparable<MyPoint>
    {
        public int x , y;
        public MyPoint(int x, int y)
        {
            this.x = x;
            this.y = y; 
        }

        public int CompareTo(MyPoint other)
        {
            if(this.y > other.y)
            {
                return 1;
            }
            else if(this.y < other.y)
            {
                return -1;
            }
            else
            {
                if(this.x >= other.x)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
        public override bool Equals(Object obj)
        {
            MyPoint myPoint = obj as MyPoint;
            if(myPoint == null)
            {
                return this.x ==myPoint.x && this.y ==myPoint.y;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            int result = 17;
            result = result * 31 + x;
            result = result * 31 + y;
            return result;
        }

        public override string ToString()
        {
            return '(' + x.ToString() + ',' + y.ToString() + ')';
        }
    }
}
