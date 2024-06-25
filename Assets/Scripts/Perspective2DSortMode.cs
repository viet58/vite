// THis script prevents Z-fightitng on the camera view
// Perspective2DSortMode.cs
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode()]
public class Perspective2DSortMode : MonoBehaviour {
	public Transform character;
	public Vector3 offset;
	void Awake () {
		GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;
	}

	void LastUpdate()
    {
		if(character != null)
        {
			transform.position = character.position + offset;
        }
    }
}