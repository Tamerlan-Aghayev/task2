using UnityEngine;
using TMPro;
using System.Collections;

public class ball : MonoBehaviour
{
    public TextMeshProUGUI wallText;
    private bool isDisplayingText = false;

    void Start()
    {
        // Ensure wallText is assigned in the Unity Editor or dynamically.
        if (wallText == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned to wallText. Please assign it in the Unity Editor.");
        }
    }

    void Update()
    {
        // Nothing to update in Update for now.
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (wallText != null && !isDisplayingText &&
            (collision.gameObject.name == "on" || collision.gameObject.name == "arxa" || collision.gameObject.name == "sag" || collision.gameObject.name == "sol"))
        {
            isDisplayingText = true;
            StartCoroutine(DisplayWallText("Wall: " + collision.gameObject.name, 1.0f));
            Debug.Log("Ball collided with wall: " + collision.gameObject.name);
        }
    }

    IEnumerator DisplayWallText(string text, float duration)
    {
        wallText.text = text;
        yield return new WaitForSeconds(duration);
        wallText.text = "";
        isDisplayingText = false;
    }
}
