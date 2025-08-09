using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1; // Value awarded for passing this trigger
    private bool hasScored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasScored) return;

        if (other.CompareTag("Player"))
        {
            hasScored = true;
            UIManager.Instance.AddScore(scoreValue); // Use the custom value
        }
    }
}
