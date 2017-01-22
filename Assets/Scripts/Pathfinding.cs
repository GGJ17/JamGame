using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	public Transform seeker, target, returnTarget;
	Grid grid;
	public List<Node> path = new List<Node> ();
	public List<Node> pathBack = new List<Node> ();

	void Awake() {
		grid = GetComponent<Grid> ();
	}

	void Update() {
		FindPath (seeker.position, target.position,false);
		FindPath (seeker.position, returnTarget.position,true);
	}

	public List<Node> GetPath(){
		return path;
	}

	public List<Node> GetPathBack(){
		return pathBack;
	}

	void FindPath(Vector3 startPos, Vector3 targetPos,bool backward) {
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Node node = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == targetNode) {
				if (!backward) {
					RetracePath (startNode, targetNode, false);
				} else {
					RetracePath (startNode, targetNode, true);
				}
				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(node)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode,bool backward) {
		List<Node> nuPath = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			nuPath.Add(currentNode);
			currentNode = currentNode.parent;
		}
		nuPath.Reverse();

		if (!backward) {
			path = nuPath;
			//grid.path = path;
		} else {
			pathBack = nuPath;
			grid.path = nuPath;
		}

	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}
