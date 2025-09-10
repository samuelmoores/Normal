using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Statue : MonoBehaviour
{
    public GameObject text;
    public GameObject textObject;
    public PlayerMovement player;
    public int scene;
    bool found = false;

    private void Start()
    {
        text.SetActive(true);
        Canvas.ForceUpdateCanvases();
        text.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && found)
        {
            textObject.GetComponent<TextMeshProUGUI>().text = "Transporting to new world";
            StartCoroutine(LoadLevel(3.0f));
        }
    }

    IEnumerator LoadLevel(float duration)
    {
        float current = 0.0f;
        player.Freeze();

        while (current < duration)
        {
            current += Time.deltaTime;
            textObject.GetComponent<TextMeshProUGUI>().text = "Transporting to new world";
            yield return null;
        }

        text.SetActive(false);
        player.UnFreeze();

        SceneManager.LoadScene(scene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
            found = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
            found = false;
        }
    }
}
