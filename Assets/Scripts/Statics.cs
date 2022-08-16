using UnityEngine;


public static class DataManager
{
	static public void SaveAll()
	{
		PID_Properties.Save();
		Baterry.Save();
		Parameters.Save();
		QuadCharacteristics.Save();
	}

	static public void LoadAll()
	{
		PID_Properties.Load();
		Baterry.Load();
		Parameters.Load();
		QuadCharacteristics.Load();
	}

	static public void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	static public void TestLoad()
    {
		PID_Properties.pitch_P = 60;
		PID_Properties.pitch_I = 40;
		PID_Properties.pitch_D = 60;
		PID_Properties.yaw_P = 60;
		PID_Properties.yaw_I = 40;
		PID_Properties.yaw_D = 60;
		PID_Properties.roll_P = 60;
		PID_Properties.roll_I = 40;
		PID_Properties.roll_D = 60;
		
		CameraProperties.firstPersonCameraAngle = 0;
		CameraProperties.FOV = 90;
		CameraProperties.mass = 1;
		CameraProperties.isFirstPersonCamOn = true;

		Baterry.mass = 1;
		Baterry.maxCharge = 100;
		Baterry.charge = 65;

		QuadCharacteristics.mass = 6;

		Parameters.musicVolume = 1;
		Parameters.effectVolume = 1;
		//Parameters.controlMap = ControlMap.FrSkyTaranisX7;
	}
}

public static class PID_Properties
{
	static public float pitch_P;
	static public float pitch_I;
	static public float pitch_D;

	static public float yaw_P;
	static public float yaw_I;
	static public float yaw_D;

	static public float roll_P;
	static public float roll_I;
	static public float roll_D;

	public static void Save()
	{
		PlayerPrefs.SetFloat("pitch_P", pitch_P);
		PlayerPrefs.SetFloat("pitch_I", pitch_I);
		PlayerPrefs.SetFloat("pitch_D", pitch_D);

		PlayerPrefs.SetFloat("yaw_P", yaw_P);
		PlayerPrefs.SetFloat("yaw_I", yaw_I);
		PlayerPrefs.SetFloat("yaw_D", yaw_D);

		PlayerPrefs.SetFloat("roll_P", roll_P);
		PlayerPrefs.SetFloat("roll_I", roll_I);
		PlayerPrefs.SetFloat("roll_D", roll_D);

		PlayerPrefs.Save();
	}
	public static void Load()
	{
		pitch_P = PlayerPrefs.GetFloat("pitch_P", 60);
		pitch_I = PlayerPrefs.GetFloat("pitch_I", 40);
		pitch_D = PlayerPrefs.GetFloat("pitch_D", 60);

		yaw_P = PlayerPrefs.GetFloat("yaw_P", 60);
		yaw_I = PlayerPrefs.GetFloat("yaw_I", 40);
		yaw_D = PlayerPrefs.GetFloat("yaw_D", 60);

		roll_P = PlayerPrefs.GetFloat("roll_P", 60);
		roll_I = PlayerPrefs.GetFloat("roll_I", 40);
		roll_D = PlayerPrefs.GetFloat("roll_D", 60);
	}
	public static void Drop()
	{
		PlayerPrefs.DeleteKey("pitch_P");
		PlayerPrefs.DeleteKey("pitch_I");
		PlayerPrefs.DeleteKey("pitch_D");

		PlayerPrefs.DeleteKey("yaw_P");
		PlayerPrefs.DeleteKey("yaw_I");
		PlayerPrefs.DeleteKey("yaw_D");

		PlayerPrefs.DeleteKey("roll_P");
		PlayerPrefs.DeleteKey("roll_I");
		PlayerPrefs.DeleteKey("roll_D");
	}
}

public static class CameraProperties
{
	static public float firstPersonCameraAngle;
	static public float FOV;
	static public float mass;
	static public bool isFirstPersonCamOn = true;

	public static void Save()
	{
		PlayerPrefs.SetFloat("firstPersonCameraAngle", firstPersonCameraAngle);
		PlayerPrefs.SetFloat("FOV", FOV);
		PlayerPrefs.SetFloat("cameraMass", mass);
		PlayerPrefs.SetString("isFirstPersonCamOn", isFirstPersonCamOn.ToString());

		PlayerPrefs.Save();
	}
	public static void Load()
	{
		firstPersonCameraAngle = PlayerPrefs.GetFloat("firstPersonCameraAngle", 45);
		FOV = PlayerPrefs.GetFloat("FOV", 100);
		mass = PlayerPrefs.GetFloat("cameraMass", 0.25f);
		isFirstPersonCamOn = bool.Parse(PlayerPrefs.GetString("isFirstPersonCamOn", "true"));
	}
}

public static class Baterry
{
	static public float mass;
	static public float maxCharge;
	static public float charge;

	public static void Save()
	{
		PlayerPrefs.SetFloat("maxCharge", maxCharge);
		PlayerPrefs.SetFloat("batteryMass", mass);

		PlayerPrefs.Save();
	}

	public static void Load()
	{
		maxCharge = PlayerPrefs.GetFloat("maxCharge", 100);
		mass = PlayerPrefs.GetFloat("batteryMass", 0.25f);
	}
}

public static class QuadCharacteristics
{
	static public float mass;

	public static void Save()
	{
		PlayerPrefs.SetFloat("droneMass", mass);
		PlayerPrefs.Save();
	}
	public static void Load()
	{
		mass = PlayerPrefs.GetFloat("droneMass", 2f);
	}
}

public static class Parameters
{
	static public float musicVolume;
	static public float effectVolume;
	static public ControlMap controlMap;

	public static void Save()
	{
		PlayerPrefs.SetFloat("musicVolume", musicVolume);
		PlayerPrefs.SetFloat("effectVolume", effectVolume);
		PlayerPrefs.Save();
	}

	public static void Load()
	{
		musicVolume = PlayerPrefs.GetFloat("musicVolume", 0);
		effectVolume = PlayerPrefs.GetFloat("effectVolume", 0);
	}
}
