using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private DataController data;
    void Start()
    {
        data = FindObjectOfType<DataController>();
    }

    // Update is called once per frame
   public void StartGame(int round)
   {
    data.SetRoundData(round);
    SceneManager.LoadScene("Game");
   }
}
