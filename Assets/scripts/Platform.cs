using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            if (transform.position.x <= -11f)
                transform.position = new Vector3(11f, transform.position.y, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            if (transform.position.x >= 11f)
                transform.position = new Vector3(-11f, transform.position.y, 0);
        }
    }

    public bool CollideWith(Ball ball)
    {
        SpriteRenderer platform = GetComponent<SpriteRenderer>();
        Bounds platformBounds = platform.bounds;

        SpriteRenderer ballSprite = ball.GetComponent<SpriteRenderer>();
        Bounds ballBounds = ballSprite.bounds;

        return platformBounds.Intersects(ballBounds);
    }
}
