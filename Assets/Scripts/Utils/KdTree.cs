using System.Collections.Generic;
using System.Numerics;

public class Node
{
	public float[] point;
	public Node left;
	public Node right;

	// Defining a constructor to initialize the Node object
	public Node(float[] point)
	{
		this.point = point;
		left = null;
		right = null;
	}
}

// Defining the KdTree class to represent a K-d tree with a specified value of k
public class KdTree
{
	private int k;
	Node root = null;
	Node lastNode = null;

	float? currentDistance = null;
	float? prevDistance = null;

	// Defining a constructor to initialize the KdTree object
	public KdTree(int k)
	{
		this.k = k;
	}

	public KdTree(int k, Vector3[] vectorArray)
	{
		this.k = k;
		InsertPoints(ParseVectorArrayToPoint(vectorArray));
	}

	// Defining a method to create a new Node object
	public Node NewNode(float[] point)
	{
		return new Node(point);
	}

	// Defining a recursive method to insert a point into the K-d tree
	private Node InsertRec(Node inRoot, float[] point, int depth)
	{
		if (inRoot == null)
		{
			return NewNode(point);
		}

		int cd = depth % k;

		if (point[cd] < inRoot.point[cd])
		{
			inRoot.left = InsertRec(inRoot.left, point, depth + 1);
		}
		else
		{
			inRoot.right = InsertRec(inRoot.right, point, depth + 1);
		}

		return inRoot;
	}

	// Defining a method to insert a point into the K-d tree
	public Node Insert(Node inRoot, float[] point)
	{
		return InsertRec(inRoot, point, 0);
	}

	// Defining a method to search for a point in the K-d tree
	public Node Search(Node root, float[] point)
	{
		return SearchRec(this.root, point, 0);
	}

	#region Private

	// Defining a method to check if two points are the same
	private bool ArePointsSame(float[] point1, float[] point2)
	{
		for (int i = 0; i < k; i++)
		{
			if (point1[i] != point2[i])
			{
				return false;
			}
		}

		return true;
	}

	private float GetDistance(float[] point1, float[] point2)
    {
		Vector3 vectorPoint1 = ParseFloatArrToVector(point1);
		Vector3 vectorPoint2 = ParseFloatArrToVector(point2);

		return Vector3.Distance(vectorPoint1, vectorPoint2);
    }

	// Defining a recursive method to search for a point in the K-d tree
	private Node SearchRec(Node root, float[] point, int depth)
	{
		if (root == null)
		{
			return lastNode;
		}

		currentDistance = GetDistance(root.point, point);
		if(prevDistance == null || prevDistance > currentDistance)
        {
			prevDistance = currentDistance;
			lastNode = root;
        }
		UnityEngine.Debug.Log("Prev dis: " + prevDistance + " currDist: " + currentDistance);
		//if (ArePointsSame(root.point, point))
		//{
		//	return true;
		//}

		int cd = depth % k;

		if (point[cd] < root.point[cd])
		{
			return SearchRec(root.left, point, depth + 1);
		}

		return SearchRec(root.right, point, depth + 1);
	}

	private List<float[]> ParseVectorArrayToPoint(Vector3[] vectorArray)
    {
		List<float[]> points = new List<float[]>();
		float[] point = new float[k];

		for (int i = 0; i < vectorArray.Length; i++)
        {
			point[0] = vectorArray[i].X;
			point[1] = vectorArray[i].Y;
			point[2] = vectorArray[i].Z;

			points.Add(new float[] { point[0], point[1], point[2] });
		}

		return points;
    }

	private Vector3 ParseFloatArrToVector(float[] point)
    {
		Vector3 newVector = new Vector3();
		if(point.Length == 3)
        {
			newVector.X = point[0];
			newVector.Y = point[1];
			newVector.Z = point[2];
        }

		return newVector;
    }

	private void InsertPoints(List<float[]> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
			root = Insert(root, points[i]);
        }
    }
    #endregion
}
