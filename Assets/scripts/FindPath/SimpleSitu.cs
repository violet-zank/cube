using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.FindPath
{
    internal class SimpleSitu
    {
        private long myCrcNum;
        private MyPoint peoplePoint;
        private HashSet<MyPoint> fillPoint;//将处于同一填充的坐标放进hashset中
        public SimpleSitu(HashSet<MyPoint> fillPoint, MyPoint peoplePoint, long crc)
        {
            this.fillPoint = fillPoint;
            this.myCrcNum = crc;
            this.peoplePoint = peoplePoint;
        }
        public SimpleSitu( MyPoint peoplePoint, long crc)
        {
            this.myCrcNum = crc;
            this.peoplePoint = peoplePoint;
        }

        public long getMyCrcNum()
        {
            return myCrcNum;
        }

        public MyPoint getPeoplePoint()
        {
            return peoplePoint;
        }
        public override int GetHashCode()
        {
            return myCrcNum.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            SimpleSitu situ = (SimpleSitu)obj;
            if (!situ.getMyCrcNum().Equals(myCrcNum))
            {
                return false;
            }
            else return true;
        }
    }
}
