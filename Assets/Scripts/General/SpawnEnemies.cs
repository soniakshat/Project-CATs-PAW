using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// ####################### //
// Script to Spawn Enemies //
// ####################### //

public class SpawnEnemies : MonoBehaviour
{
    private ScoreManager _sm;

    public int _numberofenemies = 1; // Default enemy number 1 but is defined in the editor
    [SerializeField] private GameObject _ep; // enemy model to instantiate
    GameObject[] EnemyCount;
    public int EnemyDeathCount;
    [SerializeField] private float _floorLength = 100; // define the length of the ground
    private float _randomspawnradius, _playerradius = 50;
    private Vector3 _PlayerPos;

    private float _randomRadiusSelector;
    [SerializeField] private Transform[] SpawnLocations;


    private void Awake()
    {
        //_numberofenemies = SceneManager.GetActiveScene().buildIndex * 3;
        _PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        _randomspawnradius = (_floorLength / 2) - _playerradius;
        _sm = GetComponent<ScoreManager>();
    }

    void Start()
    {
        _sm.enabled = false;
        StartCoroutine(SpawnEnemiesOnPlayArea());
    }

    void Update()
    {
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyDeathCount = _numberofenemies - EnemyCount.Length;
    }
    IEnumerator SpawnEnemiesOnPlayArea()
    {

        yield return new WaitForSeconds(2);
        if (_numberofenemies == SpawnLocations.Length)
        {
            for (byte i = 0; i < _numberofenemies; i++)
            {
                _randomRadiusSelector = Random.Range(0, i);
                if (_randomRadiusSelector < (i / 2))
                {
                    _playerradius = -_playerradius;
                }
                // Instantiate enemies at given position Vector
                //Instantiate(_ep, new Vector3( _PlayerPos.x + _playerradius + Random.Range(-_randomspawnradius, _randomspawnradius), 2.5f, _PlayerPos.x + Random.Range(-_playerradius, _playerradius)+ Random.Range(-_randomspawnradius, _randomspawnradius)), Quaternion.identity);
                Instantiate(_ep, new Vector3(SpawnLocations[i].position.x, SpawnLocations[i].position.y, SpawnLocations[i].position.z), Quaternion.identity);
            }
        }
        else{
            Debug.Log($"Not enough location for enemy spawning\nNumber of Enemies: {_numberofenemies}\tNumber of Spawn Locations: {SpawnLocations.Length}");
            
        }
        _sm.enabled = true;
    }
}
