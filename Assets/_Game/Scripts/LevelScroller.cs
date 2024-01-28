using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public Transform stage;
    public float ScrollSpeed;
    public PrefabLibrary prefabs;

    bool running = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            Vector3 dir = Quaternion.AngleAxis(-20, Vector3.forward) * Vector3.down;
            stage.Translate(dir * ScrollSpeed, Space.Self);
        }
    }

    public void Stop() {
        this.running = false;
    }
}
