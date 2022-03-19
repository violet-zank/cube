using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts.FindPath
{
    internal class Situation//用来存储地图场景
    {
        private List<MyPoint> boxPoints = new List<MyPoint>(6);//最多6个箱子
        private MyPoint peoplePoint;
        private int fangxiang;//得到此场景需要移动箱子的方向
        private MyPoint movePoint;//得到此场景需要被移动的箱子的位置
        private Situation fatherSitu;
        private int arriveBoxNum = 0;
        private long myCrcNum;
        private char[][] map;
        private int changeNum;//此场景可以得到新场景的数量
        //private HashSet<MyPoint> fillPoint;//将处于同一填充的坐标放进hashset中

        public Situation(char[][] map, Situation fatherSitu, int fangxiang, MyPoint movePoint)//遍历数组，初始化人和箱子位置
        {
            long myCrc;
            StringBuilder zifuchuan = new StringBuilder();
            for (int i = 0; i < DataStatic.kuan; i++)
            {
                for (int j = 0; j < DataStatic.chang; j++)
                {
                    switch (map[i][j])
                    {
                        case '$'://箱子
                            boxPoints.Add(new MyPoint(j, i));
                            break;
                        case '*'://箱子在目标点
                            boxPoints.Add(new MyPoint(j, i));
                            arriveBoxNum++;
                            break;
                        case '@'://人
                            peoplePoint = new MyPoint(j, i);
                            break;
                        case '+'://人在目标点
                            peoplePoint = new MyPoint(j, i);
                            break;
                    }
                }
            }
            this.map = map;
            //fillPoint = new HashSet<MyPoint>();
            boxPoints.Sort();//将箱子节点排序
            foreach (MyPoint myPoint in boxPoints)
            {//遍历箱子节点
                zifuchuan.Append(myPoint.y);
                zifuchuan.Append(myPoint.x);
            }
            //Debug.Log("Situation");
            //new BoundaryFilling().fill(map,ref fillPoint, peoplePoint);
            this.fatherSitu = fatherSitu;
            myCrc = zifuchuan.ToString().GetHashCode();
            this.myCrcNum = myCrc;
            this.fangxiang = fangxiang;
            this.movePoint = movePoint;
        }
        public bool allArrive()
        {//判断是否全都归位
            return arriveBoxNum == DataStatic.boxNum;
        }

        public bool anyliseSitu()
        {
            //return true;//DeBug
         //重复返回false，不重复返回true
         //遍历全部节点，判断当前节点是否和已存在节点相同
            HashSet<SimpleSitu> allSitu = DataStatic.allSitu;
            if (allSitu.Contains(new SimpleSitu( peoplePoint, myCrcNum)))
            {//如果走过的节点包括这个节点，返回false
                return false;
            }
            int[] num = new int[3];//0为正方形中箱子+墙壁的总个数，1为正方形中箱子的个数，2为箱子在终点的个数，
            if (DataStatic.boxNum >= 2)
            {//判断箱子构成死锁情况
                for (int i = 0; i < boxPoints.Count() - 1; i++)
                {//对每一个箱子都进行分析(箱子已经排过序),是否四周能构成正方形
                    MyPoint boxPoint = boxPoints[i];
                    //判断之字形死锁
                    if (boxPoint.y == boxPoints[i].y && boxPoint.x == boxPoints[i].x + 1)
                    {//处于一行且相近
                        if (map[boxPoint.y - 1][boxPoint.x] == '#' && map[boxPoint.y + 1][boxPoint.x + 1] == '#')
                        {
                            return false;
                        }
                    }

                    //处于正方形左上角
                    clearNum(num, boxPoint.x, boxPoint.y);
                    ChangeNum(num, boxPoint.x + 1, boxPoint.y);//you
                    ChangeNum(num, boxPoint.x, boxPoint.y + 1);//xia
                    ChangeNum(num, boxPoint.x + 1, boxPoint.y + 1);//xia you
                    if (num[0] == 4 && num[1] != num[2])
                    {
                        return false;
                    }
                    //处于正方形右上角
                    clearNum(num, boxPoint.x, boxPoint.y);
                    ChangeNum(num, boxPoint.x - 1, boxPoint.y);//zuo
                    ChangeNum(num, boxPoint.x, boxPoint.y + 1);//xia
                    ChangeNum(num, boxPoint.x - 1, boxPoint.y + 1);//xia zuo
                    if (num[0] == 4 && num[1] != num[2])
                    {
                        return false;
                    }
                    //处于正方形左下角
                    clearNum(num, boxPoint.x, boxPoint.y);
                    ChangeNum(num, boxPoint.x + 1, boxPoint.y);//you
                    ChangeNum(num, boxPoint.x, boxPoint.y - 1);//shang
                    ChangeNum(num, boxPoint.x + 1, boxPoint.y - 1);//shang you
                    if (num[0] == 4 && num[1] != num[2])
                    {
                        return false;
                    }
                    //处于正方形右下角
                    clearNum(num, boxPoint.x, boxPoint.y);
                    ChangeNum(num, boxPoint.x - 1, boxPoint.y);//zuo
                    ChangeNum(num, boxPoint.x, boxPoint.y - 1);//shang
                    ChangeNum(num, boxPoint.x - 1, boxPoint.y - 1);//shang zuo
                    if (num[0] == 4 && num[1] != num[2])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private void ChangeNum(int[] num, int x, int y)
        {
            switch (map[y][x])
            {
                case '$':
                    num[0]++;
                    num[1]++;
                    break;
                case '*':
                    num[0]++;
                    num[1]++;
                    num[2]++;
                    break;
                case '#':
                    num[0]++;
                    break;
            }
        }
        private void clearNum(int[] num, int x, int y)
        {
            num[0] = 1;
            num[1] = 1;
            if (map[y][x] == '$')
            {//如果不在终点
                num[2] = 0;
            }
            else
                num[2] = 1;
        }
    public override int GetHashCode()
        {
            int result = 17;
            foreach (MyPoint boxPoint in boxPoints)
            {
                result = result * 31 + boxPoint.x;
                result = result * 31 + boxPoint.y;
            }
            return result;
        }

    public override bool Equals(System.Object obj)
        {
            Situation nowSitu = (Situation)obj;
            if (nowSitu.getHash() != myCrcNum)
            {
                return false;
            }
            List<MyPoint> nowBoxPoint = nowSitu.getBoxPoints();
            MyPoint boxPoint1, boxPoint2;
            //遍历box数组
            for (int i = 0; i < boxPoints.Count(); i++)
            {
                boxPoint1 = boxPoints[i];
                boxPoint2 = nowBoxPoint[i];
                if (boxPoint1.y != boxPoint2.y || boxPoint1.x != boxPoint2.x)
                {
                    return false;
                }
            }
            //查看人是否相通
            return true;
        }

        public long getHash()
        {//获取当前场景的hash值，用来判断是否包含和重复
            return this.myCrcNum;
        }
        public Situation getFatherSitu()
        {
            return this.fatherSitu;
        }
        public MyPoint getPeoplePoint()
        {
            return this.peoplePoint;
        }

        public List<MyPoint> getBoxPoints()
        {
            return boxPoints;
        }

        //public HashSet<MyPoint> getFillPoint()
        //{
        //    return fillPoint;
        //}

        public char[][] getMap()
        {
            return map;
        }

        public int getChangeNum()
        {
            return changeNum;
        }

        public void setChangeNum(int changeNum)
        {
            this.changeNum = changeNum;
        }

        public int getFangxiang()
        {
            return fangxiang;
        }

        public MyPoint getMovePoint()
        {
            return movePoint;
        }

    }
}
