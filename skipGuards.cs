using System;


/*Class Invariants
 * The offset value should be calculated correctly based on the shield inventory length.
 *The offset value should be used correctly in the Block method to shift the index before calling the base Block method.
 *The skipGuards class should inherit correctly from the Guards class and override the Block method.
 */
namespace P5
{
	public class skipGuards : Guards
	{
        private int offset;


        //Constructor
        //Precond: Durability is a valid shield inventory 
        //Postcond: creates a skipGuard Object
        public skipGuards(IShieldProvider durability): base(durability)
		{
            offset = offsetCalc();
           
		}


        // Pre-cond: None
        // Post-cond:returns a integer that is calculated for the offset.
        private int offsetCalc()
        {
            int offsetVal = shieldDurability.Length % 3;
            return offsetVal;
        }


        // Precond: N/A, the guard class block method
        // Postcond:makes shifts the used shield index by a certain offset and calls guards class block
        public override bool Block(int x)
        {
            
            int newX = x + offset;
            if(newX < shieldDurability.Length)
            {
                return base.Block(newX);
            }
            return false;
            
        }

    }
}


/*Implementation Invariants
 *The offset value (offset) should be initialized correctly during the construction of a skipGuards object.
 *The offset value (offset) should be calculated as the remainder of dividing the shield inventory length (shieldDurability.Length) by 3.
 *The offset value (offset) should be stored and maintained correctly throughout the lifecycle of the skipGuards object.
 *The Block method in the skipGuards class should correctly calculate the new index (newX) by adding the offset value (offset) to the original index (x).
 *The new index (newX) should be within the range of the shield inventory array (shieldDurability.Length).
 *The skipGuards class should properly inherit from the Guards class and override the Block method.
 *The overridden Block method in the skipGuards class should correctly handle the index shifting using the offset value (offset) and call the base Block method with the new index (newX).
 *The Block method in the skipGuards class should return the correct result (true or false) based on the success or failure of the block operation.
 * 
 * 
 */