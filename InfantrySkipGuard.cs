using System;

/*Class invariants
 * The infantrySkipGuard class inherits from the Infantry class and implements the IGuard interface.
 *The skipGuard object is initialized as an instance of the skipGuards class using the provided shield inventory.
 *The isRIP variable is initially set to false, indicating that the infantrySkipGuard object is not in the RIP state.
 *The infantrySkipGuard constructor ensures that all parameters (row, column, strength, attackRange) are non-negative values, throwing an exception if any of them is negative.
 *The Block method delegates the blocking operation to the skipGuard object and returns the result without modifying the state of the infantrySkipGuard object.
 *The dodgeAtk method checks if the infantrySkipGuard object is in the RIP state before performing the dodge and attack.
 *If the infantrySkipGuard object is already in the RIP state, the dodgeAtk method returns false without modifying the state of the infantrySkipGuard object.
 *If the infantrySkipGuard object is not in the RIP state, it attempts to move to a new position by calling the Move method with modified coordinates (x + 1, y + 1) and stores the result in the moveIsValid variable.
 *It also attempts to block any incoming attack by calling the Block method with the chosenShield and stores the result in the blockIsValid variable.
 *If both the moveIsValid and blockIsValid are true, indicating a successful move and block, the dodgeAtk method attempts to vanquish the target by calling the Target method with the provided coordinates (x, y, q) and returns the VanquishIsValid result.
 *If either the moveIsValid or blockIsValid is false, indicating a failed move or block, the dodgeAtk method returns false without performing the attack.
 *If the infantrySkipGuard object is in the RIP state, the dodgeAtk method returns false without modifying the state of the infantrySkipGuard object.
 * 
 */
namespace P5
{
	public class infantrySkipGuard: Infantry, IGuard
	{
		private IGuard skipGuard;
		private bool isRIP;

        //Constructor
        //Precond: All parameter are valid value (otherwise the parent classes will put exception)
        //Postcond: creates a infantrySkipGuard object.
        public infantrySkipGuard(int row, int column, int strength, int attackRange, IArtilleryProvider artilleryProvider, IShieldProvider shieldsInv)
			:base(row, column, strength, attackRange, artilleryProvider)
		{

			skipGuard = new skipGuards(shieldsInv);
            isRIP = false;
		}


        //Preond:valid x value (illegal value will be handled by the guard exception)
        //Postond: returns a bool value from the skipGuard Block
        public bool Block(int x)
        {
			return skipGuard.Block(x);
        }

        public bool dodgeAtk(int x, int y, int q, int chosenShield)
        {

            if (!isRIP)
            {
                bool moveIsValid = Move(x + 1, y + 1);
                bool blockIsValid = Block(chosenShield);

                // If the attack was successful, use QuirkyGuard's Block method
                if (moveIsValid && blockIsValid)
                {
                    bool VanquishIsValid = Target(x, y, q);
                    return VanquishIsValid;
                }

                return false;
            }
            return false;

        }

    }
}


/*Imlementation invariants
 * The infantrySkipGuard class inherits from the Infantry class and implements the IGuard interface.
 *The skipGuard object is initialized as an instance of the skipGuards class using the provided shield inventory.
 *The isRIP variable is initially set to false.
 *The infantrySkipGuard constructor ensures that all parameters (row, column, strength, attackRange) are non-negative values, throwing an exception if any of them is negative.
 *The Block method delegates the blocking operation to the skipGuard object and returns the result without modifying the state of the infantrySkipGuard object.
 *The dodgeAtk method checks if the infantrySkipGuard object is in the RIP state before performing the dodge and attack.
 *If the infantrySkipGuard object is already in the RIP state, the dodgeAtk method returns false without modifying the state of the infantrySkipGuard object.
 *If the infantrySkipGuard object is not in the RIP state, it attempts to move to a new position by calling the Move method with modified coordinates (x + 1, y + 1).
 *It also attempts to block any incoming attack by calling the Block method with the chosenShield.
 *If both the move and block operations are successful, the dodgeAtk method attempts to vanquish the target by calling the Target method with the provided coordinates (x, y, q) and returns the result.
 *If either the move or block operation fails, the dodgeAtk method returns false without performing the attack.
 *If the infantrySkipGuard object is in the RIP state, the dodgeAtk method returns false without modifying the state of the infantrySkipGuard object.
 * 
 */