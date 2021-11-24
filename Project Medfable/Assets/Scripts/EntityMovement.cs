using UnityEngine;
using UnityEngine.AI;

public class EntityMovement : MonoBehaviour
{

    //Allows an entity to move towards a destination by changing their position to the target's
    public void MoveTowards(Vector3 target)
    {
        GetComponent<NavMeshAgent>().destination = target;
    }
}

