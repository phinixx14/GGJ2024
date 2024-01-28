using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBarbell : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;  

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
//        spriteRenderer.sprite
    }
}
