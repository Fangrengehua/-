using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Color[] colors;

    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, colors.Length);
        GetComponent<SpriteRenderer>().color = colors[r];
        color = colors[r];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
