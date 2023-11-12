using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public Animator anim;
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, maxAmo;
    private int amo;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    //public GameObject muzzleFlash, bulletHoleGraphic;
    public CameraShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI Amotxt;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        amo = maxAmo;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        //SetText
        if(!reloading)
            Amotxt.SetText(bulletsLeft + " / " + amo);

        if(amo < 0)
        {
            amo = 0;
        }
        else if(amo > maxAmo)
        {
            amo = maxAmo;
        }

        if(bulletsLeft < 0)
        {
            bulletsLeft = 0;
        }
        else if(bulletsLeft > magazineSize)
        {
            bulletsLeft = magazineSize;
        }
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && amo > 0) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            //if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<enemy>().TakeDamage(damage);
        }

        //ShakeCamera
        camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        anim.SetTrigger("her");
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        Amotxt.SetText("......");
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        if(amo >= magazineSize)
        {
            int a = magazineSize - bulletsLeft;
            amo = amo - a ;
            bulletsLeft = magazineSize;
        }
        else if (amo < magazineSize)
        {
            int a = magazineSize - bulletsLeft;
            int b = 0;
            if(a > amo)
            {
                b = amo;
            }
            else if (a < amo)
            {
               b = amo - a;

            }
            amo = amo - b;
            bulletsLeft = bulletsLeft + b;
        }
        reloading = false;
    }
}
