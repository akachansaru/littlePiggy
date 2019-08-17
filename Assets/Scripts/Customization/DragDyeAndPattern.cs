using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragDyeAndPattern : DragWearableItem {

	public override void OnEndDrag(PointerEventData eventData) {
		if (dragging) {
			if (ModifierOnItem.inItemArea) {
				ModifierOnItem.modifierOnItem.ApplyToItem(draggedObject);
			} else {
				Destroy (draggedObject);
				GrayOutListOption (false);
			}
			dragging = false;
		}
		
	}
}
