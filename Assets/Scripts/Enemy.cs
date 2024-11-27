
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 25f;

    [SerializeField] FloatingHealthBar healthBar;
    public Animator animator;
    public List<string> names = new List<string>{"Mushroom","Cactus" };

    public GameObject player;
    public float speed = 5f;
    public int damage;
    private bool dmgDone;
    bool canMove = true;
    
    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        player = GameObject.FindWithTag("Character");

        foreach (string name in names)
        {
            Transform child = transform.Find(name);

            if (child != null)
            {
                
                animator = child.GetComponent<Animator>();               
                break;
            }
        }
    }

    private void Update()
    {
        if (canMove == true)
        {
            transform.LookAt(player.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        
    }
  
    void OnTriggerEnter(Collider coll)
    {
        
        if (coll.gameObject.CompareTag("Character") && dmgDone == false)
        {
            
            FindObjectOfType<PlayerController>().HealthUpdate(damage);
            FindObjectOfType<ScoreManager>().HealthChange(damage);

            dmgDone = true;
            canMove = false;
            FindObjectOfType<GameManager>().EnemyUpdater(1);

            animator.SetBool("HasCollided", true);
            StartCoroutine(AttackAnimation());
            
        }
    }

    public void TakeDamage(float amount)
    { 
            health -= amount;
            healthBar.UpdateHealthBar(health, maxHealth);

            if (health <= 0)
            {
                Die();
            }
    }

    public void Die()
    {       
        FindObjectOfType<ScoreManager>().AddScore(25); 
        FindObjectOfType<GameManager>().EnemyUpdater(1);
        Destroy(gameObject);
    }

    private IEnumerator AttackAnimation()
    {
        Debug.Log("Playing Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }
}
