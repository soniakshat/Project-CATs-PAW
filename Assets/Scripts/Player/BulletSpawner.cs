using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _BarrelPoint;
    [SerializeField] private Transform _CrossHair;
    private Vector3 _bulletVector;
    [SerializeField] private GameObject _BulletPrefab;
    [SerializeField] private GameObject _Player;
    
    private bool _canShoot = true;
    
    [SerializeField] private float _fireRate = 0.75f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnProj();
        }
    }


    public void SpawnProj()
    {
        if (_canShoot)
        {
            StartCoroutine(SpawnProjectile());
        }
    }
    
    public IEnumerator SpawnProjectile()
    {
        GameObject bullet;
        if (_BarrelPoint != null)
        {
            _bulletVector = _CrossHair.position - _BarrelPoint.transform.position;
            // print("Bullet Vector: " + _bulletVector);
            bullet = Instantiate(_BulletPrefab, _BarrelPoint.transform.position, _BarrelPoint.transform.rotation);
            bullet.transform.localRotation = _Player.transform.localRotation;
            bullet.GetComponent<Rigidbody>().AddForce(_bulletVector, ForceMode.Impulse);
            StartCoroutine(AutoDestroyProjectile(bullet));
            _canShoot = false;   
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