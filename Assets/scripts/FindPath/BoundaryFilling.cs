using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.FindPath
{
    internal class BoundaryFilling
    {
        public void fill(char[][] map,ref HashSet<MyPoint> fillPoint, MyPoint peoplePoint)
        {//扫描线种子填充法
            Stack<MyPoint> stack = new Stack<MyPoint>();
            int leftX, rightX;
            stack.Push(peoplePoint);
            Debug.Log(peoplePoint.x + "AND" + peoplePoint.y);
            bool ifFistPoint;
            while (stack.Count!=0)
            {//当栈为非空时
                MyPoint nowPoint = stack.Peek();
                stack.Pop();//a. 栈顶像素出栈
                Debug.Log(nowPoint.x + "AND" + nowPoint.y);
                leftX = nowPoint.x - 1;
                rightX = nowPoint.x + 1;
                fillPoint.Add(nowPoint);//将当前点填充
                while (map[nowPoint.y][leftX] == '-' || map[nowPoint.y][leftX] == '.')
                {//向左检测是空的填充点
                    fillPoint.Add(new MyPoint(leftX--, nowPoint.y));
                }
                while (map[nowPoint.y][rightX] == '-' || map[nowPoint.y][rightX] == '.')
                {//向右检测直到遇到边界
                    fillPoint.Add(new MyPoint(rightX++, nowPoint.y));
                }
                //在区间[xLeft,xRight]内检查与当前扫描线相邻的上下两条扫描线是否全为边界像素或已填充的像素
                ifFistPoint = true;
                for (int i = rightX - 1; i > leftX; i--)
                {//向上
                    if ((map[nowPoint.y - 1][i] == '-' || map[nowPoint.y - 1][i] == '.') && !fillPoint.Contains(new MyPoint(i, nowPoint.y - 1)))
                    {
                        if (ifFistPoint)
                        {
                            ifFistPoint = false;
                            stack.Push(new MyPoint(i, nowPoint.y - 1));
                        }
                    }
                    else
                    {
                        ifFistPoint = true;
                    }
                }
                ifFistPoint = true;
                for (int i = rightX - 1; i > leftX; i--)
                {//向下
                    if ((map[nowPoint.y + 1][i] == '-' || map[nowPoint.y + 1][i] == '.') && !fillPoint.Contains(new MyPoint(i, nowPoint.y + 1)))
                    {
                        if (ifFistPoint)
                        {
                            ifFistPoint = false;
                            stack.Push(new MyPoint(i, nowPoint.y + 1));//加入堆栈
                        }
                    }
                    else
                    {
                        ifFistPoint = true;
                    }
                }
            }
        }
    }
}