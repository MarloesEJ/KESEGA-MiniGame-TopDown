using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Vector3 target;

    void Start()
    {
        target = pointA.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);

        Vector3 direction = (target - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by patrol!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
