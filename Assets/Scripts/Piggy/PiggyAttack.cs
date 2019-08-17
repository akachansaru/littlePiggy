using UnityEngine;
using System.Collections;

public class PiggyAttack : MonoBehaviour {

	private Pig pig;

	void Start() {
		pig = transform.parent.GetComponent<Pig> ();
	}

	void OnTriggerStay2D(Collider2D other) {
		// Layer 8 is Enemy, 11 is Kickbox. Have to check manually because the collision matrix isn't working for triggers 
		if (pig.Kicking && !pig.Kicked && ((other.gameObject.layer.Equals(8)) || (other.gameObject.layer.Equals(11)))) {
			pig.Kicked = true; // Make piggy only kick once per kick animation
			Debug.Log("Kicked " + other.name + " " + other.tag + " " + other.gameObject.layer);
			Debug.Log ("Kickbox layer: " + gameObject.layer);
			if (other.gameObject.tag.Contains(ConstantValues.tags.enemy)) {
				Debug.Log ("Kicked enemy " + other.gameObject.name);
				other.gameObject.GetComponent<Enemy.EnemyMovement> ().TakeDamage (LevelManager.piggyDamage);
			}

			if (other.gameObject.tag.Contains (ConstantValues.tags.button)) {
				Debug.Log ("Kicking button");
				other.gameObject.GetComponent<InteractableButton> ().KickButton ();
			}
		}
	}
}
