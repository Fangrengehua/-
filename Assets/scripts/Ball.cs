using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float xx = 2f;
    public float yy = 1.5f;
    float x;
    float y;
    //小球运动状态
    bool run = false;
    //public static Ball instance;
    //计分
    public int Score = 0;
    public Text ScoreText;

    //游戏时间，按秒计算，如120秒，就是2：00;
    public float CountDownTime;
    private float GameTime;
    private float timer = 0;
    public Text GameCountTimeText;

    void Start()
    {
        GameTime = CountDownTime;
        GameCountTimeText.text = GameTime + "s";
        x = Random.Range(-2f, xx);
        y = Random.Range(0.5f, yy);
    }

    // Update is called once per frame
    void Update()
    {
        //边界碰撞
        CollideWithScreen();
        //挡板碰撞
        Platform platform = FindObjectOfType<Platform>();
        if(platform.CollideWith(this))
        {
            y = -y;
        }

        Cube[] cubes = Game.instance.GameBoards;
        for(int i =0; i < cubes.Length;i++)
        {
            if (cubes[i] != null && CollideWith(cubes[i]))
            {
                /*List<Cube> neigbourCubes = new List<Cube>();
                Game.instance.FindNeigbourCubesUp(i, neigbourCubes);
                Game.instance.DestroyCubes(neigbourCubes);*/
                
                Destroy(cubes[i].gameObject);
                Score++;
                ScoreText.text = Score.ToString();
                //Debug.Log(Score);

                Debug.Log("Cubes.length:" + cubes.Length);
            }
            
        }
        if (Score == cubes.Length)
        {
            GameOver();
        }

        if (run == true)
        {
            TimeDown();
            transform.Translate(x * Time.deltaTime, y * Time.deltaTime, 0);
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            run = !run;
        }
    }

    void CollideWithScreen()
    {
        if (transform.position.y > 6)
        {
            y = -y;
        }

        if (transform.position.x < -10 || transform.position.x > 10)
        {
            x = -x;
        }
        if (transform.position.y < -3) GameOver();
    }

    void TimeDown()
    {
        int M = (int)(GameTime / 60);
        float S = GameTime % 60;
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0;
            GameTime--;
            GameCountTimeText.text = M + "：" + string.Format("{0:00}", S);
        }
        if (M <= 0 && S <= 0f)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        run = !run;
        GameCountTimeText.text = "游戏结束";
        Application.Quit();
    }
    bool CollideWith(Cube cube)
    {
        SpriteRenderer ball = GetComponent<SpriteRenderer>();
        Bounds ballBounds = ball.bounds;

        SpriteRenderer cubeSprite = cube.GetComponent<SpriteRenderer>();
        Bounds cubeBounds = cubeSprite.bounds;

        return cubeBounds.Intersects(ballBounds);
    }

    
}
