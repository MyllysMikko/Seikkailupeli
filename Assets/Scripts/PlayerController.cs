using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] Vector2 movementDir;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        movementDir.x = Input.GetAxisRaw("Horizontal");
        movementDir.y = Input.GetAxisRaw("Vertical");


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDir * movementSpeed * Time.deltaTime);
    }
}
