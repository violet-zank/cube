# cube
一个有着推箱子求解方法的C#脚本代码

## 用到的方法
1.广度优先搜索思想(主要解决新帧的生成)
2.剪枝处理(主要负责解决帧重复与帧违规)
3.种子填充算法(主要解决人物移动范围问题)

## 数据存储
--- 推箱子元素数据化设计参考：http://sokoban.cn/xsb_lurd.php
场景数据
class solution
_map 地图数据
_preSolution 父地图
_boxSet 箱子坐标hashcode
