using System;

/*Class invariants
 * he turretGuard class should inherit correctly from the Turret class and implement the IGuard interface.
 *The guard variable should be initialized correctly as an instance of the Guards class.
 * The isRIP variable should be initialized correctly to false during the construction of a turretGuard object.
 *T he constructor of the turretGuard class should validate that all parameters (row, column, strength, attackRange) are valid values and throw an exception if any of them is negative.
 *The Block method in the turretGuard class should delegate the blocking operation to the guard object and return the result.
 *The retaliation method in the turretGuard class should check if the turretGuard is alive (not in RIP state) before performing the retaliation.
 *The retaliation method should validate that the target is within range and the block operation is successful before attempting the attack.
 *The retaliation method should return true if the conditions for retaliation are met and the attack is successful, otherwise return false.
 *The retaliation method should handle the case when the turretGuard is already in RIP state and return false.
 */
namespace P5
{
	public class turretGuard: Turret,IGuard
	{

		private IGuard guard;
        private bool isRIP;

        //Constructor
        //Precond: All parameter are valid value (otherwise the parent classes will put exception)
        //Postcond: creates a turretGuard object
        public turretGuard(int row, int column, int strength, int attackRange, IArtilleryProvider artilleryProvider, IShieldProvider shieldsInv)
            : base(row, column, strength, attackRange, artilleryProvider)
        {
            if (row < 0 || column < 0 || strength < 0 || attackRange < 0)
            {
                throw new ArgumentException("No values should be negative!");
            }

            guard = new Guards(shieldsInv);
            isRIP = false;
        }


        //Preond:valid x value (illegal value will be handled by the guard exception)
        //Postond: returns a bool value from the Guard Block
        public bool Block(int x)
        {
            return guard.Block(x);
        }

        //if the target is in range, then it bloks the attack and it tries to vanquish the target., otherwise it fails, but
        //  do not die
        //Precond: must be alive, and that it bloaced and the person is whithin range
        //Postcond: reutrn true if condition is met, otherwise false
        public bool retaliation(int x, int y, int q,int p, int chosenShield)
        {
            if (!isRIP)
            {
                bool rangeIsValid = Shift(p);
                bool blockValid = Block(chosenShield);
                // If the attack was successful, use QuirkyGuard's Block method
                if (rangeIsValid && blockValid )
                {
                    bool attackResult = Target(x, y, q);
                    return attackResult;
                }

                return false;
            }
            return false;
            
        }
    }


}


/*Implementation invariants
 * The turretGuard class should correctly inherit from the Turret class and implement the IGuard interface.
 *The guard variable should be initialized correctly as an instance of the Guards class using the provided shield inventory (shieldsInv).
 *The isRIP variable should be initialized correctly to false during the construction of a turretGuard object.
 *The turretGuard constructor should validate that all parameters (row, column, strength, attackRange) are valid values (non-negative) and throw an exception if any of them is negative.
 *The Block method in the turretGuard class should correctly delegate the blocking operation to the guard object and return the result.
 *The retaliation method in the turretGuard class should correctly check if the turretGuard is alive (not in the RIP state) before performing the retaliation.
 *The retaliation method should correctly validate that the target is within range by calling the Shift method from the Turret class.
 *The retaliation method should correctly validate the success of the block operation by calling the Block method from the guard object.
 *If the retaliation conditions are met (alive, in range, successful block), the retaliation method should correctly perform the attack by calling the Target method from the Turret class and return the result.
 *If the turretGuard is already in the RIP state, the retaliation method should return false.
 *If any of the conditions for retaliation are not met (not alive, out of range, failed block), the retaliation method should return false.
 * 
 * 
 */