using UnityEngine;
using UnityEngine.InputSystem;

public struct Magazine {
    public int ammoCapacity;
    public int currentAmmo;

    public Magazine(int ammoCapacity) {
        this.ammoCapacity = ammoCapacity;
        this.currentAmmo = ammoCapacity;
    }
}

public class Shooting : MonoBehaviour
{
    public Transform gunBarrel;
    public GameObject bulletPrefab;
    public Magazine mag;
    public float bulletSpeed;
    public float lastShotTime;
    public float fireRate;
    public float timeBetweenShots;

    public void Start() {
        mag = new Magazine();
        mag.ammoCapacity = 30;
        mag.currentAmmo = 30;
    }

    // Called by inputsystem via SendMessage()
    void OnShoot(InputValue value) {
        Shoot();
    }

    void Shoot() {
        if(CanFire()) {
            CreateBullet();
            lastShotTime = Time.time;
        }
    }

    GameObject CreateBullet() {
        //Create the bullet and enqueue its destruction
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.transform.position, gunBarrel.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 3f);
        return bullet;
    }

    public bool CanFire() {
        if (mag.currentAmmo <= 0) return false;
        if (Time.time - lastShotTime < timeBetweenShots) return false;
        return true;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.DrawLine(gunBarrel.position, gunBarrel.position + gunBarrel.forward);
    }
    #endif
}
