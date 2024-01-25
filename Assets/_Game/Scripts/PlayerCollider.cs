using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    PlayerManager player;
    BoxCollider2D col;
    SpriteRenderer sprite;
    LevelScroller scroller;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerManager>();
        col = transform.parent.GetComponentInChildren<BoxCollider2D>();
        sprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
        
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
            Debug.Log("collider overlap");
            scroller.Stop();
            sprite.color = Color.red;
            collisions.ForEach(c => c.gameObject.GetComponent<SpriteRenderer>().color = Color.red);
            player.OnPlayerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("trigger enter");
    }
}
