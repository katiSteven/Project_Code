using UnityEngine;

public class GridMaker : MonoBehaviour
{
	[SerializeField]
	private float size = 1f;

	public Vector3 GetNearestPointOnGrid(Vector3 position)
	{
		position -= transform.position;

		int xCount = Mathf.RoundToInt(position.x / size);
		int yCount = Mathf.RoundToInt(position.y / size);
		int zCount = Mathf.RoundToInt(position.z / size);

		Vector3 result = new Vector3(
			(float)xCount * size,
			(float)yCount * size,
			(float)zCount * size);

		result += transform.position;

		return result;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		for (float x = transform.position.x; x < 40; x += size)
		{
			for (float z = transform.position.z; z < 40; z += size)
			{
				var point = GetNearestPointOnGrid(new Vector3(x, transform.position.y, z));
				Gizmos.DrawSphere(point, 0.1f);
			}

		}
	}
}