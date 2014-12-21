using UnityEngine;
using System.Collections;

public class SantaBehaviour : MonoBehaviour {

    //PUBLIC varibales
    //hitrost premkanja
    public float movementSpeed;
    public float jumpForce;

    //PRIVATE variables
    //kam je santa obrjen.
    private bool facingRight;
    private Animator SantaAnimator;

    private float movementX;


	void Start () {
        SantaAnimator = GetComponent<Animator>();

        facingRight = true;
	}
	
	void FixedUpdate () {
        movementX = Input.GetAxis("Horizontal");
        //če je movement v drugo smer flipnemo. 
        CheckFlip(movementX);
        //handle movement
        Movement();
        //handle jump
        Jump();

        //nastavimo hitrost v animatorju
        SantaAnimator.SetFloat("movement",Mathf.Abs(movementX));

	}

    private void Movement()
    {
        Vector3 tmp = transform.position;
        tmp.x += movementX * 0.05f;
        transform.position = tmp;
    }

    private void CheckFlip(float movementX)
    {
        if (movementX < 0 && facingRight == true)
        {
            facingRight = false;
            Flip();
        }
        if (movementX > 0 && facingRight == false)
        {
            facingRight = true;
            Flip();
        }
    }

    private void Flip()
    {
         Vector3 theScale = transform.localScale;
         theScale.x *= -1;
         transform.localScale = theScale;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && SantaAnimator.GetBool("jump") == false)
        {
            SantaAnimator.SetBool("jump", true);
            rigidbody2D.AddForce(new Vector2(0,jumpForce));
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            SantaAnimator.SetBool("jump", false);
        }
    }
}
