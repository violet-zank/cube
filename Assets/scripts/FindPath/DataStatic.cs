using System.Collections;
using System.Collections.Generic;

namespace Assets.scripts.FindPath
{
    internal class DataStatic
    {
        public static int zuo, shang;

        public static List<MyPoint> boxList = new List<MyPoint>(10);
        public static List<MyPoint> zhongdian = new List<MyPoint>(10);
        public static int peopleValue;
        public static char[][] map;
        public static int chang = 1, kuan = 1;
        public static int boxNum = 1;
        public static HashSet<MyPoint> failPoint = new HashSet<MyPoint>();
        public static HashSet<SimpleSitu> allSitu = new HashSet<SimpleSitu>();

        public static int pointToValue(MyPoint myPoint)
        {
            return myPoint.y * DataStatic.chang + myPoint.x;
        }
        public static int pointToValue(int y, int x)
        {
            return y * DataStatic.chang + x;
        }
        public static MyPoint valueToPoint(int value)
        {
            return new MyPoint(value % DataStatic.chang, value / DataStatic.chang);
        }
    }
}