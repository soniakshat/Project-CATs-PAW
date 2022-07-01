using System;
using System.Collections;
using UnityEngine;

public class PlayerTankProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _firePointPlayerTank;
    [SerializeField] private GameObject _PlayerTankProjectile;
    [SerializeField] private GameObject _PlayerTank;
    private bool _canShoot = true;
    private float _recoilForce = 50, _angleofrecoil;
    private Rigidbody _rbp;
    [SerializeField] private float _fireRate = 0.75f;

    private void Awake()
    {
        _rbp = _PlayerTank.GetComponent<Rigidbody>();
        _angleofrecoil = 3.14159f / 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Pressed Spacebar");
            SpawnProj();
        }
    }


    public void SpawnProj()
    {
        if (_canShoot)
        {
            StartCoroutine(SpawnProjectile());
            _PlayerTank.GetComponent<AudioSource>().Play();
        }
    }
    public IEnumerator SpawnProjectile()
    {
        GameObject shell;
        if (_firePointPlayerTank != null)
        {
            shell = Instantiate(_PlayerTankProjectile, _firePointPlayerTank.transform.position, _firePointPlayerTank.transform.rotation);
            shell.transform.localRotation = _PlayerTank.transform.localRotation;
            StartCoroutine(AutoDestroyProjectile(shell));
            _canShoot = false;
            _rbp.AddRelativeForce(new Vector3(0, _recoilForce * (float)Math.Cos(_angleofrecoil), -_recoilForce * (float)Math.Sin(_angleofrecoil)), ForceMode.Impulse);
        }
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    private IEnumerator AutoDestroyProjectile(GameObject other)
    {
        yield return new WaitForSeconds(10);
        Destroy(other);
    }
}