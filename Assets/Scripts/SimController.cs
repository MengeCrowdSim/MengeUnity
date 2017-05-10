using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MengeCS;
using System;

public class SimController : MonoBehaviour {
	
	public GameObject PedestrianModel;

	private MengeCS.Simulator _sim;
	private List<GameObject> _objects = new List<GameObject>();
    private bool _sim_is_valid = false;

	private List<Color> classColors = new List<Color> () {
		Color.red,
		Color.blue,
		Color.green,
		Color.gray,
		Color.magenta
	};

	// Use this for initialization
	void Start () {
		Debug.Log ("Starting simulation...");

		string demo = "4square";
		string mengeRoot = @"E:\work\projects\menge_fork\";
		string behavior = String.Format(@"{0}examples\core\{1}\{1}B.xml", mengeRoot, demo);
		string scene = String.Format(@"{0}examples\core\{1}\{1}S.xml", mengeRoot, demo);
		Debug.Log ("\tInitialzing sim");
		Debug.Log ("\t\tBehavior: " + behavior);
		Debug.Log ("\t\tScene: " + scene);

		_sim = new MengeCS.Simulator ();
        _sim_is_valid = _sim.Initialize (behavior, scene, "orca");

        if (_sim_is_valid)
        {
            int COUNT = _sim.AgentCount;
            Debug.Log(string.Format("Simulator initialized with {0} agents", COUNT));
            for (int i = 0; i < COUNT; ++i)
            {
                MengeCS.Agent a = _sim.GetAgent(i);
                UnityEngine.Vector3 pos = new UnityEngine.Vector3(a.Position.X, a.Position.Y, a.Position.Z);
                GameObject o = Instantiate(PedestrianModel, pos, Quaternion.identity) as GameObject;
                if (o != null)
                {
                    o.transform.GetComponentInChildren<Renderer>().material.color = classColors[a.Class % classColors.Count];
                    o.transform.GetChild(0).gameObject.transform.localScale = new UnityEngine.Vector3(a.Radius * 2, 0.85f, a.Radius * 2);
                    _objects.Add(o);
                }
            }
        } else
        {
            Debug.Log("Failed to initialize the simulator...");
        }
	}
		
	// Update is called once per frame
	void Update () {
        if (_sim_is_valid)
        {
            _sim.DoStep();
            UnityEngine.Vector3 newPos = new UnityEngine.Vector3();
            for (int i = 0; i < _sim.AgentCount; ++i)
            {
                MengeCS.Vector3 pos3d = _sim.GetAgent(i).Position;
                newPos.Set(pos3d.X, pos3d.Y, pos3d.Z);
                _objects[i].transform.position = newPos;
            }
        }
	}
}
