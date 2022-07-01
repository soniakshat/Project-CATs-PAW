using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    // Canvas
    [SerializeField] GameObject _GameOverPanel;
    [SerializeField] private Text _bulletCount;

    // Gun Stats
    [SerializeField] private float _gunDamage = 10;
    [SerializeField] private float _gunRange = 100;
    [SerializeField] private uint _NBullets = 25;
    
    // Some imp locations
    [SerializeField] private Camera _playerCamera;
    //[SerializeField] private GameObject RaySource; 
    
    private void Awake()
    {
        _bulletCount.text = "Bullets: " + _NBullets;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _gunRange);
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _gunRange) && _NBullets > 0)
        {
            Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward*_gunRange, Color.red, 0.25f);
            _NBullets--;
            print($"Bullets remaining: {_NBullets}");
            _bulletCount.text = "Bullets: " + _NBullets;
            //TargetHealthChange my_target = hit.transform.GetComponent<TargetHealthChange>();
            // if (my_target != null)
            // {
            //     my_target.TakeDamage(_gunDamage);
            // }
            print($"{hit.transform.name}");
        }
        else if(_NBullets == 0)
        {
            print("out of bullets");
        }
        else
        {
            //_GameOverPanel.SetActive(true);
            StartCoroutine(ExitEditor());
            print("Ray not drawn");
        }
    }
    IEnumerator ExitEditor()
    {
        yield return new WaitForSeconds(2);
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}