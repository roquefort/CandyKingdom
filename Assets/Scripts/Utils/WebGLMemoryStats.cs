using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLMemoryStats : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern uint GetTotalMemorySize();

	[DllImport("__Internal")]
	private static extern uint GetTotalStackSize();

	[DllImport("__Internal")]
	private static extern uint GetStaticMemorySize();

	[DllImport("__Internal")]
	private static extern uint GetDynamicMemorySize();
#else
	private static uint GetTotalMemorySize() => 0;
	private static uint GetTotalStackSize() => 0;
	private static uint GetStaticMemorySize() => 0;
	private static uint GetDynamicMemorySize() => 0;
#endif

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F2))
		{
			int starCount = 20;

			Debug.Log(new string('*', starCount));
			Debug.Log($"{nameof(GetTotalMemorySize)}: {GetTotalMemorySize()} bytes");
			Debug.Log($"{nameof(GetTotalMemorySize)}: {GetTotalMemorySize() / 1024 ^ 3} GB");
			Debug.Log(new string('-', starCount));
			Debug.Log($"{nameof(GetTotalStackSize)}: {GetTotalStackSize()} bytes");
			Debug.Log($"{nameof(GetTotalStackSize)}: {GetTotalStackSize() / 1024 ^ 3} GB");
			Debug.Log(new string('-', starCount));
			Debug.Log($"{nameof(GetStaticMemorySize)}: {GetStaticMemorySize()} bytes");
			Debug.Log($"{nameof(GetStaticMemorySize)}: {GetStaticMemorySize() / 1024 ^ 3} GB");
			Debug.Log(new string('-', starCount));
			Debug.Log($"{nameof(GetDynamicMemorySize)}: {GetDynamicMemorySize()} bytes");
			Debug.Log($"{nameof(GetDynamicMemorySize)}: {GetDynamicMemorySize() / 1024 ^ 3} GB");
			Debug.Log(new string('*', starCount));
		}
	}
}
