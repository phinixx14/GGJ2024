using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meander :  Behavior
{
    Transform tform;
    float speed = 1;
    Vector2 dir;
    float dist;
    Vector2 start;
    int step;
    void Start() {
        this.tform = transform.parent;
        this.start = tform.position;
        step = 0;

        Collider2D col = tform.gameObject.GetComponentInChildren<Collider2D>();
        RaycastHit2D[] hits = new RaycastHit2D[0];

        do {
            dist = PickDistance();
            dir = PickDirection();
            Vector3 p = col.transform.parent.position;
            Debug.DrawLine(p, ((dir + (Vector2)p)* dist), Color.red, 2f);
            col.Raycast(dir, hits, dist);
            
            if (hits.Length > 0) {
                Debug.Log("Meander points at obstacle. retrying.");
            }
        } while (hits.Length > 0);
        
    }
    private void Update() {
        step++;
        Act();
    }
    Vector2 PickDirection() {
        return Quaternion.AngleAxis(Random.Range(1, 360), Vector3.forward) * Vector2.up;
    }
    float PickDistance() {
        return Random.Range(2f, 5f);
    }
    public override void Act() {
        Vector2 target = speed * Time.deltaTime * dir;

        if (Vector2.Distance((Vector2)tform.position, this.start) < dist) {
            WalkTowards(target);
        }
        else {
            GameObject.Destroy(gameObject);
        }
    }

    void WalkTowards(Vector2 d) {
        tform.Translate(d);
    }
}
