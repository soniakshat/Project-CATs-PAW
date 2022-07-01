using System.Collections;
using UnityEngine;

public class InstructionsTimeOut : MonoBehaviour
{
    [SerializeField] GameObject MoveInstructionPanel, LookInstructionPanel;
    void Start()
    {
        StartCoroutine(StopDisplay());
    }

    IEnumerator StopDisplay()
    {
        MoveInstructionPanel.SetActive(true);
        LookInstructionPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        MoveInstructionPanel.SetActive(false);
        LookInstructionPanel.SetActive(false);
    }
}
