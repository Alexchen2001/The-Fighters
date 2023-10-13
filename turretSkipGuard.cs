using System;

/*Class invariants
 * The turretSkipGuard class should correctly inherit from the Turret class and implement the IGuard interface.
 *The skipGuard variable should be initialized correctly as an instance of the skipGuards class using the provided shield inventory (shieldsInv).
 *The isRIP variable should be initialized correctly to false during the construction of a turretSkipGuard object.
 *The turretSkipGuard constructor should validate that all parameters (row, column, strength, attackRange) are valid values (non-negative) and throw an exception if any of them is negative.
 *The Block method in the turretSkipGuard class should correctly delegate the blocking operation to the skipGuard object and return the result.
 *The feignedTerror method in the turretSkipGuard class should correctly check if the turretSkipGuard is in the RIP state before performing the feigned terror.
 *The feignedTerror method should correctly check if the turretSkipGuard is not able to move (moveIsValid) and the block operation is successful (blockIsValid) before scaring other objects.
 *If the turretSkipGuard is already in the RIP state, the feignedTerror method should correctly return false.
 *If the turretSkipGuard is not in the RIP state, and it is unable to move (moveIsValid) and the block operation is successful (blockIsValid), the feignedTerror method should correctly set the isRIP variable to false and return true.
 *If any of the conditions for feigned terror are not met (able to move or failed block operation), the feignedTerror method should correctly return false.
 * 
 */
namespace P5
{

    // Not DONE
	public class turretSkipGuard: Turret, IGuard
	{

		private IGuard skipGuard;
        private bool isRIP;

        //Constructor
        //Precond: All parameter are valid value (otherwise the parent classes will put exception)
        //Postcond: creates a turretSkipGuard object
        public turretSkipGuard(int row, int column, int strength, int attackRange, IArtilleryProvider artilleryProvider, IShieldProvider shieldsInv)
			: base(row, column, strength, attackRange, artilleryProvider)
		{
			skipGuard = new skipGuards(shieldsInv);
            isRIP = false;
		}

        //Preond:valid x value (illegal value will be handled by the skipguard exception)
        //Postond: returns a bool value from the skipGuard Block
        public bool Block(int x)
        {
			return skipGuard.Block(x);
        }

        //they are dead (theoreticall) so they can't move,and it blocks any attacks in coming, therefore it would scare
        // other objects, but it does not cause any damanage, otherwise it is none workable.
        //Precond: the object must be dead and it is unable to move and with a valid shield so it doesn't disappear
        //Postcond: returns true if it satisfy the condition, otherwise it could no scare other objects.
        public bool feignedTerror(int x, int y, int chosenShield)
        { 
            if (getIsRIP())
            {
                isRIP = false;
            }

            if (!isRIP)
            {
                bool moveIsValid = Move(x, y);
                bool blockIsValid = Block(chosenShield);

                // If the attack was successful, use QuirkyGuard's Block method
                if (!moveIsValid && blockIsValid)
                {
                    isRIP = false;
                    return true;
                }

                return false;
            }
            return false;
            
        }


    }
}


/*Implementation invariants
 * The turretSkipGuard class should correctly inherit from the Turret class and implement the IGuard interface.
 *The skipGuard object should be initialized correctly as an instance of the skipGuards class using the provided shield inventory (shieldsInv).
 *The isRIP variable should be initialized correctly to false during the construction of a turretSkipGuard object.
 *The turretSkipGuard constructor should validate that all parameters (row, column, strength, attackRange) are valid values (non-negative) and throw an exception if any of them is negative.
 *The Block method in the turretSkipGuard class should delegate the blocking operation to the skipGuard object and return the result without modifying the object's state.
 *The feignedTerror method in the turretSkipGuard class should check if the turretSkipGuard is in the RIP state before performing the feigned terror.
 *If the turretSkipGuard is already in the RIP state, the feignedTerror method should return false without modifying the object's state.
 *If the turretSkipGuard is not in the RIP state, and it is unable to move (moveIsValid) and the block operation is successful (blockIsValid), the feignedTerror method should set the isRIP variable to false and return true.
 *If any of the conditions for feigned terror are not met (able to move or failed block operation), the feignedTerror method should return false without modifying the object's state.
 * 
 */