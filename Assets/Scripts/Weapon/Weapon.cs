using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;
    public int weaponDamage;

    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //Spread
    public float spreadIntensity;

    //Bullet property
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;
    public GameObject muzzleEffect;
    private Animator animator;

    //Loading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    //UI
    public TextMeshProUGUI ammoDisplay;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletsLeft == 0 && isShooting)
        {
            SoundManager.Instance.emptyMagazineSound1911.Play();
        }

        if(currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single||currentShootingMode==ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(Input.GetKeyDown(KeyCode.R)&&bulletsLeft<magazineSize && isReloading==false)
        {
            Reload();
        }

        //automatic reload
        if (readyToShoot && isShooting==false && isReloading==false && bulletsLeft<=0)
        {
            Reload();
        }

        if (readyToShoot && isShooting && bulletsLeft>0)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }

        if(AmmoManager.Instance.ammoDisplay != null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft/bulletsPerBurst}/{magazineSize/bulletsPerBurst}";
        }
    }



    private void FireWeapon()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        SoundManager.Instance.shootingSound1911.Play();

        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity);

        MyBullet bul = bullet.GetComponent<MyBullet>();
        bul.bulletDamage = weaponDamage;
        Debug.Log("Weapon assigned bulletDamage: " + bul.bulletDamage);


        //Point at the shooting direction
        bullet.transform.forward = shootingDirection;

        //shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection*bulletVelocity,ForceMode.Impulse);

        //destroy bullet aft some time
        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefabLifeTime));

        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset=false;
        }

        if (currentShootingMode==ShootingMode.Burst&&burstBulletsLeft>1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon",shootingDelay);
        }

    }


    private void Reload()
    {

        SoundManager.Instance.reloadSound1911.Play();

        isReloading = true;
        Invoke("ReloadCompleted",reloadTime);

    }

    private void ReloadCompleted()
    {
        bulletsLeft=magazineSize;
        isReloading=false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint-bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity,spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity,spreadIntensity);

        return direction + new Vector3(x,y,0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet,float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
