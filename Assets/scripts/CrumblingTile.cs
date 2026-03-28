using UnityEngine;

public class CrumblingTile : MonoBehaviour
{
    [Header("Timings")]
    public float fallDelay = 3.0f;
    public float shakeDelay = 0.4f;

    [Header("Shake Settings")]
    public float shakeAmount = 0.05f;
    public float shakeSpeed = 20f;

    [Header("References")]
    public GameObject tileVisual;

    private bool isStappedOn = false;
    private bool isShaking = false;
    private bool playerOnTile = false;
    private bool hasFallen = false;

    private Vector3 originalLocalPos;

    void Start()
    {
        originalLocalPos = tileVisual.transform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            float shakeX = Mathf.Sin(Time.time * shakeSpeed)* shakeAmount;
            float shakeZ = Mathf.Cos(Time.time * shakeSpeed)* shakeAmount;

            tileVisual.transform.localPosition = originalLocalPos + new Vector3(shakeX, 0, shakeZ);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerOnTile = true;
            if (!isStappedOn)
            {
                isStappedOn = true;
                Invoke("StartShaking", shakeDelay);
                Invoke("DestroyTile", fallDelay);
            }

            if (hasFallen)
            {
                ReloadLevel();
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTile = false;
        }
    }

    void StartShaking()
    {
        isShaking = true;
    }

    void DestroyTile()
    {
        isShaking = false;
        hasFallen = true;

        tileVisual.transform.localPosition = originalLocalPos;
        tileVisual.SetActive(false);

        if (playerOnTile)
        {
            ReloadLevel();
        }
    }

    void ReloadLevel()
    {
        Debug.Log("Player fall through the hole!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
