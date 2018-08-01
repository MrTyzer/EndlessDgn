using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Messenger.Broadcast(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN);
        }
    }
}



