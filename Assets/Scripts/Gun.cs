
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float dmg = 10f;
    public float range = 100;
    public LayerMask Player;
    public GameObject gun;
    private bool canFire = true;
    public float fireRate = 2.5f;
    public ParticleSystem ParticleSystem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && canFire == true)
        {
            Shoot();
            canFire = false;
        }
    }

    void Shoot()
    {
        ParticleSystem.Play();
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, range, ~Player))
        {
            
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(dmg);
            }
        }
        else
        {
            Debug.Log("Missed");
        }       
        StartCoroutine(FireRateHandler());
    }

    IEnumerator FireRateHandler()
    {
        float timeToNextFire = 1 / fireRate;
        yield return new  WaitForSeconds(timeToNextFire);
        canFire = true;
    }
}
