using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadClownBehavior : MonoBehaviour
{
    public Behavior[] behaviors;
    public GameObject HappyClown;
    public GameManager gm;
    // Start is called before the first frame update
    void Start() {
        gm = GameManager.FindInstance();
    }
    // Update is called once per frame
    void Update() {
        Behavior currentBehavior = transform.parent.gameObject.GetComponentInChildren<Behavior>();
        if (currentBehavior == null) {
            AttachRandomBehavior();
        }
    }
    void AttachRandomBehavior() {
        int index = Mathf.FloorToInt(Random.value * behaviors.Length);
        Behavior b = behaviors[index];
        Behavior instantiated = GameObject.Instantiate<Behavior>(b, transform.parent);
    }

    public void AttachNose() {
        gm.SoundManager.PlaySFX(gm.SoundManager.NoseHitSFX);
        Instantiate(HappyClown, transform.parent.position, transform.parent.rotation, transform.parent.parent);
        Destroy(transform.parent.gameObject);
    }
}
