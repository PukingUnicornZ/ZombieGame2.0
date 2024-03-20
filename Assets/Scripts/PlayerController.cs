using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform body;
    [SerializeField] private float range;

    [SerializeField] private int dmg;

    [SerializeField] private Gun[] guns;
    [SerializeField] private Gun currentGun;
    [SerializeField] private int currentGunID;
    [SerializeField] private GameObject currentGunModel;

    [SerializeField] private Texture[] playerTextures;



    [SerializeField] private float shootDelay;
    [SerializeField] private bool canShoot;
    [SerializeField] private int currentAmmo;

    private UIManager uIManager;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        uIManager = FindObjectOfType<UIManager>();
        currentGun = guns[currentGunID];
        if ((int)OwnerClientId < 4)
        {
            body.GetComponent<SkinnedMeshRenderer>().material.mainTexture = playerTextures[(int)OwnerClientId];

        }
        setGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
        //Weapon selecting
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // up
        {
            if (currentGunID < guns.Length - 1)
            {
                currentGunID++;
                setGun();
            }
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // down
        {
            if (currentGunID > 0)
            {
                currentGunID--;
                setGun();
            }
        }
        //Shooting
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (shootDelay > currentGun.firerate)
            {
                if (currentGun.auto)
                {
                    Shoot();

                }
                else if (canShoot == true)
                {
                    Shoot();
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            canShoot = true;
        }
        if(shootDelay < currentGun.firerate)
        {
            shootDelay += Time.deltaTime;
        }
    }
    void setGun()
    {
        currentGun = guns[currentGunID];
        currentAmmo = currentGun.maxAmmo;
        setGunModelServerRpc(currentGunID);
        uIManager.updateAmmoUI(currentAmmo, currentGun.maxAmmo);
    }


    [ServerRpc(RequireOwnership = false)]
    void setGunModelServerRpc(int gunID)
    {
        setGunModelClientRpc(gunID, (int)OwnerClientId);
    }


    [ClientRpc(RequireOwnership = false)]
    void setGunModelClientRpc(int gunId,int playerId)
    {
        if (playerId == (int)OwnerClientId)
        {
            Destroy(currentGunModel);
            GameObject gun = Instantiate(guns[gunId].model, cam.transform);
            gun.transform.localPosition = new Vector3(0.225f, -0.2f, 0.63f);
            currentGunModel = gun;
        }
    }
    void Reload()
    {
        currentAmmo = currentGun.maxAmmo;
        uIManager.updateAmmoUI(currentAmmo, currentGun.maxAmmo);
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            uIManager.updateAmmoUI(currentAmmo,currentGun.maxAmmo);
            if (!currentGun.shotgun)
            {
                shootDelay = 0;
                canShoot = false;
                //Raycasting
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
                {
                    Enemy target = hit.transform.GetComponent<Enemy>();
                    if (target != null)
                    {
                        target.DamageServerRpc(currentGun.dmg);

                    }
                }
            }
            else
            {
                shootDelay = 0;
                for (int i = 0; i < currentGun.shotgunBullets; i++)
                {
                    Vector3 fwd = cam.transform.forward;
                    fwd.x += Random.Range(-currentGun.spreadFactor, currentGun.spreadFactor);
                    fwd.y += Random.Range(-currentGun.spreadFactor, currentGun.spreadFactor);
                    fwd.z += Random.Range(-currentGun.spreadFactor, currentGun.spreadFactor);
                    Vector3 forward = fwd * range;
                    Debug.DrawRay(transform.position, forward, Color.red, 10);
                    RaycastHit hit;
                    if (Physics.Raycast(cam.transform.position, forward, out hit, range))
                    {
                        Enemy target = hit.transform.GetComponent<Enemy>();
                        if (target != null)
                        {
                            target.DamageServerRpc(currentGun.dmg);

                        }
                    }
                }
            }
        }
    }
}
