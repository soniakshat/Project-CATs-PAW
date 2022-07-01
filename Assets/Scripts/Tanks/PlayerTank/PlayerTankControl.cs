using System.Collections.Generic;
using UnityEngine;
using System.Collections;

// #################################### //
// Script to control player tank motion //
// #################################### //

public class PlayerTankControl : MonoBehaviour
{
    public float _dirZ, _dirY;
    private Rigidbody _rb;
    [SerializeField] private float _PlayertankSpeed = 15;
    [SerializeField] private GameObject _GOP, _Engine;
    [SerializeField] private GameObject _PlayerCarModel;
    private AudioSource _EngineAudioSource;
    
    private void Awake()
    {
        _EngineAudioSource = _Engine.GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        // GetUserInput();
    }

    private void FixedUpdate()
    {
        MovePlayerTank();
    }

    void GetUserInput()
    {
        _dirZ = Input.GetAxis("Vertical");
        _dirY = Input.GetAxis("Horizontal");
    }

    void MovePlayerTank()
    {
        //transform.Translate(new Vector3(0, 0, _dirZ * _PlayertankSpeed) * Time.deltaTime * 2, Space.Self);
        _rb.MovePosition(this.transform.position + (_rb.transform.forward * _dirZ * Time.deltaTime * _PlayertankSpeed));
        transform.Rotate(new Vector3(0, _dirY, 0), Space.Self);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("OuterBounds"))
        {
            if (_GOP.activeInHierarchy == false)
            {

                StartCoroutine(YouLoseAudioPlayer());
                _dirY=0;
                _dirZ=0;

                // this.gameObject.GetComponentInParent<PlayerTankProjectileSpawner>().enabled = false;
                // this.gameObject.GetComponentInParent<PlayerTankControl>().enabled = false;
                // GameObject.FindGameObjectWithTag("OnScreenControls").SetActive(false);
            }
        }
    }

    IEnumerator YouLoseAudioPlayer()
    {
        _GOP.SetActive(true);
        _GOP.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(_GOP.GetComponent<AudioSource>().clip.length);
        StartCoroutine(PlayerExplode());
    }



    [SerializeField] private GameObject _explosionParticleSystem;

    IEnumerator PlayerExplode()
    {
        this.gameObject.SetActive(false);
        Instantiate(_explosionParticleSystem, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0);
    }
}