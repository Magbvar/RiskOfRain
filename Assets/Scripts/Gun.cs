
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
    public GameObject ParticleSystem;
    public Camera Camera;
    Animator animator;

    // Update is called once per frame

    private void Start()
    {
        animator = transform.Find("FemalePlayer").GetComponent<Animator>();

        if (animator == null)
        {
            Debug.Log("no");
        }
    }
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
        animator.SetBool("Attack", true);
        
        
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, range, ~Player))
        {
            
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(dmg);
            }
            
            GameObject Impact = Instantiate(ParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(Impact,0.2f);
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
        animator.SetBool("Attack", false);
        
        canFire = true;
    }
}
