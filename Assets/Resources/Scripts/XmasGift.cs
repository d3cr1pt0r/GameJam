﻿using UnityEngine;
using System.Collections;

public class XmasGift : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag.Equals("Ground"))
            gameObject.layer = 0;

        if (coll.gameObject.tag.Equals("Player"))
            Destroy(gameObject);
    }
}
