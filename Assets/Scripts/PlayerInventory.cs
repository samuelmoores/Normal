using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventory;
    public List<GameObject> inventoryImages;
    int numItems = 0; 
    int activeItem = 0;

    private void Start()
    {
        inventory.SetActive(true);
        Canvas.ForceUpdateCanvases();
        inventory.SetActive(false);
        SetActiveItem(activeItem);
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
        if(numItems < inventoryImages.Count)
        {
            inventoryImages[++numItems].GetComponent<Image>().sprite = pickupSprite;
            SetActiveItem(numItems);
        }
    }

    public void SetActiveItem(int item)
    {
        inventoryImages[activeItem].transform.parent.GetComponent<Image>().color = Color.black;
        activeItem = item;
        inventoryImages[item].transform.parent.GetComponent<Image>().color = Color.red;
    }
}
