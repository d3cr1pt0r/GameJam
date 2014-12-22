using UnityEngine;
using System.Collections;

public class SantaBehaviour : MonoBehaviour {

    //PUBLIC varibales
    //hitrost premkanja
    public float movementSpeed = 0.02f;
    public float jumpForce;
    public Transform spawnPoint;

    //PRIVATE variables
    //kam je santa obrjen.
    private bool facingRight;
    private Animator SantaAnimator;

    private float movementX;

    private float time = 0;


	void Start () {
        SantaAnimator = GetComponent<Animator>();

        facingRight = true;
	}
	
	void Update () {
        movementX = Input.GetAxis("Horizontal");
        //handle jump
        Jump();
        //handle hide
        Hide();

        //nastavimo hitrost v animatorju
        SantaAnimator.SetFloat("movement",Mathf.Abs(movementX));

        if(SantaAnimator.GetBool("death"))
        {
            time += Time.deltaTime;
            if (time > 1.5f)
            {
                SantaAnimator.SetBool("death",false);
                time = 0;
                //gameObject.transform.position = spawnPoint.transform.position;
            }
        }

	}

    void FixedUpdate()
    {
        //če je movement v drugo smer flipnemo. 
        CheckFlip(movementX);
        //handle movement
        if (!SantaAnimator.GetBool("hide") && !SantaAnimator.GetBool("death"))
            Movement();

        
    }

    private void Movement()
    {
        Vector3 tmp = transform.position;
		tmp.x += movementX * movementSpeed;
        transform.position = tmp;
    }

    void Hide()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            BoxCollider2D tmp = GetComponent<BoxCollider2D>();
            tmp.size = new Vector2(tmp.size.x,0.25f);
            SantaAnimator.SetBool("hide", true);
            gameObject.layer = 8;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            BoxCollider2D tmp = GetComponent<BoxCollider2D>();
            tmp.size = new Vector2(tmp.size.x, 0.49f);
            SantaAnimator.SetBool("hide", false);
            gameObject.layer = 0;
        }
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
        if (coll.gameObject.tag.Equals("Enemy"))
        {
            rigidbody2D.AddForce(new Vector2(0, 150));
            SantaAnimator.SetBool("death", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Ground"))
        {
            SantaAnimator.SetBool("jump", false);
        }
    }
}
