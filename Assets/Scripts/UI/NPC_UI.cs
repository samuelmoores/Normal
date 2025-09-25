using TMPro;
using UnityEngine;

public class NPC_UI : MonoBehaviour
{
    public static NPC_UI Instance;
    public GameObject text_interact;

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

    public void SetNPCText(string message)
    {
        text_interact.GetComponent<TextMeshProUGUI>().text = message;
    }

}
