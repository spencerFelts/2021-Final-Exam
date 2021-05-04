using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Gameplay : MonoBehaviour
{
    [SerializeField]
    public Text playerName, livesLeft, totalPoints;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource music;
    public Toggle musicToggle;

    public int lives;
    public static int points = 0;
    public static string username;

    public void Start()
    {
        string username = PlayerPrefs.GetString("username");
        playerName.text = "Currently playing: " + username;

        int startLives = PlayerPrefs.GetInt("lives");
        livesLeft.text = startLives.ToString();

        totalPoints.text = points.ToString();
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void IncreasePoints()
    {
        points++;
        totalPoints.text = points.ToString();
    }

    public void DecreasePoints()
    {
        points--;
        totalPoints.text = points.ToString();
    }

    public void IncreaseLives()
    {
        lives++;
        livesLeft.text = lives.ToString();
    }

    public void DecreaseLives()
    {
        lives--;
        livesLeft.text = lives.ToString();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("2Game");
        Resume();
    }

    public void AudioSetting()
    {
        if (musicToggle.isOn)
        {
            music.Play();
        }
        else
        {
            music.Stop();
        }

    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.lives = lives;
        save.score = points;
        save.username = username;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        points = 0;
        lives = 0;
        username = "";

        Debug.Log("Game Saved");
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            totalPoints.text = save.score.ToString();
            playerName.text = save.username;
            livesLeft.text = save.lives.ToString();
            points = save.score;
            username = save.username;
            lives = save.lives;

            Debug.Log("Game Loaded");

            Resume();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SaveAsJson()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as Json: " + json);
    }
}

