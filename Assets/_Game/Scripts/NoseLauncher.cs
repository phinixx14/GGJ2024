using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseLauncher : MonoBehaviour
{
    public Projectile nosePrefab;
    public Transform stage;
    public enum LaunchDirection { Left,Right};
    public void LaunchNose(LaunchDirection dir) {
        float angle = 0f;
        switch (dir) {
            case LaunchDirection.Left:
                angle = 70f;
                break;
            case LaunchDirection.Right:
                angle = -70f;
                break;
        }

        Vector3 v1 = transform.parent.position;
        Vector3 v2 = stage.position;
        Vector2 pos = stage.InverseTransformPoint(new Vector3(v1.x + v2.x, v1.y + v2.y, 0));
        Debug.Log(stage.position);
        
        Projectile p = Instantiate(nosePrefab, pos, Quaternion.Euler(0f, 0f, angle) * transform.rotation, stage);
        
        //p.rotation = Quaternion.Euler(0f,0f, angle) * transform.localRotation;
        //p.transform.Translate(new Vector3(0,0, -10));
    }
}
