using System;


/*Class invariants
 * The prevShield variable should store the index of the previously chosen shield.
 *The quirkyGuard class should inherit correctly from the Guards class and override the Block method.
 *The quirkyGuard constructor should initialize the prevShield variable to -1.
 *The Block method in the quirkyGuard class should choose a random valid index for the shield.
 *The chosenShield index should be calculated as (i + x) % shieldDurability.Length, where i represents the loop iteration.
 *The chosenShield index should be different from the prevShield index and within the range of the shieldDurability array.
 *The prevShield variable should be updated with the value of i (the current loop iteration) if a valid chosenShield index is found.
 *The quirkyGuard class should call the base Block method with the chosenShield index to perform the block operation.
 *The Block method in the quirkyGuard class should return the correct result (true or false) based on the success or failure of the block operation.
 * 
 */
namespace P5
{
	public class quirkyGuard:Guards
	{
        private int prevShield;

        //Constructor
        //Precond: Durability is a valid shield inventory 
        //Postcond: creates a quirkyGuard Object
		public quirkyGuard(IShieldProvider Durability):base(Durability)
		{
            prevShield = -1;

		}

        // Precond: N/A, the guard class block method
        // Postcond:makes sure that it chooses a random valid index for the shield anc calls block
        //                from guards.
        public override bool Block(int x)
        {
            int chosenShield = -1;
            for(int i = 0; i < shieldDurability.Length; i++)
            {
                chosenShield = (i + x) % shieldDurability.Length;
                if(prevShield != i && chosenShield < shieldDurability.Length)
                {
                    prevShield = i;
                    break;
                }

            }
            return base.Block(chosenShield);
        }

    }
}

/*Implementation invariants
 * he prevShield variable should be initialized correctly during the construction of a quirkyGuard object.
 *The prevShield variable should be initialized to -1 initially.
 *The quirkyGuard class should properly inherit from the Guards class and override the Block method.
 *The overridden Block method in the quirkyGuard class should correctly choose a random valid index for the shield.
 *The chosenShield index should be calculated as (i + x) % shieldDurability.Length, where i represents the loop iteration.
 *The chosenShield index should be different from the prevShield index to ensure randomness in the shield selection.
 *The chosenShield index should be within the range of the shieldDurability array to avoid accessing out-of-bounds indices.
 *The prevShield variable should be updated with the value of i (the current loop iteration) when a valid chosenShield index is found.
 *The quirkyGuard class should call the base Block method with the chosenShield index to perform the block operation.
 *The Block method in the quirkyGuard class should return the correct result (true or false) based on the success or failure of the block operation.
 * 
 */