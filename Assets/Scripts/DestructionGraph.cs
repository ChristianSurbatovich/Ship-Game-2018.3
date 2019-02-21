using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipGame.Destruction;

[CreateAssetMenu()]
public class DestructionGraph : ScriptableObject
{

    private List<List<int>[]> graphEdges = new List<List<int>[]>();
    private List<string> graphNames = new List<string>();
    public List<int>[] getGraph(DestroyableObject requester)
    {
        List<int>[] newGraph;
        int index = graphNames.IndexOf(requester.graphName);
        // if there wasn't a graph with the right name then create and add it
        if(index < 0)
        {
            graphEdges.Add(requester.generateGraph());
            graphNames.Add(requester.graphName);
            index = graphNames.IndexOf(requester.graphName);
        }

        // create a new array
        newGraph = new List<int>[graphEdges[index].Length];
        // copy the edges of each vertice into the new array
        for(int i = 0; i < graphEdges[index].Length; i++)
        {
            newGraph[i] = new List<int>(graphEdges[index][i]);
        }
        return newGraph;
    }
}
