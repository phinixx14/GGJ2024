using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    PlayerManager player;
    BoxCollider2D col;
    SpriteRenderer sprite;
    LevelScroller scroller;
    GameManager gm;

    // Start is called before the first frame update
    void Start() {
        player = GetComponentInParent<PlayerManager>();
        col = transform.parent.GetComponentInChildren<BoxCollider2D>();
        sprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
        gm = GameManager.FindInstance();

        if (!scroller) {
            scroller = FindObjectOfType<LevelScroller>();
        }
    }

    // Update is called once per frame
    void Update() {
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        //filter.SetLayerMask(LayerMask.NameToLayer("Nose Targets"));
        List<Collider2D> collisions = new();
        col.OverlapCollider(filter, collisions);

        if (collisions.Count > 0) {
            int hits = 0;
            collisions.ForEach(col => {
                if (!col.CompareTag("FriendlyProjectile")) {
                    if (col.CompareTag("Finish")) {
                        gm.TriggerReachedFinish();
                    }
                    else {
                        hits++;
                        Debug.Log("collider overlap");
                        col.gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(.5f, .2f, .2f);
                    }
                }
            });

            if (hits > 0) {
                sprite.color = new Color(.5f, .2f, .2f);
                gm.TriggerDeath();
            }
        }
    }
}
