using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

// ################### //
// Script of main menu //
// ################### //

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider CamSenSlider;
    [SerializeField] private Text CamSenDisplayText;
    private int setCamSen;
    private float sliderval;
    
    private void Start()
    {
        CamSenSlider.minValue = 30;
        CamSenSlider.maxValue = 100;
        CamSenSlider.value = 50;
        CamSenSlider.value = PlayerPrefs.GetInt("CameraSensitivity", 50);
    }
    private void Update()
    {
        sliderval = CamSenSlider.value;
        
        // print("Slider Val: " + sliderval);
        setCamSen = (int)Mathf.Clamp(sliderval, CamSenSlider.minValue, CamSenSlider.maxValue);
        // print("Clamped value: " + setCamSen);
        CamSenDisplayText.text = setCamSen.ToString();
        PlayerPrefs.SetInt("CameraSensitivity", (int)setCamSen);
        // print("CamSen in PlayerPref: " + PlayerPrefs.GetFloat("CameraSensitivity"));
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // load 1st scene 
    }

    public void QuitGame()
    {
        Application.Quit(); // Exit application
    }
}
