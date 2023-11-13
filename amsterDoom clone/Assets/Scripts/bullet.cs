using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject THIS;
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
        TCTime = TTime + TTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if(transform.position == targetPos)
        {
            Destroy(THIS);
        }

        if(TTime > endTime)
        {
            Destroy(THIS);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().TakeDamage(damage);
        Destroy(this);
    }
}
