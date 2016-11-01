using System;

public static class ConstantValues {
	public static DonutNames donutNames = new DonutNames();
	public static Tags tags = new Tags();
	public static PiggyAnimatorParameterNames piggyAnimatorParameterNames = new PiggyAnimatorParameterNames();
	public static EnemyAnimatorParameterNames enemyAnimatorParameterNames = new EnemyAnimatorParameterNames();
}

public class DonutNames {
	public string cinnamonHole = "cinnamonHole";
	public string chocolateHole = "chocolateHole";
	public string sprinklesHole = "sprinklesHole";
	public string chocolate = "chocolate";
	public string strawberry = "strawberry";
}

public class Tags {
	public string player = "Player";
	public string floor = "Floor";
	public string edge = "Edge";
	public string enemy = "Enemy";
	public string kickbox = "Kickbox";
	public string donut = "Donut";
	public string parent = "Parent";
	public string goal = "Goal";
	public string right = "Right";
	public string left = "Left";
	public string middle = "Middle";
	public string block = "Block";
	public string landable = "Landable";
	public string catcher = "Catcher";
	public string moving = "Moving";
	public string path = "Path";
	public string checkpoint = "Checkpoint";
	public string button = "Button";
	public string impassable = "Impassable";
	public string ground = "Ground";
	public string heightChange = "Heightchange";
	public string wearable = "Wearable";
	public string head = "Head";
	public string dye = "Dye";
}

public class PiggyAnimatorParameterNames {
	public string forward = "Forward";
	public string backward = "Backward";
	public string jump = "Jump";
	public string kick = "Kick";
}

public class EnemyAnimatorParameterNames {
	public string stop = "Stop";
	public string move = "Move";
	public string turn = "Turn";
}