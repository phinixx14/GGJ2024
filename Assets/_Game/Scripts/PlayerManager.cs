using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
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

    public void OnPlayerDeath(object sender) {
        Debug.Log("player death");
        col.gameObject.SetActive(false);
        controller.OnPlayerDeath();
    }
}
