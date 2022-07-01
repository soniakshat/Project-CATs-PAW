using System.Collections;
using UnityEngine;
using System;

// ########################################## //
// Script to control the motion of projectile //
// ########################################## //

public class ProjectileMotion : MonoBehaviour
{
    [SerializeField] private float _proSpeed = 35; // projectile speed
    private float _DestroyAfterSeconds = 10f;
    [SerializeField] GameObject GameBarrel;
    private Rigidbody _rb, _orb;
    //private float _orecoilForce = 50, _oangleofrecoil;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (_proSpeed != 0)
        {
            //transform.position += transform.forward * _proSpeed * Time.deltaTime;
            //_rb.AddRelativeForce(Vector3.forward * _proSpeed, ForceMode.Impulse); // throw projectile with impulse force
            _rb.MovePosition(this.transform.position + (_rb.transform.forward * Time.deltaTime ));
            StartCoroutine(DestroyProjectile());
        }
        else
        {
            Debug.Log("no speed of projectile");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        _orb = other.gameObject.GetComponent<Rigidbody>();
        //_orb.AddRelativeForce(new Vector3(0, _orecoilForce * (float)Math.Cos(_oangleofrecoil), _orecoilForce * (float)Math.Sin(_oangleofrecoil)), ForceMode.Impulse);
        Destroy(this.gameObject); // if projectile hit any collider then it gets destroyed
    }

    IEnumerator DestroyProjectile(){
        yield return new WaitForSeconds(_DestroyAfterSeconds);
        Destroy(this.gameObject);
    }
}
