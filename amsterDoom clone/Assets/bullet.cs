using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Vector3 targetPos;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = FindObjectOfType<PlayerMovement>().transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if(transform.position == targetPos)
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().TakeDamage(damage);
        Destroy(this);
    }
}
