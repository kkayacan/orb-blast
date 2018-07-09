using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawnerController : MonoBehaviour {

    public Transform newOrb;
    public float startPos;

    // Use this for initialization
    void Start () {
        startPos = transform.position.y;
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, GameObject.Find("Background").GetComponent<GameWorld>().fallSpeed, 0);
        if (transform.position.y <= ( startPos - 1 ))
        {
            var tmp = transform.position;
            tmp.y += 1;
            transform.position = tmp;
            Spawn();
        }
    }

    void Spawn ()
    {
        for (float i = -2f; i <= 2; i = i + 1f)
        {
            Transform orb = Instantiate(newOrb, new Vector3(i, transform.position.y, 0), Quaternion.identity);
            orb.gameObject.GetComponent<OrbController>().isFalling = true;
            Utility.setColor(orb.gameObject);
        }
    }
}
