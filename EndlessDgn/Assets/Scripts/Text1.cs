using UnityEngine;

public class Text1 : MonoBehaviour {
    void FixedUpdate ()
    {
        transform.position = Input.mousePosition + new Vector3(90 , 0 ,0);
    }
}
