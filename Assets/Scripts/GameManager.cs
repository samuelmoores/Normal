using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int killCount = 0;
    bool playerWon = false;

    public static GameManager Instance;
    
    GameObject killCountUI;
    TextMeshProUGUI killCountUItext;

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void KillZombie()
    {
        killCount++;
        Score_UI.Instance.SetScoreText(killCount.ToString());

        if (killCount == 1)
        {
            Score_UI.Instance.Show();
        }

        if(killCount == 5)
        {
            PlayerMovement.Instance.Win();
            playerWon = true;
        }
    }

    public bool HasWon()
    {
        return playerWon;
    }

    public int GetKillCount()
    {
        return killCount;
    }
}
