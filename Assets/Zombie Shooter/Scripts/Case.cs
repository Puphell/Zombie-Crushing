using UnityEngine;
using System.Collections;

public class Case : MonoBehaviour{
    //everything is configured optimally, this is not very important
    public float lifetime;
    public float flytime;
    public float speed;

    void Start()
    {
        Destroy(gameObject, lifetime);//destroy when lifetime is over
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * speed);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-10,10));
        flytime += Random.Range(0, flytime);
    }

    void Update()
    {
        flytime -= Time.deltaTime;//stop when flytime is over
        if (flytime < 0) { 
            GetComponent<Rigidbody2D>().Sleep(); 
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

