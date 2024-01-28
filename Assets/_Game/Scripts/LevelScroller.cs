using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public Transform stage;
    public float ScrollSpeed;
    public PrefabLibrary prefabs;
    GameManager gm;
    bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.FindInstance();
        gm.OnPlayerDeath += Stop;
        gm.OnPause += Stop;
        gm.OnUnpause += Resume;
    }
    private void OnDestroy() {
        gm.OnPlayerDeath -= Stop;
        gm.OnPause -= Stop;
        gm.OnUnpause -= Resume;
    }
    // Update is called once per frame
    void Update()
    {
        if (running && stage != null) {
            Vector3 dir = Quaternion.AngleAxis(-20, Vector3.forward) * Vector3.down;
            stage.Translate(dir * ScrollSpeed, Space.Self);
        }
    }
    public void Stop() {
        this.running = false;
    }
    public void Stop(object sender) {
        this.running = false;
    }
    public void Resume() {
        this.running = true;
    }
}
