using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletDamageOverTime;
    [SerializeField] bool shoot;
    [SerializeField] bool damageOverTime;

    private float delay = 1;
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (shoot && !damageOverTime)
            {
                delay = 1;
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
            if (shoot && damageOverTime)
            {
                Instantiate(bulletDamageOverTime, transform.position, Quaternion.identity);
                delay = 10;
            }
        }
    }
}
