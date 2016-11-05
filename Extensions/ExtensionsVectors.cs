﻿using UnityEngine;

public static class ExtensionsVectors
{
	
	#region Set X/Y/Z

	public static Vector3 SetXY(this Vector3 vector, float x, float y)
	{
		return new Vector3(x, y, vector.z);
	}

	public static void SetXY(this Transform transform, float x, float y)
	{
		transform.position = transform.position.SetXY(x, y);
	}


	public static Vector3 SetX(this Vector3 vector, float x)
	{
		return new Vector3(x, vector.y, vector.z);
	}

	public static Vector2 SetX(this Vector2 vector, float x)
	{
		return new Vector2(x, vector.y);
	}

	public static void SetX(this Transform transform, float x)
	{
		transform.position = transform.position.SetX(x);
	}


	public static Vector3 SetY(this Vector3 vector, float y)
	{
		return new Vector3(vector.x, y, vector.z);
	}

	public static Vector2 SetY(this Vector2 vector, float y)
	{
		return new Vector2(vector.x, y);
	}

	public static void SetY(this Transform transform, float y)
	{
		transform.position = transform.position.SetY(y);
	}


	public static Vector3 SetZ(this Vector3 vector, float z)
	{
		return new Vector3(vector.x, vector.y, z);
	}
	
	public static void SetZ(this Transform transform, float z)
	{
		transform.position = transform.position.SetZ(z);
	}

	#endregion


	#region Offset X/Y

	public static Vector3 OffsetXY(this Vector3 vector, float x, float y)
	{
		return new Vector3(vector.x + x, vector.y + y, vector.z);
	}

	public static void OffsetXY(this Transform transform, float x, float y)
	{
		transform.position = transform.position.OffsetXY(x, y);
	}


	public static Vector3 OffsetX(this Vector3 vector, float x)
	{
		return new Vector3(vector.x + x, vector.y, vector.z);
	}

	public static Vector2 OffsetX(this Vector2 vector, float x)
	{
		return new Vector2(vector.x + x, vector.y);
	}

	public static void OffsetX(this Transform transform, float x)
	{
		transform.position = transform.position.OffsetX(x);
	}


	public static Vector2 OffsetY(this Vector2 vector, float y)
	{
		return new Vector2(vector.x, vector.y + y);
	}

	public static Vector3 OffsetY(this Vector3 vector, float y)
	{
		return new Vector3(vector.x, vector.y + y, vector.z);
	}

	public static void OffsetY(this Transform transform, float y)
	{
		transform.position = transform.position.OffsetY(y);
	}

	#endregion


	#region Clamp X/Y

	public static Vector3 ClampX(this Vector3 vector, float min, float max)
	{
		return vector.SetX(Mathf.Clamp(vector.x, min, max));
	}

	public static Vector2 ClampX(this Vector2 vector, float min, float max)
	{
		return vector.SetX(Mathf.Clamp(vector.x, min, max));
	}

	public static void ClampX(this Transform transform, float min, float max)
	{
		transform.SetX(Mathf.Clamp(transform.position.x, min, max));
	}


	public static Vector3 ClampY(this Vector3 vector, float min, float max)
	{
		return vector.SetY(Mathf.Clamp(vector.x, min, max));
	}

	public static Vector2 ClampY(this Vector2 vector, float min, float max)
	{
		return vector.SetY(Mathf.Clamp(vector.x, min, max));
	}

	public static void ClampY(this Transform transform, float min, float max)
	{
		transform.SetY(Mathf.Clamp(transform.position.x, min, max));
	}

	#endregion


	public static Vector2 ToVector2(this Vector3 vector)
	{
		return new Vector2(vector.x, vector.y);
	}

	public static Vector3 ToVector3(this Vector2 vector)
	{
		return new Vector3(vector.x, vector.y);
	}


	public static void RoundPositionToInt(this Transform transform)
	{
		transform.position = transform.position.RoundToInt();
	}

	public static Vector2 RoundToInt(this Vector2 vector)
	{
		return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
	}
	public static Vector3 RoundToInt(this Vector3 vector)
	{
		return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
	}
	
}
