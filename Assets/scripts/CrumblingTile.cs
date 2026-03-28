using UnityEngine;

public class CrumblingTile : MonoBehaviour
{
    public float fallDelay = 0.8f;
    public GameObject tileVisual;
    private bool isStappedOn = false;
    private bool playerOnTile = false;
    private bool hasFallen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerOnTile = true;
            if (!isStappedOn)
            {
                isStappedOn = true;
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

    void DestroyTile()
    {
        hasFallen = true;
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
