using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button restartButton;
    private GameController gameManager;

    // Start is called before the first frame update
    void Start()
    {
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(RestartGame);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RestartGame()
    {
        gameManager.StartGame();


        //Use the below line if we want to go back to the start screen
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
