using UnityEngine;
using System.Collections;

public class LevelEntries : MonoBehaviour {
    public LevelSetup levelSetup;
    public string levelName;
    public int minLevelpayment;
    public int maxLevelpayment;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(ConstantValues.tags.player)) {
            levelSetup.SelectLevelPayment(levelName + LevelSetup.delimiter + minLevelpayment + LevelSetup.delimiter + maxLevelpayment);
            other.gameObject.GetComponent<PigControlInput>().StopForward();
            other.gameObject.GetComponent<PigControlInput>().StopBackward();
        }
    }
}
