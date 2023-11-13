using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Vector3 targetPos;
    public float speed;
    public int damage;
    public float endTime;
    public float TTime;
    private float TCTime;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = FindObjectOfType<PlayerMovement>().transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        TCTime = TCTime + TTime;

        transform.position = Vector3.MoveTowards(transform.position, Vector3.forward, speed * Time.deltaTime);

        if(TCTime > endTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().TakeDamage(damage);
        Destroy(this.gameObject);
    }
}
