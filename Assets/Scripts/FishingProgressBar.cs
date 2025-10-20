using System.Collections;
using UnityEngine;

public class FishingProgressBar : MonoBehaviour
{   
    public Transform fillTransform;
    private Vector3 originalScale;
    private Quaternion initialRotation;
    private bool _isBarRequested;
    private float _currentFishingProgress = 0;

    void Start()
    {
        initialRotation = transform.rotation;
        originalScale = fillTransform.localScale;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_isBarRequested)
        {
            CalculateCurrentFishProgress();
            SetProgress(_currentFishingProgress);
        }
    }

    void LateUpdate()
    {
        transform.rotation = initialRotation;
    }

    public void ShowBar()
    {
        _isBarRequested = true;
        gameObject.SetActive(true);
    }

    public void HideBar()
    {
        _isBarRequested = false;
        gameObject.SetActive(false);
        ResetProgress();
    }

    public void SetProgress(float progress)
    {
        Vector3 newScale = originalScale;
        newScale.x = originalScale.x * progress;
        fillTransform.localScale = newScale;
    }

    public void ResetProgress()
    {
        _currentFishingProgress = 0;
        SetProgress(0);
    }

    float CalculateCurrentFishProgress()
    {
        _currentFishingProgress += Time.deltaTime;
        return _currentFishingProgress;
    }
}
