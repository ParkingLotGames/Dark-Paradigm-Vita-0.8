using UnityEngine;
namespace DP.Interfaces
{
    /// <summary>
    /// Interface for objects that can be shattered. 
    /// </summary>
    public interface IShatterable
    {
        /// <summary>
        /// Explodes the object with the specified force.
        /// </summary>
        /// <param name="force">The amount of force to use for the explosion.</param>
        void Explode(float force);
    }
}