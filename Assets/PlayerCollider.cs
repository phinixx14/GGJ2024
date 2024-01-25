using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    BoxCollider2D col;
    LevelScroller scroller;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        if (!scroller) {
            scroller = FindObjectOfType<LevelScroller>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<Collider2D> collisions = new();
        col.OverlapCollider(new ContactFilter2D().NoFilter(), collisions);
        if (collisions.Count > 0) {
            scroller.Stop();
            collisions[0].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("trigger enter");
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("collision enter");
        GameObject.Find("Stage Scroller").GetComponent<LevelScroller>().Stop();
        collision.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
