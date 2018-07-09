using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbController : MonoBehaviour {

    public Text GameOverText;
    public bool isRolling = false;
    public bool isLoaded = false;
    public bool isFalling = false;
    public float loadSpeed = 15f;
    public float throwSpeed = 15f;
    public Vector3 startPosition;

    private bool isThrowed = false;
    
    void Update()
    {
        if (isRolling == true)
        {
            Vector3 target = new Vector3(startPosition.x + 1f, startPosition.y, startPosition.z);
            transform.position = Vector3.MoveTowards(transform.position, target, loadSpeed * Time.deltaTime);
            if(transform.position.x == target.x)
            {
                isRolling = false;
            }
        }

        if(isLoaded == true)
        {
            Vector3 playerPosition = GameObject.Find("Player").transform.position;
            Vector3 target = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
            target.x = (float)System.Math.Round(playerPosition.x);
            transform.position = Vector3.MoveTowards(transform.position, target, loadSpeed * Time.deltaTime);
            if(transform.position == target)
            {
                isLoaded = false;
                isThrowed = true;
            }
        }

        if(isThrowed == true)
        {
            transform.Translate(Vector2.up * throwSpeed * Time.deltaTime);

            Vector3 colliderPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            Collider2D hitCollider = Physics2D.OverlapCircle(colliderPosition, 0.1f);
            if (hitCollider != null && hitCollider.gameObject.GetComponent<OrbController>().isFalling == true)
            {
                float diff = hitCollider.gameObject.transform.position.y - transform.position.y;
                if (diff < 1.2 && diff > 0.8)
                {
                    transform.position = new Vector3(transform.position.x, hitCollider.gameObject.transform.position.y - 1f, transform.position.z);
                    isThrowed = false;
                    isFalling = true;

                    List<GameObject> goList = new List<GameObject>();

                    bool isFound = false;
                    bool destroyThis = false;
                    diff = 0;
                    int objectCount = 0;
                    do
                    {
                        isFound = false;
                        diff += 1f;
                        colliderPosition = new Vector3(transform.position.x, transform.position.y + diff, transform.position.z);
                        hitCollider = Physics2D.OverlapCircle(colliderPosition, 0.1f);
                        if (hitCollider != null && 
                            hitCollider.gameObject.GetComponent<OrbController>().isFalling == true &&
                            hitCollider.gameObject.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color)
                        {
                            Debug.Log("Orb found");
                            isFound = true;
                            objectCount += 1;
                            goList.Add(hitCollider.gameObject);
                        }
                    } while (isFound == true);

                    if(objectCount >= 2)
                    {
                        foreach(var objectToDestroy in goList)
                        {
                            Destroy(objectToDestroy);
                        }
                        destroyThis = true;
                        GameObject.Find("Background").GetComponent<GameWorld>().UpdateScore(objectCount);
                    }
                    goList.Clear();

                    isFound = false;
                    diff = 1f;
                    objectCount = 0;
                    do
                    {
                        isFound = false;
                        colliderPosition = new Vector3(transform.position.x + diff, transform.position.y, transform.position.z);
                        hitCollider = Physics2D.OverlapCircle(colliderPosition, 0.1f);
                        if (hitCollider != null &&
                            hitCollider.gameObject.GetComponent<OrbController>().isFalling == true &&
                            hitCollider.gameObject.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color)
                        {
                            isFound = true;
                            if(diff > 0) { diff += 1f; }
                            else if(diff < 0) { diff -= 1f; }
                            objectCount += 1;
                            goList.Add(hitCollider.gameObject);
                        } else if(diff > 0)
                        {
                            isFound = true;
                            diff = -1f;
                        }
                    } while (isFound == true);

                    if (objectCount >= 2)
                    {
                        foreach (var objectToDestroy in goList)
                        {
                            Destroy(objectToDestroy);
                        }
                        destroyThis = true;
                        GameObject.Find("Background").GetComponent<GameWorld>().UpdateScore(objectCount);
                    }
                    goList.Clear();

                    if (destroyThis == true)
                    {
                        GameObject.Find("Background").GetComponent<GameWorld>().UpdateScore(1);
                        Destroy(gameObject);
                    }
                                        
                }
            }

        }

        if(isFalling == true)
        {
            transform.Translate(0, GameObject.Find("Background").GetComponent<GameWorld>().fallSpeed, 0);

            if(transform.position.y <= -3)
            {
                //Game Over
                Time.timeScale = 0.0f;
                GameObject.Find("Background").GetComponent<GameWorld>().fallSpeed = 0;
                GameObject.Find("Canvas").transform.Find("GameOverText").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("RestartButton").gameObject.SetActive(true);
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
