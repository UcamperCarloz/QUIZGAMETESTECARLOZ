using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class DataController : MonoBehaviour
{

    private RoundData [] todasAsRodadas; //era publico antes de crear el script GameData, despues decrearlo, llamar la bblioteca .IO lo volvimos privado.

    private int rodadaIndex;
    private int playerHighScore;
    private string gameDataFileName = "data.json";
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadGameData(); //Se colocó como parte de la creación del JSON
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {}
    public void SetRoundData(int round)
    {
        rodadaIndex = round;
    }

    public RoundData GetCurrentRoundData()
    {
        return todasAsRodadas[rodadaIndex];
    }

    private void LoadGameData() //Se colocó como parte de la creación del JSON
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            todasAsRodadas = loadedData.todasAsRodadas;
        }
        else
        {
            Debug.LogError("No fue posible cargar datos!");
        }
    }

     public void EnviarNovoHighScore (int newScore)
    {
        if(newScore > playerHighScore)
        {
            playerHighScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighScore()
    {
        return playerHighScore;
    }
    private void LoadPlayerProgress()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            playerHighScore = PlayerPrefs.GetInt("highscore");
        }
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highScore", playerHighScore);
    }


}
