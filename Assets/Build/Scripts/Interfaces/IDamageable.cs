namespace DP.Interfaces
{
    /// <summary>
    /// Interface for objects that can take damage
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Called to damage the object.
        /// </summary>
        /// <param name="damage">The amount of damage to inflict.</param>
        void TakeDamage(int damage);
    }
}