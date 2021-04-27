/*
	SetRenderQueue.cs
 
	Sets the RenderQueue of an object's materials on Awake. This will instance
	the materials, so the script won't interfere with other renderers that
	reference the same materials.
*/

using UnityEngine;

[AddComponentMenu("Rendering/SetRenderQueue")]

public class SetRenderQueue : MonoBehaviour
{

	[SerializeField]
	protected int[] m_queues = new int[] { 2000 };

	protected void Awake()
	{
		Set();
	}

	private void Set ()
	{
		Renderer rend = GetComponent<Renderer>();
		if (rend != null)
			SetQueue(rend);

		Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
		if (rends != null)
			foreach (Renderer renderer in rends)
				SetQueue(renderer);
	}

	private void SetQueue(Renderer renderer)
	{
		Material[] materials = renderer.materials;
		for (int i = 0; i < materials.Length && i < m_queues.Length; ++i)
			materials[i].renderQueue = m_queues[i];
	}
}