using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;

    private void Start()
    {
        deathPanel.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
