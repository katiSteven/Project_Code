using UnityEngine;

public class CubePlacer : MonoBehaviour
{
	public GameObject ObjectType;
	public GameObject containerObject;
	public float hightOffset = 0.5f;

	private Camera camera1;
	private GridMaker grid;

	private void Awake()
	{
		grid = FindObjectOfType<GridMaker>();
		camera1 = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo;
			Ray ray = camera1.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast(ray, out hitInfo))
			{
				PlaceCubeNear(hitInfo.point);
			}
		}
	}

	private void PlaceCubeNear(Vector3 clickPoint)
	{
		var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
		GameObject newObject = Instantiate(ObjectType);
		newObject.transform.position = new Vector3 (finalPosition.x, finalPosition.y + hightOffset, finalPosition.z);
		newObject.transform.parent = containerObject.transform;
	}
}