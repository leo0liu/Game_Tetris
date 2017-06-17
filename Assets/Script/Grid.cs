
using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    //整个游戏界面的长和宽,一共200个格子的空间
    public static int w = 10;
    public static int h = 20;
    
    //数据结构 (200个格子的空间数组)
    public static Transform[,] grid = new Transform[w, h];

    //得到一个四舍五入的整数X和Y轴的值.
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),Mathf.Round(v.y));//Math.Round如果数字末尾是.5，不管是偶数或是奇数，将返回偶数(大于5当然还是进一位,不管奇数或偶数),大于5的还是进一位,少于5的还是不进。
    }

    //保证每个被检查的位置不超过左边框,不超过右边框,不低于最底部.
    public static bool inSideBorder(Vector2 pos)
    {
       return ((int)pos.x>=0&&(int)pos.x<w && (int)pos.y>=0);
    }

    //检查某一行的每一个单元是否为空,如果有一个是空的,那么该行还没有满.
    public static bool isRowFull(int y)
    {  
        for (int x=0;x<w;++x)
        {
            if (grid[x,y]==null)
            {
                return false;  
            }
        }
        return true;
    }



    //清除一行后,把上面一行整掉下来填补上
    public static void DecreseRow(int y)
    {  
        for (int x=0; x<w ; ++x) 
        {
            if (grid[x,y]!=null)
            {
                //1.复制该行的数据到下一行
                grid[x, y - 1] = grid[x, y];

                //2.清空该行数据
                grid[x, y] = null;
                
                //3.视觉上的,改变原来的方块的位置(Y+(-1))到下一行
                grid[x, y-1].position += new Vector3(0,-1,0);
            }
        }
    }

    //从指定的行数开始检查该行和该行以上的位置,把上面的数据搬到下面
    public static void DecreaseRowAbove(int y)
    { 
        for (int i=y ; i<h ; i++)
        {
            DecreseRow(i);
        }
    }

    #region  //删除所有已经填满的格子(2个方法)
    // 删除某一行的所有数据
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    //删除所有已经填满的行
    public static void DeleteFullRow()
    {
        for (int y=0;y<h;y++)
        {
            if (isRowFull(y))
            {
                DeleteRow(y);

                DecreaseRowAbove(y+1);
            }
        }
    }
    #endregion  
}
