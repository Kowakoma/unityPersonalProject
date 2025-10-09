using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI recordText;
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playButton.onClick.AddListener(_spawnManager.StartGame);
        gameOverText.enabled = false;
    }

    public void EnableMainMenu()
    {
        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        recordText.enabled = true;
        titleText.enabled = true;
    }

    public void DisableMainMenu()
    {
        StartCoroutine(HideMessegeAfterDelay());

        IEnumerator HideMessegeAfterDelay()
        {
            yield return new WaitForSeconds(1);
            playButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            recordText.enabled = false;
            titleText.enabled = false;
        }
    }

    public void EnableGameOverMessage()
    {
        gameOverText.enabled = true;
    }
    public void DisableGameOverMessage()
    {
        StartCoroutine(HideMessageAfterDelay());

        IEnumerator HideMessageAfterDelay()
        {
            yield return new WaitForSeconds(1);
            gameOverText.enabled = false;
        }
    }
}
