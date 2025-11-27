using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;

    public GameObject player;

    enum MovementState {
        WANDERING,
        CHASING
    }

    private MovementState state = MovementState.WANDERING;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        SetNewDestination();
    }

    void OnCollisionEnter(Collision collision){
        foreach (ContactPoint contact in collision.contacts){
            if (contact.otherCollider.name == "Player"){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            Debug.Log("Collide");
            Debug.Log(contact.otherCollider.name);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.name == "Player"){
            state = MovementState.CHASING;
        }
        Debug.Log(other.name);
    }

    void OnTriggerExit(Collider other){
        if (other.name == "Player"){
            state = MovementState.WANDERING;
        }
        Debug.Log(other.name);
    }

    void SetNewDestination(){
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        if (state == MovementState.WANDERING && agent.remainingDistance <= 1.0f)
        {
            float x = Random.Range(-75.0f, 75.0f);
            float z = Random.Range(-75.0f, 75.0f);
            agent.destination = new Vector3(x, 0.0f, z);
        }
        else if (state == MovementState.CHASING){
            agent.destination = player.transform.position;
        }
    }
}
