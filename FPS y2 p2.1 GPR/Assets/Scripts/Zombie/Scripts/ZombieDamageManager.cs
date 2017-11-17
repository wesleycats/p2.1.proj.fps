using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamageManager : MonoBehaviour
{

    public int damageToGive;


    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //uncomment when importing dave zn shit
            //other.gameObject.GetComponent<PlayerManager>().applyDamageOnPlayer(damageToGive);
        }
    }

}
