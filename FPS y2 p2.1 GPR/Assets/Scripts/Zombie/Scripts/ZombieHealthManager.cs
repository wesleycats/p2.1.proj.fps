using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthManager : MonoBehaviour
{
    public int startingHealth;
    private int currentHealth;

    private Renderer theRenderer;
    private Color storedColor;

    public float flashLength;
    private float flashCounter;

	void Start ()
    {
        currentHealth = startingHealth;

        theRenderer = GetComponent<Renderer>();

        storedColor = theRenderer.material.GetColor("_Color");
	}
	
	void Update ()
    {
		if(currentHealth <= 0)
        {
            Destroy(gameObject);
            gameObject.transform.GetComponentInParent<ZombieParentDestroy>().DestroyZombie();
        }

        if(flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if(flashCounter < 0)
            {
                theRenderer.material.SetColor("_Color", storedColor);
            }
        }
	}

    public void GiveZombieDamage(int gunDamage)
    {
        currentHealth -= gunDamage;
        flashCounter = flashLength;
        theRenderer.material.SetColor("_Color", Color.red);
    }
}
