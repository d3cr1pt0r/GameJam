using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour
{
	public float speed = 0.02f;

	private bool flipped;

    private bool movingLeft = true;

    private GameObject ground;
    private float patrolPoint;

    private Animator snowmanAnimator;

    private bool ground_collision = false;
	void Start ()
	{
		flipped = false;
        snowmanAnimator = GetComponent<Animator>();
	}

	void Update ()
	{
        if (snowmanAnimator.GetBool("death") == false)
        {
            SnowmanLogic();

            if (ground_collision && movingLeft)
                moveLeft(speed);
            if (ground_collision && !movingLeft)
                moveRight(speed);

        }
	}

    void SnowmanLogic()
    {
        if (ground_collision && (movingLeft && transform.position.x < patrolPoint || !movingLeft && transform.position.x > patrolPoint))
            RandomPoint();
    }

    void RandomPoint()
    {
        if(gameObject.transform.position.x > ground.transform.position.x)
        {
            //generiraj levo
            patrolPoint = Random.Range(ground.transform.position.x - ground.transform.localScale.x / 2, ground.transform.position.x);
            movingLeft = true;
        }
        else
        {
            //generiraj desno
            patrolPoint = Random.Range(ground.transform.position.x,ground.transform.localScale.x / 2 + ground.transform.position.x);
            movingLeft = false;
        }
            
    }

	void moveLeft(float speed)
	{
		transform.position += new Vector3(-speed, 0, 0);
		if (flipped)
		{
			flipped = false;
			Vector3 curScale = transform.localScale;
			curScale.x *= -1.0f;
			transform.localScale = curScale;
		}
	}

	void moveRight(float speed)
	{
		transform.position += new Vector3(speed, 0, 0);
		if (!flipped)
		{
			flipped = true;
			Vector3 curScale = transform.localScale;
			curScale.x *= -1.0f;
			transform.localScale = curScale;
		}
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            ground_collision = true;
            ground = coll.gameObject;
            RandomPoint();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            snowmanAnimator.SetBool("death", true);
            BoxCollider2D tmp = GetComponent<BoxCollider2D>();
            tmp.size = new Vector2(tmp.size.x, 0.25f);
            gameObject.layer = 10;
        }
    }
}
