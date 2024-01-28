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
        gm.OnReachedFinish += OnReachFinish;
        col = GetComponentInChildren<PlayerCollider>();
        controller = GetComponentInChildren<PlayerController>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnDestroy() {
        gm.OnPlayerDeath -= OnPlayerDeath;
        gm.OnReachedFinish -= OnReachFinish;
    }
    public void OnPlayerDeath(object sender) {
        if (col) {
            col.gameObject.SetActive(false);
        }
        controller.OnPlayerDeath();
    }

    public void OnReachFinish() {
        if (col) {
            col.gameObject.SetActive(false);
        }
        controller.OnPlayerDeath();
    }
}
