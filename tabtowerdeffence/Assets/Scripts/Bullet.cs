using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;

    private Transform target;
    public GameObject effectHitPrefab;
    public string weaponeType;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
         if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - gameObject.transform.position;
        float distanceThisFrame = speed * Time.deltaTime;


        // target Hit
        // target distance = speed * time
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {

        GameObject effectHit = Instantiate(effectHitPrefab, transform.position, transform.rotation);
        Destroy(effectHit,1f);

        target.gameObject.GetComponent<Enemy>().Hit(damage, weaponeType);

        Destroy(gameObject);
    }
}
