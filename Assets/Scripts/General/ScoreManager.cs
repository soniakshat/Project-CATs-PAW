using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

// ###################### //
// Script to manage score //
// ###################### //

public class ScoreManager : MonoBehaviour
{
    //[SerializeField] public Text _scoreCard; // get scorecard UI Text Element
    [SerializeField] public TextMeshProUGUI _scoreCard;
    [SerializeField] private Text FinalScore; // get Final Score UI Text Element to show after enemy dies
    [SerializeField] private GameObject FinalPanel; // get the final panel to show after enemy or player dies
    [SerializeField] private GameObject _PlayerControlPanel;

    private SpawnEnemies _spe; // Spawn Enemies script reference
    private int _Score; // int variable to manage score
    
    void Start()
    {
        _spe = GetComponent<SpawnEnemies>(); // set reference to script
    }


    void Update()
    {
        _scoreCard.text = "Score: " + _spe.EnemyDeathCount; // update the score card according to death of enemy
        FinalScore.text = $"Final Score: {_spe.EnemyDeathCount}"; // update the final score card according to scorecard
        if (_spe.EnemyDeathCount == _spe._numberofenemies && FinalPanel.activeInHierarchy == false)
        {
            StartCoroutine(LevelCompleteAudio());
        }
    }

    IEnumerator LevelCompleteAudio()
    {
        //GameObject.FindGameObjectWithTag("OnScreenControls").SetActive(false);
        yield return new WaitForSeconds(0.65f);
        FinalPanel.SetActive(true);
        _PlayerControlPanel.SetActive(false);
        FinalPanel.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(FinalPanel.GetComponent<AudioSource>().clip.length);
    }
}
