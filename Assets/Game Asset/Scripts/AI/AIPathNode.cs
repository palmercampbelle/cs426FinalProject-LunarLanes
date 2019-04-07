using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathNode : MonoBehaviour
{
    [SerializeField] private AIPathNode[] m_AdjacentNodes;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public AIPathNode[] GetAdjacentNodes()
    {
        return m_AdjacentNodes;
    }
}
