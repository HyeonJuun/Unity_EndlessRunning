using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameover;
    public GameObject gameover_pannel;

    public static bool isGameStarted;
    public GameObject start_text;

    public static int number_of_coins;
    public Text coins_text;
    void Start()
    {
        gameover = false;
        Time.timeScale = 1;
        isGameStarted = false;
        number_of_coins = 0;
    }

    void Update()
    {
        if(gameover)
        {
            Time.timeScale = 0;
            gameover_pannel.SetActive(true);
        }
        coins_text.text = "Coins :" + number_of_coins;
        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(start_text);
        }

    }
}
