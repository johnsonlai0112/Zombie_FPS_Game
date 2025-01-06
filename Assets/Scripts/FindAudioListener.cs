using UnityEngine;

public class FindAudioListener : MonoBehaviour
{
    void Start()
    {
        // Find all AudioListener components in the scene
        AudioListener[] audioListeners = Object.FindObjectsOfType<AudioListener>();

        // Check the number of AudioListeners
        int audioListenerCount = audioListeners.Length;

        if (audioListenerCount == 0)
        {
            Debug.Log("No AudioListener components found in the scene.");
        }
        else if (audioListenerCount == 1)
        {
            Debug.Log("One AudioListener component found in the scene.");
        }
        else
        {
            Debug.Log(audioListenerCount + " AudioListener components found in the scene. Please ensure there is only one.");

            // Output the names of GameObjects with AudioListeners
            foreach (var audioListener in audioListeners)
            {
                Debug.Log("AudioListener found on GameObject: " + audioListener.gameObject.name);
            }
        }
    }
}
