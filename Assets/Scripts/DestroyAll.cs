using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    [SerializeField]private GameObject panel;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }  
}