using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventory;
    public List<GameObject> itemImages;
    int numItems = 0; 
    int activeItem = 0;

    private void Start()
    {
        inventory.gameObject.SetActive(true);
        Canvas.ForceUpdateCanvases();
        inventory.gameObject.SetActive(false);
        SetActiveItem(activeItem);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeSelf);
        }
    }

    public void AddItem(Sprite pickupSprite)
    {
        if(numItems < itemImages.Count)
        {
            itemImages[++numItems].GetComponent<Image>().sprite = pickupSprite;
            SetActiveItem(numItems);
        }
    }

    public void SetActiveItem(int item)
    {
        itemImages[activeItem].transform.parent.GetComponent<Image>().color = Color.black;
        activeItem = item;
        itemImages[item].transform.parent.GetComponent<Image>().color = Color.red;
    }
}
