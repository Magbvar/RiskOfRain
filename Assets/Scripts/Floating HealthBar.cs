using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    [SerializeField] private Transform target;
    [SerializeField] private GameObject camera;

    private void Awake()
    {
        camera = GameObject.FindWithTag("MainCamera");
        
    }
    public void UpdateHealthBar(float health, float maxHealth)
    {
        slider.value = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}
