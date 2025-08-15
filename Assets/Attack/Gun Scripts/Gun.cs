using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Transform bulletSpawnPoint;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject bulletPrefab, bulletHoleGraphics;
    public float bulletSpeed = 10;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();     
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit))
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
            }
            
    }

}

