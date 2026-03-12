using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CreditsManager : MonoBehaviour
{
    public float autoReturnTime = 10f;
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= autoReturnTime)
        {
            GoToMenu();
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            GoToMenu();
        }
    }

    void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}