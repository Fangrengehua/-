using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Cube[] GameBoards { get; set; }

    public int H = 10; //10列 横向
    public int V = 5; //5行 竖向

    public static Game instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameBoards = new Cube[H * V];
        float OffsetX = -9f;
        float OffsetY = 6.15f;
        for(int i = 0; i < V; i++)   //一行
            for(int j = 0; j < H; j++)  //一列
            {
                GameObject cube = Instantiate(Resources.Load("cube")) as GameObject;
                GameBoards[i * 10 + j] = cube.GetComponent<Cube>();
                cube.transform.position = new Vector3((j * 2)+ OffsetX,-i + OffsetY,0);
            }
    }

    public void FindNeigbourCubesUp(int i, List<Cube> list)
    {
        list.Add(GameBoards[i]);
        if(i>=0&&(i / H) < V)
        {
            if (GameBoards[i].color == GameBoards[i - H].color)
            {
                FindNeigbourCubesUp(i -= H, list); //向上
            }
            else if (GameBoards[i].color == GameBoards[i + H].color)
            {
                FindNeigbourCubesUp(i += H, list); //向下
            }
        }
        if (i >= 0 && (i % H) < H)
        {
            if (GameBoards[i].color == GameBoards[i + 1].color) //向右
            {
                FindNeigbourCubesUp(i++, list); 
            }
            else if (GameBoards[i].color == GameBoards[i - 1].color) //向右
            {
                FindNeigbourCubesUp(i--, list); 
            }
        }
            
        /*int up = i - Game.instance.H;
        if (up >= 0 )
        {
            if (GameBoards[up].color == GameBoards[i].color)
            {
                FindNeigbourCubesUp(up, list);
            }
        }*/
    }

    public void DestroyCubes(List<Cube> list)//
    {
        for(int i = list.Count-1; i < 0; i--)
        {
            GameObject.Destroy(list[i].gameObject);
            list[i] = null;
        }

        list.Clear();
       
    }


    // Update is called once per frame
    void Update()
    {

    }
}
