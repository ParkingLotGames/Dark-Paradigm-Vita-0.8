using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP
{
    /// <summary>
    /// Represents a throwable object that can be instantiated and thrown in the game.
    /// </summary>
    public class Throwable : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of the throwable object.
        /// </summary>
        public static Throwable Instance;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            Singleton();
        }

        /// <summary>
        /// Ensures that there is only one instance of the throwable object and assigns the instance to the Instance variable.
        /// If there is already an instance, it destroys the current game object.
        /// </summary>
        void Singleton()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(Throwable))]
	//[CanEditMultipleObjects]
	public class CustomThrowableInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
#endregion
}