using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadNewScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
