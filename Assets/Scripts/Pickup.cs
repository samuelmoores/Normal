using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject socket;
    bool found = false;
    bool pickedUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(found && Input.GetKeyDown(KeyCode.Space))
        {
            pickedUp = true;
            transform.parent = socket.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        Debug.Log(transform.localPosition);

        if(!pickedUp)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 50.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            found = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            found = false;
        }
    }
}
