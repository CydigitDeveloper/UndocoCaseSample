using UnityEngine;

public class ChestCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Submarine"))
        {
            QuizManager manager = FindObjectOfType<QuizManager>();
            
            if (manager != null)
            {
                manager.OnChestCollected();
            }
            
            Destroy(gameObject);
        }
    }
}
