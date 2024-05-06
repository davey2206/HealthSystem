using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Start()
    {
        player = GameObject.Find("Dummy");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (10f * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Health playerHealth = other.GetComponent<Health>();
        playerHealth.TakeDamage(20);

        Destroy(gameObject);
    }
}
