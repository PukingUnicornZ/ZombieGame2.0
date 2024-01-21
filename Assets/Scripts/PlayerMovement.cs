using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerMovement : NetworkBehaviour
{
    public GameObject Enemy;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    Vector3 velocity;
    bool isGrounded;


    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        characterController = GetComponent<CharacterController>();
        gameObject.name = "player" + OwnerClientId;



        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!IsOwner) { 
            playerCamera.enabled = false;
            playerCamera.gameObject.GetComponent<AudioListener>().enabled = false;
        
        }
    }
    private void Start()
    {
        transform.position = new Vector3(0, 5, -7);
    }
    // Update is called once per frame
    void Update()
    {
        //input
        if (!IsOwner) { return; }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


        //Reset
        if(transform.position.y < 0)
        {
            transform.position = new Vector3(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyServerRpc();
        }
      
    }
    [ServerRpc]   
    public void EnemyServerRpc()
    {
        GameObject g = Instantiate(Enemy);
        g.GetComponent<NetworkObject>().Spawn(true);
    }
}

