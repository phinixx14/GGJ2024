using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    PlayerCollider collider;
    PlayerController controller;
    SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponentInChildren<PlayerCollider>();
        controller = GetComponentInChildren<PlayerController>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        
    }

    public void OnPlayerDeath() {
        Debug.Log("player death");
        collider.gameObject.SetActive(false);
        controller.OnPlayerDeath();
    }
}
