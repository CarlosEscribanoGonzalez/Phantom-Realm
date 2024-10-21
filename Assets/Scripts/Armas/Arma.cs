using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    //Variables de la pistola:
    [SerializeField] protected float maxBullets;
    protected float currentBullets;
    protected float totalBullets;
    public bool canShoot = true;
    public bool canReload = true;
    [SerializeField] protected float reloadTime;

    //Variables de la bala:
    [SerializeField] protected Transform initialBulletTransform;
    [SerializeField] protected GameObject BulletPrefab;
    [SerializeField] protected float BulletSpeed;

    protected AudioManager audioManager;
    protected Camera cam;
    public bool canBeActive;
    public string weaponName;
    [SerializeField] protected bool gunOnFloor;


    private void Awake()
    {
        currentBullets = maxBullets;
        totalBullets = maxBullets * 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cam = GameObject.Find("FP Camera").GetComponent<Camera>();
    }

    public float GetTotalBullets() { return totalBullets; }
    public float GetCurrentBullets() { return currentBullets; }
    public float GetMaxBullets() { return maxBullets; }
    public void SetTotalBullets(float bulletsToSet) { this.totalBullets = bulletsToSet; }
}
