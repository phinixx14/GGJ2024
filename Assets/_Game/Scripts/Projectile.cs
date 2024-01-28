using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Quaternion rotation;
    public SpriteRenderer sprite;
    public float speed = 1;
    public Collider2D col;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        this.sprite = GetComponentInChildren<SpriteRenderer>();
        this.col = GetComponentInChildren<Collider2D>();
        gm = GameManager.FindInstance();
        gm.OnPlayerDeath += DestroySelf;
    }
    private void OnDestroy() {
        gm.OnPlayerDeath -= DestroySelf;
    }
    // Update is called once per frame
    void Update() {
        sprite.transform.Rotate(Vector3.forward, 360f * Time.deltaTime);
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);

        ContactFilter2D filter = new ContactFilter2D();
        //filter.SetLayerMask(LayerMask.NameToLayer("Nose Targets"));
        List<Collider2D> collisions = new();
        col.OverlapCollider(filter, collisions);

        if (collisions.Count > 0) {
            int hits = 0;
            collisions.ForEach(col => {
                if (col.CompareTag("SadClown")) {
                    hits++;
                    Debug.Log("Nose Hit Clown");
                    col.transform.parent.GetComponentInChildren<SadClownBehavior>()?.AttachNose();
                    //col.gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(.5f, .2f, .2f);

                    //PrefabLibrary plib = PrefabLibrary.CreateInstance<PrefabLibrary>();
                    //PrefabLibrary.CreateInstance<PrefabLibrary>().HappyClown;
                    //col.transform.gameObject.
                }
            });

            if (hits > 0) {
                DestroySelf(this);
            }
        }
    }

    void DestroySelf(object sender) {
        if (this && this.gameObject != null) {
            Destroy(this.gameObject);
        }
    }
}
