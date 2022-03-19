using System;
using System.Collections;
using UnityEngine;

namespace Assets.scripts.FindPath
{
    public class Find : MonoBehaviour
    {
        private Situation finSitu;
        public char[][] map;
        void Start()
        {
            Debug.Log("开始！");
            testMapSlove2();
        }

        void testMapSlove()
        {
            map = new char[9][];
            map[0] = new char[] { '#', '#', '#', '#', '#', '-', '-', '-', '-' };
            map[1] = new char[] { '#', '-', '-', '@', '#', '-', '-', '-', '-' };
            map[2] = new char[] { '#', '-', '$', '$', '#', '-', '#', '#', '#' };
            map[3] = new char[] { '#', '-', '$', '-', '#', '-', '#', '.', '#' };
            map[4] = new char[] { '#', '#', '#', '-', '#', '#', '#', '.', '#' };
            map[5] = new char[] { '-', '#', '#', '-', '-', '-', '-', '.', '#' };
            map[6] = new char[] { '-', '#', '-', '-', '-', '#', '-', '-', '#' };
            map[7] = new char[] { '-', '#', '-', '-', '-', '#', '#', '#', '#' };
            map[8] = new char[] { '-', '#', '#', '#', '#', '#', '-', '-', '-' };
            DataStatic.chang = 9;
            DataStatic.kuan = 9;
            DataStatic.boxNum = 3;
            FindLogic slove = new FindLogic();
            finSitu = slove.findPath(map);

            if (finSitu == null) Debug.Log("未找到解!");
            else Debug.Log("找到解了!");
            //输出解
            string output = "";
            while (finSitu != null)
            {
                output += finSitu.getPeoplePoint().ToString() + finSitu.getFangxiang().ToString() + "\n";
                finSitu = finSitu.getFatherSitu();
            }
            Debug.Log(output);
        }
        void testMapSlove2()
        {
            map = new char[7][];
            map[0] = new char[] { '#', '#', '#', '#', '#', '#', '#' };
            map[1] = new char[] { '#', '#', '-', '-', '#', '#', '#' };
            map[2] = new char[] { '#', '.', '-', '@', '$', '-', '#' };
            map[3] = new char[] { '#', '-', '$', '#', '$', '-', '#' };
            map[4] = new char[] { '#', '-', '-', '-', '-', '-', '#' };
            map[5] = new char[] { '#', '#', '.', '#', '.', '#', '#' };
            map[6] = new char[] { '#', '#', '#', '#', '#', '#', '#' };
            DataStatic.chang = 7;
            DataStatic.kuan = 7;
            DataStatic.boxNum = 3;
            FindLogic slove = new FindLogic();
            finSitu = slove.findPath(map);

            if (finSitu == null) Debug.Log("未找到解!");
            else Debug.Log("找到解了!");
            //输出解
            //输出解
            string output = "";
            while (finSitu != null)
            {
                output += finSitu.getPeoplePoint().ToString() + finSitu.getFangxiang().ToString() + "\n";
                finSitu = finSitu.getFatherSitu();
            }
            Debug.Log(output);
        }

        //      3
        //  1       2
        //      0
        void testMapSlove3()
        {
            map = new char[5][];
            map[0] = new char[] { '#', '#', '#', '#', '#' };
            map[1] = new char[] { '#', '-', '-', '-', '#' };
            map[2] = new char[] { '#', '.', '$', '@', '#' };
            map[3] = new char[] { '#', '-', '-', '-', '#' };
            map[4] = new char[] { '#', '#', '#', '#', '#' };
            DataStatic.chang = 5;
            DataStatic.kuan = 5;
            DataStatic.boxNum = 1;
            Debug.Log("初始化!");
            FindLogic slove = new FindLogic();
            finSitu = slove.findPath(map);

            if (finSitu == null) Debug.Log("未找到解!");
            else Debug.Log("找到解了!");
            //输出解
            string output = "";
            while(finSitu != null)
            {
                output += finSitu.getPeoplePoint().ToString() + finSitu.getFangxiang().ToString() + "\n";
                finSitu = finSitu.getFatherSitu();
            }
            Debug.Log(output);
        }
    }
}