using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Attached to the item in ItemCustomization. Only one around
public class ModifierOnItem : MonoBehaviour {

	public static ModifierOnItem modifierOnItem;
	public static bool inItemArea;

	private HeadItem currentlyModifying;

	void Start() {
		modifierOnItem = this;
		inItemArea = false;
		currentlyModifying = GlobalControl.Instance.itemToModify;
		name = currentlyModifying.itemName;
		GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/WearableItems/" + name);
	}

	// When the button is pressed indicating that the player is done creating their item
	public void CreateItem() {
		HeadItem newItem = new HeadItem (name, new SerializableColor (GetComponent<SpriteRenderer> ().color), new SpriteRenderer (), 
			currentlyModifying.itemStatIncrease, 0, false);
		// Create the new item if the player doesn't already own the same one
		if (!GlobalControl.Instance.savedData.headItems.Exists (h => h.Equals (newItem))) {
			GlobalControl.Instance.savedData.headItems.Add (newItem);
			GlobalControl.Instance.Save ();
			SceneManager.LoadScene ("Customization");
		} else {
			// TODO Add message about already owning the item
			Debug.Log ("Already have that item.");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Contains(ConstantValues.tags.dye)) {
			inItemArea = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag.Contains(ConstantValues.tags.dye)) {
			inItemArea = false;
		}
	}

	public void ApplyToItem(GameObject modifier) {
		if (modifier.tag.Contains(ConstantValues.tags.dye)) {
			ChangeColor(modifier);
		}
		inItemArea = false;
	}

	void ChangeColor(GameObject modifier) {
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		sprite.color = modifier.GetComponent<SpriteRenderer> ().color;
		Debug.Log ("Changed color to " + sprite.color);
		Destroy (modifier);
	}
}
