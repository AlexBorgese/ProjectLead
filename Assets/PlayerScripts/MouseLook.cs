using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

     private GameObject player;
    private float minClamp = -45;
    private float maxClamp = 45;
    [HideInInspector]
    public Vector2 rotation;
    private Vector2 currentLookRot;
    private Vector2 rotationV = new Vector2(0,0);
    public float lookSensitivity = 2;
    public float lookSmoothDamp = 0.1f; 
 
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(WeaponWheelController.weaponWheelSelected != true) {
        //Player input from the mouse
        rotation.y += Input.GetAxis("Mouse Y") * lookSensitivity;
 
        //Limit ability look up and down.
        rotation.y = Mathf.Clamp(rotation.y, minClamp, maxClamp);

        player.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity);

        currentLookRot.y = Mathf.SmoothDamp(currentLookRot.y, rotation.y, ref rotationV.y, lookSmoothDamp);

        transform.localEulerAngles = new Vector3(-currentLookRot.y, 0, 0);
        }
    }
}
