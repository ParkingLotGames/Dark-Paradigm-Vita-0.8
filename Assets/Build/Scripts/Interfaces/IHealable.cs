namespace DP.Interfaces
{
    /// <summary>
    /// Interface for objects that can be healed.
    /// </summary>
    public interface IHealable
    {
        /// <summary>
        /// Heals the object by the specified amount.
        /// </summary>
        /// <param name="healAmt">The amount to heal the object by.</param>
        void Heal(int healAmt);
    }
}