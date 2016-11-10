using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// On each UI wearable item element. Controls each one individually
public class DragWearableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
	public GameObject itemPrefab;
	public Canvas canvas;

	protected GameObject draggedObject;
	private bool selectable = true;
	protected bool dragging = false;

	/// <summary>
	/// Creates the object but on the screen rather than the canvas.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnBeginDrag(PointerEventData eventData) {
		if (selectable) {
			CreateWearableItem(eventData.position);
			GrayOutListOption (true);
			dragging = true;
		}
	}

	public void OnDrag(PointerEventData eventData) {
		if (dragging) {
			draggedObject.transform.position = ScreenPosition (eventData.position);
		}
	}

	public virtual void OnEndDrag(PointerEventData eventData) {
		if (dragging) {
			if (ItemsOnPig.inPigArea) {
				ItemsOnPig.itemsOnPig.PlaceOnPig (draggedObject, gameObject);
			} else {
				Destroy (draggedObject);
				GrayOutListOption (false);
			}
			dragging = false;
		}
	}

	public void GrayOutListOption(bool inUse) {
		Image image = GetComponent<Image> ();
		if (inUse) {
			image.color = new Color (image.color.r, image.color.g, image.color.b, 0.5f);
			selectable = false;
		} else {
			image.color = new Color (image.color.r, image.color.g, image.color.b, 1f);
			selectable = true;
		}
	}

	/// <summary>
	/// Creates a gameobject with the same properties (style, color, pattern) as the UI element selected.
	/// </summary>
	/// <param name="position">Position.</param>
	void CreateWearableItem(Vector2 position) {
		draggedObject = Instantiate (itemPrefab, ScreenPosition (position), Quaternion.identity) as GameObject;
		draggedObject.GetComponent<SpriteRenderer> ().color = gameObject.GetComponent<Image> ().color;
	}

	/// <summary>
	/// Converts a position on the canvas to one on the screen that directly relates to where a finger is.
	/// </summary>
	/// <returns>The position.</returns>
	/// <param name="canvasPoint">Canvas point.</param>
	Vector2 ScreenPosition(Vector2 canvasPoint) {
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (canvas.transform as RectTransform, canvasPoint,
			Camera.main, out localPoint);
		return canvas.transform.TransformPoint(localPoint);
	}
}
