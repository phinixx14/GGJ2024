using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Transform player;
    PlayerControls controls;
    public float speed = 200f;
    Vector2 movement;

    void Start() {
        player = transform.parent;
        controls = new PlayerControls();
        controls.RoadControls.Enable();
        controls.RoadControls.Move.performed += this.HandleMovement;
        controls.RoadControls.Action.performed += this.OnActionButtonPressed;
    }

    void Update() {
        //        Vector2 inputVector = controls.RoadControls.Movement.ReadValue<Vector2>() * speed * Time.deltaTime;
        Debug.Log((Vector2)player.position);
        player.Translate(movement * Time.deltaTime);
        player.position = new Vector3(Mathf.Clamp(player.position.x, -11, 11), Mathf.Clamp(player.position.y, -4, 4), 0);
    }

    public void OnPlayerDeath() {
        controls.RoadControls.Disable();
        movement = Vector3.zero;
    }

    private void HandleMovement(InputAction.CallbackContext ctx) {
        Vector2 input = ctx.ReadValue<Vector2>();
        movement = new Vector3(input.x * speed, input.y * speed, 0);
    }

    private void OnActionButtonPressed(InputAction.CallbackContext obj) {
        Debug.Log("on act");
    }
}
