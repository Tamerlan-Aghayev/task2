using System.Collections;
using UnityEngine;
using TMPro;  // Added the TextMeshPro namespace

public class goal : MonoBehaviour
{
    public GameObject ball;
    public TextMeshProUGUI goalText;  // Reference to the TextMeshProUGUI component
    private int goals = 0;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            goals++;
            StartCoroutine(ShowGoalAlert());
            RespawnBall(other.gameObject);
        }
    }

    private IEnumerator ShowGoalAlert()
    {
        goalText.text = "Gooooaaaal! Goals: " + goals;
        yield return new WaitForSeconds(1.0f);
        goalText.text = "";
    }

    private void RespawnBall(GameObject ballObject)
    {
        Vector3 respawnPosition = new Vector3(-0.33f, 2.31f, -3.19f);
        ballObject.transform.position = respawnPosition;

        Rigidbody ballRigidbody = ballObject.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
