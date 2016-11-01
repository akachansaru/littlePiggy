using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SerializableColor {
	private float r, g, b, a;

	public SerializableColor(Color color) {
		r = color.r;
		g = color.g;
		b = color.b;
		a = color.a;
	}

	public static Color ToColor(SerializableColor serializableColor) {
		return new Color (serializableColor.r, serializableColor.g, serializableColor.b, serializableColor.a);
	}

	public static SerializableColor FromColor(Color color) {
		return new SerializableColor (color);
	}

	public static SerializableColor white {
		get { return FromColor(Color.white); }
	}

	public override string ToString () {
		return ("SerializableColor (" + r + ", " + g + ", " + b + ", " + a + ")");
	}

	public override bool Equals (object obj) {
		if (obj == null) return false;
		SerializableColor objAsSerializableColor = obj as SerializableColor;
		if (objAsSerializableColor == null)	return false; 
		else return this.Equals (objAsSerializableColor);
	}

	public bool Equals(SerializableColor other) {
		if (other == null) return false;
		return (this.r.Equals (other.r) && this.g.Equals (other.g) && this.b.Equals (other.b));
	}

	public override int GetHashCode() {
		int hash = 13;
		hash = (hash * 7) + r.GetHashCode ();
		hash = (hash * 7) + g.GetHashCode ();
		hash = (hash * 7) + b.GetHashCode ();
		return hash;
	}
}

