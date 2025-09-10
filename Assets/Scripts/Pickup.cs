using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public GameObject socket;
    public GameObject text;
    public PlayerAttack playerAttack;
    public PlayerInventory playerInventory;
    public Sprite sprite;
    public string weapon;
    bool found = false;
    bool pickedUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.SetActive(true);
        Canvas.ForceUpdateCanvases();
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(found && !pickedUp && Input.GetKeyDown(KeyCode.Space))
        {
            playerInventory.AddItem(sprite);
            InitPickup();
        }

        if(!pickedUp)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 75.0f);
        }
    }

    private void InitPickup()
    {
        pickedUp = true;
        transform.parent = socket.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        text.SetActive(false);
        playerAttack.PickupWeapon(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !pickedUp)
        {
            found = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && !pickedUp)
        {
            found = false;
            text.SetActive(false);
        }
    }
}
