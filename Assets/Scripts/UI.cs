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

        UpdateScoreText();
        UpdateRecordText();
    }

    public void EnableMainMenu()
    {
        StartCoroutine(WaitWhileCameraIsMoving());

        IEnumerator WaitWhileCameraIsMoving()
        {
            playButton.gameObject.SetActive(true);
            playButton.interactable = false; // Disabling the ability to press the button
            quitButton.gameObject.SetActive(true);
            recordText.enabled = true;
            titleText.enabled = true;

            // Waiting before enable the ability to click on a button.
            // It solves the problem of camera latching during fast click on play button.
            yield return new WaitForSeconds(3);  

            // Enable the ability to click on a button.
            playButton.interactable = true;
        }
    }

    public void DisableMainMenu()
    {
        StartCoroutine(HideMessegeAfterDelay());

        IEnumerator HideMessegeAfterDelay()
        {
            yield return new WaitForSeconds(2);
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
            yield return new WaitForSeconds(2);
            gameOverText.enabled = false;
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = $"Score: {_spawnManager.score}";
    }

    public void UpdateRecordText()
    {
        recordText.text = $"Record: {_spawnManager.record}";
    }
}
