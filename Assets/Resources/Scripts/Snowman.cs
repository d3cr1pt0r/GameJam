using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour
{
	public float speed = 0.02f;

	private bool flipped;

    private bool movingLeft = false;

    private GameObject ground;
    private Vector2 patrolPoint;

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
            SnowmanLogic();
	}

    void SnowmanLogic()
    {
        //Debug.Log("gameobject:" + gameObject.transform.position.x + " patrolpoint:" + patrolPoint.x + " movingLeft:" + movingLeft);
        if (ground_collision && gameObject.transform.position.x <  patrolPoint.x && movingLeft)
        {
            movingLeft = false;
            RandomPoint();
        }
        else if (ground_collision && gameObject.transform.position.x > patrolPoint.x && movingLeft == false)
        {
            movingLeft = true;
            RandomPoint();
        }

        if (ground_collision && patrolPoint.x < gameObject.transform.position.x && movingLeft)
            moveLeft(speed);
        if (ground_collision && patrolPoint.x > gameObject.transform.position.x && movingLeft == false)
            moveRight(speed);
    }

    void RandomPoint()
    {
        //če je bil prejšnji patrol point manjši od od polovice
        float randomX = 0;
        if (patrolPoint.x < ground.transform.position.x)
            randomX = Random.Range(ground.transform.localScale.x / 2, ground.transform.localScale.x);
        else
            randomX = Random.Range(0, ground.transform.localScale.x / 2);
       
        float randomXpos = (ground.transform.position.x - Mathf.Abs(ground.transform.localScale.x) / 2) + randomX ;
        print(randomXpos);
        patrolPoint = new Vector2(randomXpos , transform.position.y);
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
