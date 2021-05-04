using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public InputField username;
    public Slider timeSlider;
    public Dropdown lives;

    public bool check = false;

    public static int startLife;
    private int dropLife;
    public void StartGame()
    {
        SceneManager.LoadScene("2Game");

        string user = username.GetComponent<InputField>().text;
        PlayerPrefs.SetString("username", user);

        PlayerPrefs.SetFloat("time", timeSlider.value);

        PlayerPrefs.SetInt("lives", lives.value);

        if(check == false)
        {
            startLife = 9;
        } else
        {
            startLife = dropLife;
        }
    }

    public void Drop()
    {
        switch (lives.value)
        {
            default:
                dropLife = 9;
                check = true;
                break;
            case 1:
                dropLife = 1;
                check = true;
                break;
            case 2:
                dropLife = 2;
                check = true;
                break;
            case 3:
                dropLife = 3;
                check = true;
                break;
            case 4:
                dropLife = 4;
                check = true;
                break;
            case 5:
                dropLife = 5;
                check = true;
                break;
            case 6:
                dropLife = 6;
                check = true;
                break;
            case 7:
                dropLife = 7;
                check = true;
                break;
            case 8:
                dropLife = 8;
                check = true;
                break;
            case 9:
                dropLife = 9;
                check = true;
                break;
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("3Exit");
    }

    public void Replay()
    {
        SceneManager.LoadScene("1Intro");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }


}
