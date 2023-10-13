using System;

/*Class invariants
 * The infantryQuirkyGuard class inherits from the Infantry class and implements the IGuard interface.
 *The quirkyGuard object is initialized as an instance of the quirkyGuard class using the provided shield inventory.
 *The isRIP variable is not initialized or used in the provided code. However, it could potentially be used to track the state of the infantryQuirkyGuard object.
 *The infantryQuirkyGuard constructor validates that all parameters (row, column, strength, attackRange) are non-negative values, throwing an exception if any of them is negative.
 *The Block method delegates the blocking operation to the quirkyGuard object and returns the result without modifying the object's state.
 *The strikes method checks if the infantryQuirkyGuard object is in the RIP state before performing the strike.
 *If the infantryQuirkyGuard object is already in the RIP state, the strikes method returns false without modifying the object's state.
 *If the infantryQuirkyGuard object is not in the RIP state, it attempts to vanquish the target by calling the Target method and stores the result in the VanquishResult variable.
 *If the VanquishResult is true, indicating a successful vanquish, the strikes method attempts to block any incoming attack by calling the Block method and returns the blockResult.
 *If the VanquishResult is false, indicating a failed vanquish, the strikes method sets the isRIP variable to true, indicating the death of the infantryQuirkyGuard object.
 *The strikes method returns the blockResult, indicating whether the incoming attack was successfully blocked or not.
 */
namespace P5
{
	public class infantryQuirkyGuard:Infantry, IGuard
	{
		private IGuard quirkyGuard;
		private bool isRIP;

        //Constructor
        //Precond: All parameter are valid value (otherwise the parent classes will put exception)
        //Postcond: creates a infantryQuirkyGuard object.
        public infantryQuirkyGuard(int row, int column, int strength, int attackRange, IArtilleryProvider artilleryProvider, IShieldProvider shieldsInv)
			:base(row, column, strength, attackRange, artilleryProvider)
		{
			quirkyGuard = new quirkyGuard(shieldsInv);
		}

        // Attacks, if it fails it would die, but if it success from attack it will block any attack from incoming.
        //Preond:valid x value (illegal value will be handled by the guard exception)
        //Postond: returns a bool value from the quirckGuard Block
        public bool Block(int x)
        {
			return quirkyGuard.Block(x);
        }

        public bool strikes(int x,int y, int p, int chosenShield)
        {
            if (!isRIP)
            {
                
                bool VanquishResult = Target(x, y, p);
                if (VanquishResult)
                {
                    bool blockResult = Block(chosenShield);
                    return blockResult ;

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
 * The infantryQuirkyGuard class inherits from the Infantry class and implements the IGuard interface.
 *The quirkyGuard object is initialized as an instance of the quirkyGuard class using the provided shield inventory.
 *The isRIP variable is not utilized in the provided code, and its purpose or behavior is not clear.
 *The infantryQuirkyGuard constructor ensures that all parameters (row, column, strength, attackRange) are non-negative values, throwing an exception if any of them is negative.
 *The Block method delegates the blocking operation to the quirkyGuard object and returns the result without modifying the state of the infantryQuirkyGuard object.
 *The strikes method checks if the infantryQuirkyGuard object is in the RIP state before performing the strike.
 *If the infantryQuirkyGuard object is already in the RIP state, the strikes method returns false without modifying the state of the infantryQuirkyGuard object.
 *If the infantryQuirkyGuard object is not in the RIP state, it attempts to vanquish the target by calling the Target method and stores the result in the VanquishResult variable.
 *If the VanquishResult is true, indicating a successful vanquish, the strikes method attempts to block any incoming attack by calling the Block method and returns the blockResult.
 *If the VanquishResult is false, indicating a failed vanquish, the strikes method sets the isRIP variable to true, potentially indicating the death or disabling of the infantryQuirkyGuard object. However, the purpose and consequences of this variable are not explicitly defined in the code.
 *The strikes method returns the blockResult, indicating whether the incoming attack was successfully blocked or not.
 * 
 */