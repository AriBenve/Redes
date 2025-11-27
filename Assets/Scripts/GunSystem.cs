using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GunSystem : MonoBehaviour
{
    delegate void ShootDelegate();
    ShootDelegate shoot;
    
    [Header("Gun Stats")]
    public int dmg;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int _bulletsLeft, _bulletsShot;

    [Header("Bools")]
    bool _shooting, _readyToShoot, _reloading;
    public Animator _animator;

    [Header("References")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    [Header("Graphics")]
    public GameObject muzzleFlash;
    public float camShakeMagnitude, camShakeDuration;
    public CameraShake camShake;
    public TextMeshProUGUI text;


    [Header("Audio")]
    public List<AudioClip> audioClipList = new List<AudioClip>();
    public AudioSource audioSource;

    private void Awake()
    {
        _bulletsLeft = magazineSize;
        _readyToShoot = true;
        audioSource = GetComponent<AudioSource>();
        shoot = Shoot;
    }


    private void Start()
    {
        text = GameObject.Find("AmmoCount").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {

        _MyInput();

        text.SetText(_bulletsLeft + " / " + magazineSize);
    }

    private void _MyInput()
    {
        if(allowButtonHold) _shooting = Input.GetKey(KeyCode.Mouse0);
        else _shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && _bulletsLeft < magazineSize && !_reloading) Reload();

        int i = Random.Range(0, audioClipList.Count);


        if (_readyToShoot && _shooting && !_reloading && _bulletsLeft > 0)
        {
            _bulletsShot = bulletsPerTap;
            audioSource.PlayOneShot(audioClipList[i], 0.7f);
            shoot();
        }
            
    }

    private void Shoot()
    {
        _readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x,y,0);

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            if(rayHit.collider.CompareTag("Player"))
                rayHit.collider.GetComponent<Player>().Damage(dmg);
        }

        var flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        flash.transform.parent = attackPoint.transform;
        flash.GetComponent<ParticleSystem>().Play();
        flash = null;

        _bulletsLeft--;
        _bulletsShot--;

        StartCoroutine(camShake.Shake(camShakeMagnitude, camShakeDuration));

        Invoke("ResetShot", timeBetweenShooting);

        if (_bulletsShot > 0 && _bulletsShot > 0)
            Invoke("Shoot", timeBetweenShots);

        _animator.SetTrigger("ataque");

    }

    private void ResetShot()
    {
        _readyToShoot = true;
    }

    private void Reload()
    {
        _reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        _bulletsLeft = magazineSize;
        _reloading = false;
    }
}
