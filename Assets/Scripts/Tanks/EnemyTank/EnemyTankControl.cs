using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ################################### //
// Script to control enemy tank motion //
// ################################### //

public class EnemyTankControl : MonoBehaviour
{

    private GameObject Player; // get player
    private Vector3 _PlayerPos; // for player position
    private float _EnemyMaxHealth;
    private float _enemyTankHealth; // current health
    private float _damageByPlayer;
    [SerializeField] Slider sli; // health slider UI
    private float dist; // distance from player
    private Rigidbody _rb;
    private float _EnemytankSpeed = 3;
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private AudioClip[] _enemyTankAudioClips;
    [SerializeField] private AudioSource _EnemyOtherEffects;
    private Vector3 m;
    private float maxMovingDist;
    [SerializeField] private float[] MaxHealth, BulletRequired;

    private float enemySpeedDivider = 1.95f;


    private void Awake()
    {
        // default fail safe
        _EnemyMaxHealth = 100;
        _damageByPlayer = 30;

        // health and damage according to level
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _EnemyMaxHealth = MaxHealth[0];
            _damageByPlayer = _EnemyMaxHealth / BulletRequired[0];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            _EnemyMaxHealth = MaxHealth[1];
            _damageByPlayer = _EnemyMaxHealth / BulletRequired[1];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            _EnemyMaxHealth = MaxHealth[2];
            _damageByPlayer = _EnemyMaxHealth / BulletRequired[2];
        }

        _enemyTankHealth = _EnemyMaxHealth;
        sli.value = _enemyTankHealth;
        
        _rb = GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player");
        _EnemyOtherEffects = GetComponent<AudioSource>();

        // Enemy Speed according to 
        _EnemytankSpeed = SceneManager.GetActiveScene().buildIndex/enemySpeedDivider;
    }
    void Start()
    {
        _PlayerPos = Player.transform.position; // get player position from scene
    }

    void Update()
    {
        _PlayerPos = Player.transform.position; // update player position from scene
        dist = Vector3.Distance(_PlayerPos, this.transform.position); // calculate distance from player
        maxMovingDist = 0.05f * _EnemytankSpeed;
        UpdateHealthSlider(); // update health UI Slider
    }


    // [SerializeField] private float _viewRange = 100; // view range 
    private void FixedUpdate()
    {
        this.transform.LookAt(Player.transform);
        m = Vector3.MoveTowards(this.transform.position, Player.transform.position, maxMovingDist);
        _rb.MovePosition(m);
    }

    private void OnCollisionEnter(Collision other) // if projectile hits enemy => decrease health
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            HitByProjectile();
        }
        if (other.gameObject.CompareTag("OuterBounds"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            HitByProjectile();
        }
    }

    private void UpdateHealthSlider()
    {
        sli.maxValue = _EnemyMaxHealth;
        sli.value = _enemyTankHealth;
        sli.transform.LookAt(Camera.main.transform);
        sli.transform.Rotate(0, 180, 0);
    }

    private void HitByProjectile()
    {
        _EnemyOtherEffects.clip = _enemyTankAudioClips[0];
        _EnemyOtherEffects.Play();
        _enemyTankHealth -= _damageByPlayer;
        //yield return new WaitForSeconds(_enemyTankAudioClips[0].length);
        if (_enemyTankHealth <= 0.5f)
        {
            sli.gameObject.SetActive(false);
            StartCoroutine(ExplosionEffect());
        }
    }

    IEnumerator ExplosionEffect()
    {
        // Enable the below 3 line to add enemy about to explode audio 
        // _EnemyOtherEffects.clip = _enemyTankAudioClips[1];
        // _EnemyOtherEffects.Play();
        // yield return new WaitForSeconds(_enemyTankAudioClips[1].length);
        
        Destroy(this.gameObject);
        Instantiate(_explosionParticles, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0);
    }
}