using UnityEngine;

public class GlobalClippingManager : MonoBehaviour {
	
	/*
	 * The clipping plane system uses the following logic:
	 * - There are up to 3 planes
	 * - Each plane has:
	 *	an object (planeX) which is a grabbable box that the user can pove arround
	 *	a camrea to see what's in the clipping plane
	 *	a sceen to display it
	 *
	 * Since there is only 3 of each and Unity doesn't allow arrays in the editor inspector (at least not as I know), we need to
	 * provide each object indepedantly, which is gross, but I don't know a better way
	 *
	 * The public function getObject (id, type) can easily provide the right object thanks to its ugly huge switch statement
	 *
	 * We can set, in the editor or while running thanks to aanother script the number of planes and cameras (cameras are laggy)
	 */
	
	public GameObject plane1;
	public GameObject plane2;
	public GameObject plane3;
	
	public GameObject camera1;
	public GameObject camera2;
	public GameObject camera3;
	
	public GameObject screen1;
	public GameObject screen2;
	public GameObject screen3;
	
	[Range (0,3)]
	public int planeNb = 0;
	[Range (0,3)]
	public int cameraNb = 0;
	
	private int planeNbOld=-1;
	private int cameraNbOld=-1;
	
	private ClippableObject[] objList;

    public void Start() { 
		objList = FindObjectsOfType<ClippableObject>();
	}


	
	public void Update() {
		
		if (cameraNb>planeNb)
			cameraNb=planeNb; // Wa can't have more cameras than planes
		
		if (planeNb!=planeNbOld) {
			planeNbOld=planeNb;
			foreach (ClippableObject obj in objList)
				obj.clipPlanes=planeNb;
			for (int i = 1; i <= 3; i++)
				if (i<=planeNb)
					setPlaneEnabled(i,true);
				else
					setPlaneEnabled(i,false);
		}
		
		if (cameraNb!=cameraNbOld) {
			cameraNbOld=cameraNb;
			for (int i = 1; i <= 3; i++)
				if (i<=cameraNb)
					setCameraEnabled(i,true);
				else
					setCameraEnabled(i,false);
		}
		
		Vector3 plane1Position=plane1.GetComponent<Transform>().position;
		Vector3 plane1Rotation=plane1.GetComponent<Transform>().rotation.eulerAngles;
		
		Vector3 plane2Position=plane2.GetComponent<Transform>().position;
		Vector3 plane2Rotation=plane2.GetComponent<Transform>().rotation.eulerAngles;
		
		Vector3 plane3Position=plane3.GetComponent<Transform>().position;
		Vector3 plane3Rotation=plane3.GetComponent<Transform>().rotation.eulerAngles;
		
		foreach (ClippableObject obj in objList) {
			obj.plane1Position=plane1Position;
			obj.plane1Rotation=plane1Rotation;
			
			obj.plane2Position=plane2Position;
			obj.plane2Rotation=plane2Rotation;
			
			obj.plane3Position=plane3Position;
			obj.plane3Rotation=plane3Rotation;
			
		}
		
	}
	
	/*
	 * Return the correct the plane, camera or screen of the corresponding ID, or null if an incorrect ID is provided
	 */
	public GameObject getObject(int id, planeObjectType type) {
		switch ((id, type)) {
			case (1,planeObjectType.Plane):
				return plane1;
			case (1,planeObjectType.Camera):
				return camera1;
			case (1,planeObjectType.Screen):
				return screen1;
			
			case (2,planeObjectType.Plane):
				return plane2;
			case (2,planeObjectType.Camera):
				return camera2;
			case (2,planeObjectType.Screen):
				return screen2;
			
			case (3,planeObjectType.Plane):
				return plane3;
			case (3,planeObjectType.Camera):
				return camera3;
			case (3,planeObjectType.Screen):
				return screen3;
		}
		return null;
	}
	
	private void setPlaneEnabled(int id, bool b) {
		getObject(id,planeObjectType.Plane).SetActive(b);
	}
	
	private void setCameraEnabled(int id, bool b) {
		getObject(id,planeObjectType.Camera).SetActive(b);
		getObject(id,planeObjectType.Screen).SetActive(b);
	}

    public void Toggle()
    {
        if (planeNb == 0)
        {
            planeNb = 3;
            cameraNb = 3;
        }
        else
        {
            planeNb = 0;
            cameraNb = 0;
        }
    }
}

public enum planeObjectType {Plane, Camera, Screen};