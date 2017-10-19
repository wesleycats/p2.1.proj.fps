using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntBar : MonoBehaviour {

    GameObject zombie;
    Renderer _renderer;
    Color wallColor;
    int lives = 100;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        wallColor = _renderer.material.color;

        // If zombie tag is different, change the tag, otherwise zet zombie to [Serialized Field]
        zombie = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Start () {
    }
	
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider == zombie.GetComponent<CapsuleCollider>())
        {
            StartCoroutine(decreaseLife());
        }
    }

    private IEnumerator decreaseLife()
    {
        lives -= 1;
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(2);
        _renderer.material.color = wallColor;
    } 
}
