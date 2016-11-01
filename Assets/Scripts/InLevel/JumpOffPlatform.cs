using UnityEngine;
using System.Collections;

// UNDONE Make different enemy behaviors
public class JumpOffPlatform : MonoBehaviour {
	public GameObject ground;
	public GameObject platform;

	private Enemy thisEnemy;

	// Use this for initialization
	void Start() {
		thisEnemy = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update() {
		if ((HeightFromGround() <= 2f) && thisEnemy.AtEdge(gameObject)) {
			JumpOff();
		} else if (thisEnemy.AtEdge(gameObject)) {
			thisEnemy.ChangeDirection();
		}
	}

	float HeightFromGround() {
		return (platform.transform.position.y - ground.transform.position.y);
	}

	void JumpOff() {

	}
}
