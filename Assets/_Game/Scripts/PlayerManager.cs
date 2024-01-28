using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public NoseLauncher launcher;

    PlayerCollider col;
    PlayerController controller;
    SpriteRenderer sprite;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.FindInstance();
        gm.OnPlayerDeath += OnPlayerDeath;

        col = GetComponentInChildren<PlayerCollider>();
        controller = GetComponentInChildren<PlayerController>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnDestroy() {
        gm.OnPlayerDeath -= OnPlayerDeath;
    }
    public void OnPlayerDeath(object sender) {
        Debug.Log("player death");
        if (col) {
            col.gameObject.SetActive(false);
        }
        controller.OnPlayerDeath();
    }
}
