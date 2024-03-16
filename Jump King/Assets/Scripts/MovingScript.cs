using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class MovingScript : MonoBehaviour
{
    private bool move = false;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float objectSpeed;

    private int targetIndex = 0;
    private GameObject nextPos;
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        move = false;  // Set move to false to prevent unnecessary updates
    }

    // Detect trigger with player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If we triggered the player, enable movement and show indicator
        if (other.CompareTag("Player"))
        {
            anim.SetInteger("state",1);
            move = true;
            Debug.Log("Collided with player");
        }
    }

    void Update()
    {
        if(move == true)
        {
            MoveGameObject();
        }
    }

    void MoveGameObject()
    {
        if (Vector2.Distance(targets[targetIndex].transform.position, transform.position) < 0.1f)
        {
            targetIndex++;
            if (targetIndex >= targets.Length)
            {
                targetIndex = 0;
            }
            nextPos = targets[targetIndex];
            move = false;
            anim.SetInteger("state",0);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targets[targetIndex].transform.position, objectSpeed * Time.deltaTime);
        }
    }
}
