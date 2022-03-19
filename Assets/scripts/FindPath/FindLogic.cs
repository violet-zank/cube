using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.scripts.FindPath
{
    internal class FindLogic
    {
        //使用Deque作为队列
        private Queue<Situation> situationDeque = new Queue<Situation>(5000);
        private HashSet<SimpleSitu> failSitu = new HashSet<SimpleSitu>();
        public Situation findPath(char[][] map)
        {//如果找到就返回末尾节点的Situation，找不到返回null
            situationDeque.Enqueue(new Situation(map, null, -1, null));//将第一个场景加入队列尾部
            Situation nowSituation  = situationDeque.Dequeue();//将队首取出初始化
            //findFailPoint(map);
            while (true)
            {
                System.Diagnostics.Debug.Assert(nowSituation != null);
                if (nowSituation.allArrive())
                {//所有箱子到达终点
                    return nowSituation;
                }
                //while (nowSituation != null && failSitu.Contains(new SimpleSitu( nowSituation.getPeoplePoint(), nowSituation.getHash())))
                //{//如果在失败队列
                //    nowSituation = situationDeque.Dequeue();
                //}
                if (nowSituation == null)
                {
                    return null;
                }
                moveBox(nowSituation.getMap(), nowSituation);
                //moveBoxtest(nowSituation.getMap(), nowSituation);
                if (situationDeque.Count() == 0 )
                {//队列场景数量为零，场景无解
                    Debug.Log("无解!");
                    return null;
                }
                if(situationDeque.Count() > 5000) {
                    Debug.Log("超出求解能力范围!");
                    return null;
                }

                nowSituation = situationDeque.Dequeue();//将队首取出
            }
        }
        private void moveBoxtest(char[][] map, Situation nowSitu)
        {
            foreach(MyPoint point in nowSitu.getBoxPoints())
            {
                //Up
                
                //Down

                //Left

                //Right
            }
        }
        private void moveBox(char[][] map, Situation nowSitu)
        {
            int changeNum = 0;
            //HashSet<MyPoint> fillPoint = nowSitu.getFillPoint();
            HashSet<MyPoint> failPoint = DataStatic.failPoint;
            HashSet<SimpleSitu> allSitu = DataStatic.allSitu;
            //遍历box节点
            MyPoint peoplePoint = nowSitu.getPeoplePoint();
            foreach (MyPoint boxPoint in nowSitu.getBoxPoints())
            {
                //箱子向上走,即 人可以到达目前箱子的下面，且目前箱子的上面不是箱子和墙壁
                if ((map[boxPoint.y - 1][boxPoint.x] == '-' || map[boxPoint.y - 1][boxPoint.x] == '@'
                        || map[boxPoint.y - 1][boxPoint.x] == '+' || map[boxPoint.y - 1][boxPoint.x] == '.'))
                {
                    char[][] newMap = new char[DataStatic.kuan][];
                    for (int i = 0; i < DataStatic.kuan; i++)
                    {
                        newMap[i] =  new char[DataStatic.chang];
                        Array.Copy(map[i], 0, newMap[i], 0, DataStatic.chang);
                    }
                    //改变地图
                    if (map[peoplePoint.y][peoplePoint.x] == '+')
                    {//如果人在终点
                        newMap[peoplePoint.y][peoplePoint.x] = '.';
                    }
                    else
                    {
                        newMap[peoplePoint.y][peoplePoint.x] = '-';
                    }
                    if (map[boxPoint.y - 1][boxPoint.x] == '.' || map[boxPoint.y - 1][boxPoint.x] == '+')
                    {//如果上面是终点
                        if (map[boxPoint.y][boxPoint.x] == '*')
                        {//如果当前箱子在终点
                            newMap[boxPoint.y][boxPoint.x] = '+';
                            newMap[boxPoint.y - 1][boxPoint.x] = '*';
                        }
                        else
                        {
                            newMap[boxPoint.y][boxPoint.x] = '@';
                            newMap[boxPoint.y - 1][boxPoint.x] = '*';
                        }
                    }
                    else
                    {//如果上面不是终点
                        if (map[boxPoint.y][boxPoint.x] == '*')
                        {//如果当前箱子在终点
                            newMap[boxPoint.y][boxPoint.x] = '+';
                            newMap[boxPoint.y - 1][boxPoint.x] = '$';
                        }
                        else
                        {
                            newMap[boxPoint.y][boxPoint.x] = '@';
                            newMap[boxPoint.y - 1][boxPoint.x] = '$';
                        }

                    }
                    Situation newSitu = new Situation(newMap, nowSitu, 3, boxPoint);
                    if (newSitu.anyliseSitu())
                    {//如果不重复
                        situationDeque.Enqueue(newSitu);//上
                        allSitu.Add(new SimpleSitu( newSitu.getPeoplePoint(), newSitu.getHash()));
                        changeNum++;
                    }
                }
                //箱子向下走,即 人可以到达目前箱子的上面，且目前箱子的下面不是箱子和墙壁,是人，空地，人到达终点
                if ((map[boxPoint.y + 1][boxPoint.x] == '-' || map[boxPoint.y + 1][boxPoint.x] == '@'
                        || map[boxPoint.y + 1][boxPoint.x] == '+' || map[boxPoint.y + 1][boxPoint.x] == '.'))
                {
                    //复制地图
                    char[][] newMap = new char[DataStatic.kuan][];
                    for (int i = 0; i < DataStatic.kuan; i++)
                    {
                        newMap[i] = new char[DataStatic.chang];
                        Array.Copy(map[i], 0, newMap[i], 0, DataStatic.chang);
                    }
                    //改变地图
                    if (map[peoplePoint.y][peoplePoint.x] == '+')
                    {//如果人在终点
                        newMap[peoplePoint.y][peoplePoint.x] = '.';
                    }
                    else
                    {
                        newMap[peoplePoint.y][peoplePoint.x] = '-';
                    }
                    if (map[boxPoint.y + 1][boxPoint.x] == '.' || map[boxPoint.y - 1][boxPoint.x] == '+')
                    {//如果下面是终点
                        if (map[boxPoint.y][boxPoint.x] == '*')
                        {//如果当前箱子在终点
                            newMap[boxPoint.y][boxPoint.x] = '+';
                            newMap[boxPoint.y + 1][boxPoint.x] = '*';
                        }
                        else
                        {
                            newMap[boxPoint.y][boxPoint.x] = '@';
                            newMap[boxPoint.y + 1][boxPoint.x] = '*';
                        }
                    }
                    else
                    {//如果上面不是终点
                        if (map[boxPoint.y][boxPoint.x] == '*')
                        {//如果当前箱子在终点
                            newMap[boxPoint.y][boxPoint.x] = '+';
                            newMap[boxPoint.y + 1][boxPoint.x] = '$';
                        }
                        else
                        {
                            newMap[boxPoint.y][boxPoint.x] = '@';
                            newMap[boxPoint.y + 1][boxPoint.x] = '$';
                        }

                    }
                    Situation newSitu = new Situation(newMap, nowSitu, 0, boxPoint);
                    if (newSitu.anyliseSitu())
                    {//如果不重复
                        situationDeque.Enqueue(newSitu);//xia
                        allSitu.Add(new SimpleSitu( newSitu.getPeoplePoint(), newSitu.getHash()));
                        changeNum++;
                    }
                }
                //箱子向右走,即 人可以到达目前箱子的左面，且目前箱子的右面不是箱子和墙壁
                if ((map[boxPoint.y][boxPoint.x + 1] == '-' || map[boxPoint.y][boxPoint.x + 1] == '@'
                        || map[boxPoint.y][boxPoint.x + 1] == '+' || map[boxPoint.y][boxPoint.x + 1] == '.') )
                {
                    //复制地图
                    char[][] newMap = new char[DataStatic.kuan][];
                    for (int i = 0; i < DataStatic.kuan; i++)
                    {
                        newMap[i] = new char[DataStatic.chang];
                        Array.Copy(map[i], 0, newMap[i], 0, DataStatic.chang);
                    }
                    //改变地图
                    if (map[peoplePoint.y][peoplePoint.x] == '+')
                    {//如果人在终点
                        newMap[peoplePoint.y][peoplePoint.x] = '.';
                    }
                    else
                    {
                        newMap[peoplePoint.y][peoplePoint.x] = '-';
                    }
                    if (map[boxPoint.y][boxPoint.x + 1] == '.' || map[boxPoint.y][boxPoint.x + 1] == '+')
                    {//如果you面是终点
                        if (map[boxPoint.y][boxPoint.x] == '*')
                        {//如果当前箱子在终点
                            newMap[boxPoint.y][boxPoint.x] = '+';
                            newMap[boxPoint.y][boxPoint.x + 1] = '*';
                        }
                        else
                        {
                            newMap[boxPoint.y][boxPoint.x] = '@';
                            newMap[boxPoint.y][boxPoint.x + 1] = '*';
                        }
                    }
                    else
                    {//如果you面不是终点
                        if (map[boxPoint.y][boxPoint.x] == '*')
                        {//如果当前箱子在终点
                            newMap[boxPoint.y][boxPoint.x] = '+';
                            newMap[boxPoint.y][boxPoint.x + 1] = '$';
                        }
                        else
                        {
                            newMap[boxPoint.y][boxPoint.x] = '@';
                            newMap[boxPoint.y][boxPoint.x + 1] = '$';
                        }

                    }
                    Situation newSitu = new Situation(newMap, nowSitu, 2, boxPoint);
                    if (newSitu.anyliseSitu())
                    {//如果不重复
                        situationDeque.Enqueue(newSitu);
                        allSitu.Add(new SimpleSitu( newSitu.getPeoplePoint(), newSitu.getHash()));
                        changeNum++;
                    }
                }
                //箱子向左走,即 人可以到达目前箱子的右面，且目前箱子的左面不是箱子和墙壁
                if ((map[boxPoint.y][boxPoint.x - 1] == '-' || map[boxPoint.y][boxPoint.x - 1] == '@'
                        || map[boxPoint.y][boxPoint.x - 1] == '+' || map[boxPoint.y][boxPoint.x - 1] == '.') )
                {
                    //复制地图
                    char[][] newMap = new char[DataStatic.kuan][];
                    for (int i = 0; i < DataStatic.kuan; i++)
                    {
                        newMap[i] = new char[DataStatic.chang];
                        Array.Copy(map[i], 0, newMap[i], 0, DataStatic.chang);
                    }
                    //改变地图
                    if (map[peoplePoint.y][peoplePoint.x] == '+')
                    {//如果人在终点
                        newMap[peoplePoint.y][peoplePoint.x] = '.';
                    }
                    else
                    {
                        newMap[peoplePoint.y][peoplePoint.x] = '-';
                    }
                    if (map[boxPoint.y][boxPoint.x - 1] == '.' || map[boxPoint.y][boxPoint.x - 1] == '+')
                    {//如果左面是终点
                        if (map[boxPoint.y][boxPoint.x] == '*')
                        {//如果当前箱子在终点
                            newMap[boxPoint.y][boxPoint.x] = '+';
                            newMap[boxPoint.y][boxPoint.x - 1] = '*';
                        }
                        else
                        {
                            newMap[boxPoint.y][boxPoint.x] = '@';
                            newMap[boxPoint.y][boxPoint.x - 1] = '*';
                        }
                    }
                    else
                    {//如果上面不是终点
                        if (map[boxPoint.y][boxPoint.x] == '*')
                        {//如果当前箱子在终点
                            newMap[boxPoint.y][boxPoint.x] = '+';
                            newMap[boxPoint.y][boxPoint.x - 1] = '$';
                        }
                        else
                        {
                            newMap[boxPoint.y][boxPoint.x] = '@';
                            newMap[boxPoint.y][boxPoint.x - 1] = '$';
                        }

                    }
                    Situation newSitu = new Situation(newMap, nowSitu, 1, boxPoint);
                    if (newSitu.anyliseSitu())
                    {//如果不重复
                        situationDeque.Enqueue(newSitu);//左
                        allSitu.Add(new SimpleSitu( newSitu.getPeoplePoint(), newSitu.getHash()));
                        changeNum++;
                    }
                }
            }
            if (changeNum == 0)
            {
                addFailSitu(nowSitu);
            }
        }

        private void findFailPoint(char[][] map)//将死角全部筛选，将边界上一整排没有终点的筛选
        {
            HashSet<MyPoint> failPoint = DataStatic.failPoint;
            HashSet<MyPoint> fillPoint = new HashSet<MyPoint>();
            char[][] newMap = new char[DataStatic.kuan][];
            for (int i = 0; i < DataStatic.kuan; i++)
            {
                newMap[i] = new char[DataStatic.chang];
            }
            MyPoint peoplePoint = null;
            //将箱子转化成空地
            for (int i = 0; i < DataStatic.kuan; i++)
            {
                for (int j = 0; j < DataStatic.chang; j++)
                {
                    switch (map[i][j])
                    {
                        case '@':
                            peoplePoint = new MyPoint(j, i);
                            newMap[i][j] = '-';
                            break;
                        case '+':
                            peoplePoint = new MyPoint(j, i);
                            newMap[i][j] = '.';
                            break;
                        case '.':
                        case '*':
                            newMap[i][j] = '.';
                            break;
                        case '#'://是墙还是墙
                            newMap[i][j] = '#';
                            break;
                        default:
                            newMap[i][j] = '-';//其他的转化成空地
                            break;
                    }
                }
            }
            bool shang = true, xia = true, zuo = true, you = true;
            //new BoundaryFilling().fill(newMap,ref fillPoint, peoplePoint);//针对这个地图箱子能到的地方
            for (int i = 1; i < DataStatic.kuan - 1; i++)
            {
                for (int j = 1; j < DataStatic.chang - 1; j++)
                {
                    if (i == 1)
                    {//上
                        if (newMap[i][j] == '.')
                        {
                            shang = false;
                        }
                    }
                    if (i == DataStatic.kuan - 2)
                    {
                        if (newMap[i][j] == '.')
                        {
                            xia = false;
                        }
                    }
                    if (j == 1)
                    {
                        if (newMap[i][j] == '.')
                        {
                            zuo = false;
                        }
                    }
                    if (j == DataStatic.chang - 2)
                    {
                        if (newMap[i][j] == '.')
                        {
                            you = false;
                        }
                    }
                    if (fillPoint.Contains(new MyPoint(j, i)) && newMap[i][j] != '.')
                    {//如果箱子能到，且这个点不是终点
                        if (map[i - 1][j] == '#' || map[i + 1][j] == '#')
                        {//上面或者下面是墙
                            if (map[i][j - 1] == '#' || map[i][j + 1] == '#')
                            {//左面或右面是墙
                                failPoint.Add(new MyPoint(j, i));
                            }
                        }
                    }
                }
            }
            //筛选边界
            //shang
            if (shang)
            {
                for (int j = 1; j < DataStatic.chang - 1; j++)
                {
                    MyPoint nowPoint = new MyPoint(j, 1);
                    if (fillPoint.Contains(nowPoint))
                        failPoint.Add(nowPoint);
                }
            }
            if (xia)
            {
                for (int j = 1; j < DataStatic.chang - 1; j++)
                {
                    MyPoint nowPoint = new MyPoint(j, DataStatic.kuan - 2);
                    if (fillPoint.Contains(nowPoint))
                        failPoint.Add(nowPoint);
                }
            }
            if (zuo)
            {
                for (int i = 1; i < DataStatic.kuan - 1; i++)
                {
                    MyPoint nowPoint = new MyPoint(1, i);
                    if (fillPoint.Contains(nowPoint))
                        failPoint.Add(nowPoint);
                }
            }
            if (you)
            {
                for (int i = 1; i < DataStatic.kuan - 1; i++)
                {
                    MyPoint nowPoint = new MyPoint(DataStatic.chang - 2, i);
                    if (fillPoint.Contains(nowPoint))
                        failPoint.Add(nowPoint);
                }
            }
        }

        private void addFailSitu(Situation nowSitu)
        {
            Situation thisSitu;
            failSitu.Add(new SimpleSitu( nowSitu.getPeoplePoint(), nowSitu.getHash()));
            thisSitu = nowSitu.getFatherSitu();
            while (thisSitu != null && thisSitu.getChangeNum() == 1)
            {
                failSitu.Add(new SimpleSitu(nowSitu.getPeoplePoint(), nowSitu.getHash()));
                thisSitu = thisSitu.getFatherSitu();
            }
            if (thisSitu != null)
            {
                thisSitu.setChangeNum(thisSitu.getChangeNum() - 1);
            }
        }
    }
}
