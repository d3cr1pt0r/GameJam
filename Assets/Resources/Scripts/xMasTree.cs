using UnityEngine;
using System.Collections;

public class xMasTree : MonoBehaviour {

    private Animator treeAnimator;

    private float time = 0;

	void Start () {
        treeAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(treeAnimator.GetBool("theEnd"))
        {
            time += Time.deltaTime;
            if (time > 0.5f)
                Application.LoadLevel(0);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            treeAnimator.SetBool("theEnd", true);
        }
    }
}
