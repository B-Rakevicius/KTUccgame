using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class KeyBindings
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
}
// Command pattern ↓
public interface ICommand
{
    void Execute();
}
public class MoveCommand : ICommand
{
    private CharacterControllerTopDown player;
    private float xAxis;
    private float yAxis;

    public MoveCommand(CharacterControllerTopDown player, float xAxis, float yAxis)
    {
        this.player = player;
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public void Execute()
    {
        player.Move();
    }
}
public class InteractCommand : ICommand
{
    private CharacterControllerTopDown player;


    public InteractCommand(CharacterControllerTopDown player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Interact();
    }
}
// Command pattern ↑


public class CharacterControllerTopDown : MonoBehaviour
{
    private ICommand moveCommand;
    private ICommand interactCommand;
    private float speed; // Player movement speed
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    //Resource heavy movement
    public bool movingRight, movingLeft, movingUp, movingDown;
    public int playerDirection;
    public GameObject faceR, faceL, faceU, faceD;
    public Animator playerAnimations;
    //Resource heavy movement

    //Input Variables
    public KeyBindings keyBindings;
    private float xAxis, yAxis;
    private bool interact = false;
    bool openMap;

    void Awake()
    {
        speed = 5;
    }
    void Start()
    {
        // Command pattern
        moveCommand = new MoveCommand(this, xAxis, yAxis);
        interactCommand = new InteractCommand(this);
        // Command pattern

        //LoadKeyBindings();

    }
    void Update()
    {
        GetInputs();
        moveCommand.Execute();
        interactCommand.Execute();

    }
    void GetInputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        interact = Input.GetButtonDown("Interact");
        openMap = Input.GetButton("Map");
        
    }
    public void Move()
    {
        
        if (Mathf.Abs(xAxis) > MathF.Abs(moveInput.y))
        {
            moveInput.y = 0;
            rb2d.velocity = moveInput * speed;
        }
        else
        {
            xAxis = 0;
            rb2d.velocity = moveInput * speed;
        }
        if (xAxis > 0) //right
        {
            movingRight = true;
            faceR.SetActive(true);
            faceL.SetActive(false);
            faceD.SetActive(false);
            faceU.SetActive(false);
            playerAnimations.Play("walkR");
        }
        else
        {
            movingRight = false;
        }
        if (xAxis < 0) // left
        {
            movingLeft = true;
            faceR.SetActive(false);
            faceL.SetActive(true);
            faceD.SetActive(false);
            faceU.SetActive(false);
            playerAnimations.Play("walkL");
        }
        else
        {
            movingLeft = false;
        }
        if (moveInput.y < 0) // down
        {
            movingDown = true;
            faceR.SetActive(false);
            faceL.SetActive(false);
            faceD.SetActive(true);
            faceU.SetActive(false);
            playerAnimations.Play("walkD");
        }
        else
        {
            movingDown = false;
        }
        if (moveInput.y > 0) // up
        {
            movingUp = true;
            faceR.SetActive(false);
            faceL.SetActive(false);
            faceD.SetActive(false);
            faceU.SetActive(true);
            playerAnimations.Play("walkU");
        }
        else
        {
            movingUp = false;
        }
        if (moveInput.y == 0 && moveInput.x == 0)
        {
            playerAnimations.Play("PlayerIdle");
        }
        
        /*
        if (Input.GetKey(keyBindings.moveUp))
        {
            transform.Translate(Vector2.up * testVert(Vector2.up));
        }
        if (Input.GetKey(keyBindings.moveDown))
        {
            transform.Translate(Vector2.down * testVert(Vector2.down));
        }
        if (Input.GetKey(keyBindings.moveLeft))
        {
            transform.Translate(Vector2.left * testHor(Vector2.left));
        }
        if (Input.GetKey(keyBindings.moveRight))
        {
            transform.Translate(Vector2.right * testHor(Vector2.right));
        }*/
    }
    public void Interact()
    {
        if (interact)
        {

        }
    }
    // Method to remap keybindings
    public void RemapKey(string action, KeyCode newKey)
    {
        switch (action)
        {
            case "Up":
                keyBindings.moveUp = newKey;
                break;
            case "Down":
                keyBindings.moveDown = newKey;
                break;
            case "Left":
                keyBindings.moveLeft = newKey;
                break;
            case "Right":
                keyBindings.moveRight = newKey;
                break;
        }
    }

    /*
    // UI button calls this function to remap a key ↑ for remap key
    public void SetNewKeyForMoveUp(KeyCode newKey)
    {
        playerMovement.RemapKey("Up", newKey);
    }
     */
    /*
    public void SaveKeyBindings()
    {
        PlayerPrefs.SetInt("MoveUp", (int)keyBindings.moveUp);
        PlayerPrefs.SetInt("MoveDown", (int)keyBindings.moveDown);
        PlayerPrefs.SetInt("MoveLeft", (int)keyBindings.moveLeft);
        PlayerPrefs.SetInt("MoveRight", (int)keyBindings.moveRight);
        PlayerPrefs.Save();
    }
    public void LoadKeyBindings()
    {
        keyBindings.moveUp = (KeyCode)PlayerPrefs.GetInt("MoveUp", (int)KeyCode.W);
        keyBindings.moveDown = (KeyCode)PlayerPrefs.GetInt("MoveDown", (int)KeyCode.S);
        keyBindings.moveLeft = (KeyCode)PlayerPrefs.GetInt("MoveLeft", (int)KeyCode.A);
        keyBindings.moveRight = (KeyCode)PlayerPrefs.GetInt("MoveRight", (int)KeyCode.D);
    }
    public void ResetToDefaults()
    {
        keyBindings.moveUp = KeyCode.W;
        keyBindings.moveDown = KeyCode.S;
        keyBindings.moveLeft = KeyCode.A;
        keyBindings.moveRight = KeyCode.D;

        SaveKeyBindings();  // Save default settings
    }

    void ToggleMap()
    {
        if (openMap)
        {
            UIManager.Instance.mapHandler.SetActive(true);
        }
        else
        {
            UIManager.Instance.mapHandler.SetActive(false);
        }

    }
    */
    private float testHor(Vector2 dir)
    {
        Vector2 origin = transform.position;
        float offset;
        if (dir.Equals(Vector2.left))
        {
            offset = -0.125f; // Offset for left side of character
        }
        else
        {
            offset = 0.125f; // Offset for right side of character
        }
        origin.x += offset;
        RaycastHit2D raycast = Physics2D.Raycast(origin, dir, speed * Time.deltaTime);

        if (raycast.collider != null && raycast.collider.gameObject.tag == "Collidable")
        { // If raycast hits something tagged "Collidable"
            float distance = Math.Abs(raycast.point.x - origin.x);
            return distance;
        }

        return speed * Time.deltaTime;
    }
    private float testVert(Vector2 dir)
    {
        Vector2 origin = transform.position;
        float offset;
        if (dir.Equals(Vector2.up))
        {
            offset = .1f; // Offset for top side of character
        }
        else
        {
            offset = -0.5f; // Offset for bottom side of character
        }
        origin.y += offset;
        RaycastHit2D raycast = Physics2D.Raycast(origin, dir, speed * Time.deltaTime);

        if (raycast.collider != null && raycast.collider.gameObject.tag == "Collidable")
        { // If raycast hits something tagged "Collidable"
            float distance = Math.Abs(raycast.point.y - origin.y);
            return distance;
        }

        return speed * Time.deltaTime;
    }
}
