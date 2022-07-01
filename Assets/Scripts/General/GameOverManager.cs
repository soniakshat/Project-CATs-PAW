using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;  // game over panel to show 
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            DisplayGameOver();
        }
    }

    public void DisplayGameOver() // Display Gameover if player not found
    {
        GameOverPanel.SetActive(true);
    }
}
