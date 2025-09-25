using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrapStartLoad : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(0);
    }
    
}
