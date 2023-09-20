using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] Vector2 movementDir;
    public Playerstate state;

    public EventHandler DialogueQuitPressed;

    // Start is called before the first frame update
    void Start()
    {
        state = Playerstate.Alive;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("e");
        }


        switch (state)
        {
            case Playerstate.Alive:

                GetMovementInput();
                break;

            case Playerstate.Dialogue:

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    DialogueQuitPressed.Invoke(this, EventArgs.Empty);
                }
                break;

            default:
                break;
        }




    }

    private void FixedUpdate()
    {
        if (state == Playerstate.Alive)
        {
            rb.MovePosition(rb.position + movementDir * movementSpeed * Time.deltaTime);
        }
    }

    private void GetMovementInput()
    {
        movementDir.x = Input.GetAxisRaw("Horizontal");
        movementDir.y = Input.GetAxisRaw("Vertical");
    }


    public enum Playerstate
    {
        Alive,
        Dialogue,
    }
}
