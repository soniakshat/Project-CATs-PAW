using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NarrationToGame : MonoBehaviour
{
    [SerializeField] GameObject SkipButton;
    private GameObject _license;

    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level-ThankyouNote"))
        {
            _license = GameObject.Find("LicenseImage");
            _license.SetActive(false);
        }

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            NarrationScene();
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level-ThankyouNote"))
        {
            ThankingScene(_license);
        }
    }

    public void LoadNextScene(string sc)
    {
        if (sc == "next")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (sc == "MM")
        {
            SceneManager.LoadScene(0);
        }
    }

    void NarrationScene()
    {
        if (PlayerPrefs.GetInt("GameOncePlayed?", -1) != 1)
        {
            SkipButton.SetActive(false);
            PlayerPrefs.SetInt("GameOncePlayed?", 1);
        }
        else
        {
            SkipButton.SetActive(true);
        }
        StartCoroutine(GoForTestingWeapon());
    }

    void ThankingScene(GameObject licence_image)
    {
        StartCoroutine(DisplayLicense(licence_image));
        if (PlayerPrefs.GetInt("GameOnceCompleted?", -1) != 1)
        {
            SkipButton.SetActive(false);
            PlayerPrefs.SetInt("GameOnceCompleted?", 1);
        }
        else
        {
            SkipButton.SetActive(true);
        }
        StartCoroutine(GoToLicense());
    }

    IEnumerator GoForTestingWeapon()
    {
        yield return new WaitForSecondsRealtime(33);
        LoadNextScene("next");
    }

    IEnumerator GoToLicense()
    {
        yield return new WaitForSecondsRealtime(30);
        LoadNextScene("MM");
    }

    IEnumerator DisplayLicense(GameObject image)
    {
        yield return new WaitForSecondsRealtime(22);
        image.SetActive(true);
    }
}
