using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTrap : MonoBehaviour
{
    public GameObject spikeVisual;
    public float activeTime = 1f;
    public float idleTime = 2f;

    public float startHeight = 0.5f;

    private bool isActive = true;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (isActive && timer >= activeTime)
        {
            SetSpikeTrap(false);
        }
        else if (!isActive && timer >= idleTime)
        {
            SetSpikeTrap(true);
        }
    }

    private void SetSpikeTrap(bool state)
    {
        isActive = state;
        timer = 0;
        
        Vector3 currentPos = spikeVisual.transform.localPosition;

        currentPos.y = state ? startHeight : -startHeight;
        spikeVisual.transform.localPosition = currentPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("Player hit by spike trap!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
