using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    public void OnStartLevel(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}
