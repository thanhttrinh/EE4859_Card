using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour {
    public int health = 0;
    public int attack = 0;
    public int movement = 0;
    public int range = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            Die();
        }
	}

    void Die()
    {
        Destroy(gameObject);
    }
}
