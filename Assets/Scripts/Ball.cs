using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Ball : MonoBehaviour
{
    public Transform strikerPoint, baseGuy1, baseGuy2, baseGuy3, baseGuy4;
    bool isThrowed = false, catcher = false;
    public GameObject ballCatcher;
    int force;

    private void Start()
    {
        Invoke(nameof(ThrowBall), 2f);
    }
    void FirstThrow()
    {
        
        transform.DOMove(strikerPoint.transform.position, 2).OnComplete(BallHit);
    }

    void BallHit()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

        int randomNumber = Random.Range(0, 100);

        if (randomNumber <= 10)
        {
            force = 3000;
        }
        else
        {
            force = 2000;
        }

        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0.35f, 0.8f) * force);
        Invoke(nameof(BallCatcher), 1);
    }

    void BallCatcher()
    {
        catcher = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball Catcher")
        {
            transform.parent = other.gameObject.transform;
            GetComponent<Rigidbody>().isKinematic = true;
            catcher = false;
            transform.DOLocalMoveY(0.3f, 0.25f);
            other.transform.DORotate(new Vector3(0, -75, 0), 1).OnComplete(BallToTheBases);
        }
    }

    void BallToTheBases()
    {      
        transform.parent = null;
        transform.DOMove(baseGuy1.position, 1.5f);
        transform.DOMove(baseGuy2.position, 1.5f).SetDelay(1.5f);
        transform.DOMove(baseGuy3.position, 1.5f).SetDelay(3.0f);
        transform.DOMove(baseGuy4.position, 1.5f).SetDelay(4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isThrowed == false)
        {
            ThrowBall();
        }
        if(catcher == true)
        {
            ballCatcher.GetComponent<NavMeshAgent>().destination = transform.position;
        }
    }

    void ThrowBall()
    {
        isThrowed = true;
        FirstThrow();
    }

    
}
