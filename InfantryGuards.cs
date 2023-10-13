using System;


/*Class invariants
 * The infantryGuards class should correctly inherit from the Infantry class and implement the IGuard interface.
 *The guard object should be initialized correctly as an instance of the Guards class using the provided shield inventory (shieldsInv).
 *The isRIP variable should be initialized correctly to false during the construction of an infantryGuards object.
 *The infantryGuards constructor should validate that all parameters (row, column, strength, attackRange) are valid values (non-negative) and throw an exception if any of them is negative.
 *The Block method in the infantryGuards class should delegate the blocking operation to the guard object and return the result without modifying the object's state.
 *The resistance method in the infantryGuards class should check if the infantryGuards object is in the RIP state before performing the resistance.
 *If the infantryGuards object is already in the RIP state, the resistance method should return false without modifying the object's state.
 *If the infantryGuards object is not in the RIP state, it should attempt to block the attack by calling the Block method and store the result in the blockResult variable.
 *If the blockResult is true, indicating a successful block, the resistance method should attempt to vanquish the target by calling the Target method and store the result in the VanquishResult variable.
 *If the blockResult is false, indicating a failed block, the resistance method should set the isRIP variable to true, indicating the death of the infantryGuards object.
 *The resistance method should return the VanquishResult, indicating whether the target was successfully resisted or not.
 */
namespace P5
{
	public class infantryGuards:Infantry, IGuard
	{

        private IGuard guard;
        private bool isRIP;


        //Constructor
        //Precond: All parameter are valid value (otherwise the parent classes will put exception)
        //Postcond: creates a InfantryGuard object.
        public infantryGuards(int row, int column, int strength, int attackRange, IArtilleryProvider artilleryProvider, IShieldProvider shieldsInv)
            :base(row, column, strength, attackRange, artilleryProvider)
		{
            guard = new Guards(shieldsInv);
            isRIP = false;
		}


        //Preond:valid x value (illegal value will be handled by the guard exception)
        //Postond: returns a bool value from the Guard Block
        public bool Block(int x)
        {
            return guard.Block(x);
        }

        // Blocks and attack back but may or may not be able to resists the opponenet.
        // Precond: has to be alive
        // Postond: reutrns whether if it was able to resists target, if not it dies.
        public bool resistance(int chosenShield, int x, int y, int p)
        {
            if (!isRIP)
            {
                bool blockResult = Block(chosenShield);

                if (blockResult)
                {
                    bool VanquishResult = Target(x, y, p);
                    return VanquishResult;

                }
                else
                {
                    isRIP = true;
                }
            }
            return false;
        }

    }
}

/*Implementation invariants
 *The infantryGuards class inherits from the Infantry class and implements the IGuard interface.
 *The guard object is initialized as an instance of the Guards class using the provided shield inventory.
 *The isRIP variable is initially set to false during the construction of an infantryGuards object.
 *The infantryGuards constructor validates that all parameters (row, column, strength, attackRange) are non-negative values, throwing an exception if any of them is negative.
 *The Block method delegates the blocking operation to the guard object and returns the result without modifying the object's state.
 *The resistance method checks if the infantryGuards object is in the RIP state before performing the resistance.
 *If the infantryGuards object is already in the RIP state, the resistance method returns false without modifying the object's state.
 *If the infantryGuards object is not in the RIP state, it attempts to block the attack by calling the Block method and stores the result in the blockResult variable.
 *If the blockResult is true, indicating a successful block, the resistance method attempts to vanquish the target by calling the Target method and stores the result in the VanquishResult variable.
 *If the blockResult is false, indicating a failed block, the resistance method sets the isRIP variable to true, indicating the death of the infantryGuards object.
 *The resistance method returns the VanquishResult, indicating whether the target was successfully resisted or not.
 * 
 */