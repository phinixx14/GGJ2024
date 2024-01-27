using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadClownBehavior : MonoBehaviour
{
    AbstractBehavior behavior;
    // Start is called before the first frame update
    void Start() {

    }
    // Update is called once per frame
    void Update() {
        behavior = ChooseAction();
    }
    AbstractBehavior ChooseAction() {
        return new Meander();
    }

}
