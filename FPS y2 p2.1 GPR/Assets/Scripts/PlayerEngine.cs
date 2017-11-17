using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerEngine : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float camRotLimit = 85f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCamRotX = 0;

    private float shoot;
    private bool moving;
    private bool shooting;
                                                                             
    public float weaponRange = 50f;
    public int gunDamage;
    public Transform gunEnd;

                                            
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    
    private AudioSource gunAudio;                                       
    private LineRenderer laserLine;                                    

    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private GameObject impactEffect;

    private Rigidbody rb;
    private Collider coll;
    private float nextFire;
    private float fireRate = 15f;

    private string targetTag;
    public string targetName;
    public double targetX;
    public double targetY;
    public double targetZ;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        laserLine = GetComponent<LineRenderer>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void Jump(Vector3 _jumpForce)
    {
        jumpForce = _jumpForce;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    public void Shoot(bool _shooting)
    {
        shooting = _shooting;
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.2f);
    }
    public float Approach(float a, float b, float amount)
    {
        if (a < b)
        {
            a += amount;
            if (a > b)
                return b;
        }
        else
        {
            a -= amount;
            if (a < b)
                return b;
        }
        return a;
    }


    void FixedUpdate()
    {
        PerformMovement();
        performRotation();
        performJump();
        PerformShoot();
    }

    void PerformMovement()
    {
        RaycastHit hit;
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
//        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0f), Vector3.down, out hit, offset))
//        {

//        }
    }
    void PerformShoot()
    {
        if (shooting && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;

            muzzleFlash.Play();

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange))
            {
                Target target = hit.transform.GetComponent<Target>();
                targetTag = hit.transform.tag;
                targetName = hit.transform.parent.name;
                targetX = Math.Floor(hit.transform.position.x);
                targetY = Math.Floor(hit.transform.position.y);
                targetZ = Math.Floor(hit.transform.position.z);

                if (targetTag == "Zombie")
                {
                    gameObject.GetComponent<InfoToDB>().ActivatePHP(targetName, targetX, targetY, targetZ);
                    hit.transform.GetComponent<ZombieHealthManager>().GiveZombieDamage(gunDamage);
                }

                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
//                // Check if the object we hit has a rigidbody attached
//                if (hit.rigidbody != null)
//                {
//                    // Add force to the rigidbody we hit, in the direction from which it was hit
//                    hit.rigidbody.AddForce(-hit.normal * hitForce);
//                }
            }
        }
    }

    void performRotation()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            currentCamRotX -= cameraRotationX;
            currentCamRotX = Mathf.Clamp(currentCamRotX, -camRotLimit, camRotLimit);

            cam.transform.localEulerAngles = new Vector3(currentCamRotX, 0f, 0f);
        }
    }
    void performJump()
    {
        if (jumpForce != Vector3.zero && Grounded())
        {
            rb.AddForce(Vector3.up * jumpForce.y, ForceMode.Impulse);
        }
    }
}
