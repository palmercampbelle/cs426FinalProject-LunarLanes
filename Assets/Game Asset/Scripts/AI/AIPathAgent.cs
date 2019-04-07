using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AIPathAgent : MonoBehaviour
{
    [SerializeField] protected AIPath m_path;
    [SerializeField] protected float m_MoveSpeed = 5.0f;

    public readonly float NEAR_TARGET_THRESHOLD = 0.5f;

    protected AIPathNode targetPathNode;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull( m_path );
        targetPathNode = m_path.GetHeadNode();
        Assert.IsNotNull( targetPathNode );
    }

    // Update is called once per frame
    void Update()
    {
        if ( AmINearTarget() )
        {
            SeekNextTargetAtRandom();
        }

        MoveTowardsTarget();
    }

    bool AmINearTarget()
    {
        float distance = (targetPathNode.transform.position - transform.position).magnitude;
        return distance < NEAR_TARGET_THRESHOLD;
    }

    void SeekNextTargetAtRandom()
    {
        AIPathNode[] adjNodes = targetPathNode.GetAdjacentNodes();
        Assert.IsNotNull( adjNodes );

        targetPathNode = adjNodes[Random.Range( 0, adjNodes.Length )];
        Assert.IsNotNull( targetPathNode );
    }

    void MoveTowardsTarget()
    {
        Vector3 moveDirection = (targetPathNode.transform.position - transform.position).normalized;

        moveDirection *= (m_MoveSpeed * Time.deltaTime);

        transform.position += moveDirection;
    }
}
