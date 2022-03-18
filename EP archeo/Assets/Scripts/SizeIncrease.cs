using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeIncrease : MonoBehaviour
{

	private EventCentre EC;
    // Start is called before the first frame update
    void Start()
    {
		EC = GameObject.Find("EventCentre").GetComponent<EventCentre>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increment(float value)
    {
        EC.scale += 0.1f;
    }
}
