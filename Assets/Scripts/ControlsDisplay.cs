using System;
using UnityEngine;

public class ControlsDisplay : MonoBehaviour
{
    public GameObject controlsDisplayUI;

    // Start is called before the first frame update
    void start()
	{
		//TODO
	}

    // Update is called once per frame
    void update()
	{
		//TODO
	}

	public void showUI()
	{
		controlsDisplayUI.SetActive(true);
	}

	public void hideUI()
	{
		controlsDisplayUI.SetActive(false);
	}
}
