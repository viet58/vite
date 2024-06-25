using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public Animator anim;
    
    public float horizontal;
    public float vertical;

    public float currentSpeed;
    public float normalSpeed = 25.0f;
    public float sprintSpeed = 50.0f;
    public bool running = false;
    
    public float angle = 45f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        
    horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.LeftShift) && (horizontal != 0 || vertical != 0) || Input.GetKey(KeyCode.RightShift) && (horizontal != 0 || vertical != 0))
        {

            currentSpeed = sprintSpeed;
            running = true;
            
        }
        else
        {
            currentSpeed = normalSpeed;
            running = false;
        }

        
            

        if (horizontal > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
          
        else if (horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

       if(vertical > 0 && transform.localScale.y < 0)
        {
            FlipD();
        }
        else if(vertical < 0 && transform.localScale.y > 0)
        {
            FlipD();
            
        }

       if(vertical == 0 && transform.localScale.y < 0)
        {
            FlipD();
        }

       if(horizontal > 0 && vertical < 0 || horizontal < 0 && vertical < 0)
        {
            FlipD();
        }

        Border();
       
       
        

        anim.SetFloat("DiChuyen", Mathf.Abs( horizontal));
        anim.SetFloat("DiChuyenDoc", Mathf.Abs(vertical));
            
       
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        
        position.x = position.x + currentSpeed * horizontal * Time.deltaTime;
        
        position.y = position.y + currentSpeed * vertical * Time.deltaTime;

        Vector2 movement = new Vector2(horizontal, vertical);
        if(movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        



        position += movement * currentSpeed * Time.deltaTime;

        rigidbody2d.MovePosition(position);



    }

    void Flip()
    {
        Vector2 currenScale = transform.localScale;
        currenScale.x *= -1;
        currenScale.y = currenScale.y;

        transform.localScale = currenScale;
    }

    void FlipD()
    {
        Vector2 currenScale = transform.localScale;

        currenScale.x = currenScale.x;
        currenScale.y *= -1;

        transform.localScale = currenScale;

    }

    void Border()
    {
        if(transform.position.y >= 48.33f)
        {
            transform.position = new Vector3(transform.position.x, 48.33f, 0);
        }
        else if(transform.position.y <= -70.8f)
        {
            transform.position = new Vector3(transform.position.x, -70.8f, 0);
        }
        else if(transform.position.x <= -1.1f)
        {
            transform.position = new Vector3(-1.1f, transform.position.y, 0);
        }
        else if (transform.position.x >= 286.1f)
        {
            transform.position = new Vector3(286.1f, transform.position.y, 0);
        }

       
       
    }

   




}
