using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private float speed = 5;
    private float mouseSensitivity = 3.5f;
    private float gravity = -9;
    Transform cameraTrans;
    float cameraPitch = 0;
    [SerializeField]
    private float standingHeight = 0.75f;
    [SerializeField]
    private float crouchingHeight = 0.375f;

    
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        cameraTrans = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //handle looking
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);

        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);
        cameraTrans.localEulerAngles = Vector3.right * cameraPitch;

        Vector3 crouchingHeightVec = new Vector3(0, crouchingHeight,0);
        Vector3 standingHeightVec = new Vector3(0, standingHeight, 0);

        cameraTrans.localPosition = (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl)) ? crouchingHeightVec : standingHeightVec;

        //handle movement
        //float gravcalc = transform.position -= gravity;
        Vector3 move = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), -10 * Time.deltaTime, Input.GetAxis("Vertical"));
        cc.Move(move * Time.deltaTime * speed);


        //handle player interaction
        

    }


}
