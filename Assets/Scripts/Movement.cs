using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public float runSpeed;
    public float destroyDelay;

    private Collider myCollider;
    private Rigidbody myRigidbody;
    private Spawner spawner;

    private Vector3 direction;
    public Vector3 rotationSpeed;

    private bool hasCollided;

    private Vector3 RandomDirection()

    {
        return new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.5f, -1.0f), 0);

    }

    void increaseSpeed()
    {
        runSpeed = runSpeed + 20f;
    }

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        hasCollided = false;
        direction = RandomDirection();
        InvokeRepeating("increaseSpeed", 0.3f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
        transform.Translate(direction * runSpeed * Time.deltaTime, Space.World);
    }

    public void SetSpawner(Spawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Diamond")) {
            if (other.CompareTag("Hand") && !hasCollided)
            {
                hasCollided = true;
                CollectDiamond();
            }

            if (other.CompareTag("Head") && !hasCollided)
            {
                hasCollided = true;
                LoseDiamond();
            }
        }

        else if (gameObject.CompareTag("Rock"))
        {
            if (other.CompareTag("Head") && !hasCollided)
            {
                hasCollided = true;
                LoseLive();
            }

            if (other.CompareTag("Hand") && !hasCollided)
            {
                hasCollided = true;
                LoseDiamond();
            }
        }
    }

    private void CollectDiamond()
    {
        runSpeed = 0;
        Destroy(gameObject, destroyDelay);
        spawner.RemoveDiamFromList(gameObject);
        GameManager.Instance.collectDiamond();
    }

    private void LoseLive()
    {
        runSpeed = 0;
        Destroy(gameObject, destroyDelay);
        spawner.RemoveProjFromList(gameObject);
        GameManager.Instance.lessLife();

    }

    private void LoseDiamond()
    {
        runSpeed = 0;
        Destroy(gameObject, destroyDelay);
        spawner.RemoveDiamFromList(gameObject);
        GameManager.Instance.loseDiamond();
    }
}
