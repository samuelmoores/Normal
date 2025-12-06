using TMPro;
using UnityEngine;

public class Score_UI : MonoBehaviour
{
    public static Score_UI Instance;
    public GameObject text_score;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetScoreText(string message)
    {
        text_score.GetComponent<TextMeshProUGUI>().text = message;
    }

}
