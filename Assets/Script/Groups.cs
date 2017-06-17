using UnityEngine;
using System.Collections;

public class Groups : MonoBehaviour
{
    #region 全局变量

    //落下的时间
    float lastFall =0;

    #endregion

    void Start()
    {
        //如果网格无效则游戏结束  
        if (!isValidGridPos())
        {
            GameObject.Find("Canvas").GetComponent<GUIMange>().GameOver();//找到画布组件
            Destroy(gameObject); //删除游戏
        }
    }

    void Update()
    {

        //当按下左键时
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            //使当前的变形格向左移动一格
            transform.position += new Vector3(-1, 0, 0);

            //如果下一次格子可以移动的话,就更新当前格子的位置.
            if (isValidGridPos())
            {
                UpdateGrid();
            }
            //如果下一次格子无效的话,就让当前格子再退回上一次的位置
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        //当按下右键时
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (isValidGridPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        //当按下上键时
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //使当前变形格子旋转90度
            transform.Rotate(0, 0, -90);
            if (isValidGridPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }

        //当按下下键按钮时
        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            //变形格子下落1个单位
            transform.position += new Vector3(0, -1, 0);

            if (isValidGridPos())
            {
                UpdateGrid();
            }
            else
            {

                transform.position += new Vector3(0, 1, 0);

                //已经到底,测试所有列是否有满一行的格子,如果有就删除已经填满的行
                Grid.DeleteFullRow();

                //找到builder类里的创建方法,继续创建新的变形格子往下落
                FindObjectOfType<Builder>().SpawnNext();

                //如果变形方块已经到位了,那么当前这个变形方块身上的脚本就取消其作用,不再执行,下一个变形方块会继续挂载新的脚本继续游戏
                 enabled = false;

            }

            //把时间赋值给时间的变量,使变形方块1秒钟往下掉一次.
            lastFall = Time.time;

        }
    }
    
    //返回一个BOOL 判断变形格子是否在有效的游戏界面范围内
    bool isValidGridPos()
    {
        foreach (Transform child in transform)//获得一个变形方块身上的每一个小方块的位置
        {
            Vector2 v = Grid.roundVec2(child.position); //得到小方块的2D位置

            //判断是否在边界之内(左,右,下方)
            if (!Grid.inSideBorder(v))
            {
                return false;
            }

            //2.如果在下落的过程中,下一次下落的格子不为NULL的话,那么就返回false.
            if (Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }
        }
        //如果没有意外,就返回TRUE,让格子继续往下落
        return true;
    }

    //上一次的数据清理,移去原来占据的格子信息
    void UpdateGrid()
    {
        for (int y=0;y<Grid.h;y++)
        {
            for (int x=0;x<Grid.w;x++)
            {
                if (Grid.grid[x, y] != null)
                {
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;
                }
            }
        }

        //加入本次更新的位置信息
       foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position );
            Grid.grid [(int)v.x, (int)v.y] = child;
        }
    }
}
