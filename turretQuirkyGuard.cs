using System;


/*Class invariants
 * The turretQuirkyGuard class should correctly inherit from the Turret class and implement the IGuard interface.
 *The quirkyGuard variable should be initialized correctly as an instance of the quirkyGuard class.
 *The isRIP variable should be initialized correctly to false during the construction of a turretQuirkyGuard object.
 *The constructor of the turretQuirkyGuard class should validate that all parameters (row, column, strength, attackRange) are valid values and throw an exception if any of them is negative.
 *The Block method in the turretQuirkyGuard class should correctly delegate the blocking operation to the quirkyGuard object and return the result.
 *The selfDestruction method in the turretQuirkyGuard class should check if the turretQuirkyGuard is not in the RIP state before performing the self-destruction.
 *The selfDestruction method should perform the attack and defense simultaneously by calling the Target method from the Turret class and the Block method from the quirkyGuard object.
 *If the conditions for self-destruction are met (successful attack and block), the selfDestruction method should set the isRIP variable to true and return true.
 *If the turretQuirkyGuard is already in the RIP state, the selfDestruction method should return false.
 *If any of the conditions for self-destruction are not met (failed attack or block), the selfDestruction method should return false.
 */
namespace P5
{
	public class turretQuirkyGuard: Turret, IGuard
	{

        private IGuard quirkyGuard;
        private bool isRIP;

        //Constructor
        //Precond: All parameter are valid value (otherwise the parent classes will put exception)
        //Postcond: creates a turretQuirkyGuard object.
        public turretQuirkyGuard(int row, int column, int strength, int attackRange, IArtilleryProvider artilleryProvider, IShieldProvider shieldsInv)
            : base(row, column, strength, attackRange, artilleryProvider)
		{
            quirkyGuard = new quirkyGuard(shieldsInv);
            isRIP = false;
		}

        //Preond:valid x value (illegal value will be handled by the quirkyguard exception)
        //Postond: returns a bool value from the quickGuard Block
        public bool Block(int x)
        {
            return quirkyGuard.Block(x);
        }


        // it does attack and defense at the same time, which it self destructs in the range, oss it dies, but eliminates enemy.
        // Precond: it is able to vanquish target and block ( which kept the attack to itself) explosive object,
        //           object is not dead
        //Postond:self-destructs if condition met, otherwise it stays the same.
        public bool selfDestruction(int x, int y, int q, int chosenShield)
        {
            if (!isRIP)
            {
                bool attackResult = Target(x, y, q);
                bool blockResult = Block(chosenShield);
                if (attackResult && blockResult)
                {
                    isRIP = true;
                    return true;
                }
                 return false;
            }
            return false;
       }
            
    }
}

/*Implementation invariants
 * 
 * 
 * The turretQuirkyGuard class should correctly inherit from the Turret class and implement the IGuard interface.
 *The quirkyGuard variable should be initialized correctly as an instance of the quirkyGuard class using the provided shield inventory (shieldsInv).
 *The isRIP variable should be initialized correctly to false during the construction of a turretQuirkyGuard object.
 *The turretQuirkyGuard constructor should validate that all parameters (row, column, strength, attackRange) are valid values (non-negative) and throw an exception if any of them is negative.
 *The Block method in the turretQuirkyGuard class should correctly delegate the blocking operation to the quirkyGuard object and return the result.
 *The selfDestruction method in the turretQuirkyGuard class should correctly check if the turretQuirkyGuard is not in the RIP state before performing the self-destruction.
 *The selfDestruction method should correctly perform the attack by calling the Target method from the Turret class and return the result.
 *The selfDestruction method should correctly perform the block operation by calling the Block method from the quirkyGuard object and return the result.
 *If the conditions for self-destruction are met (successful attack and block), the selfDestruction method should correctly set the isRIP variable to true.
 *If the turretQuirkyGuard is already in the RIP state, the selfDestruction method should correctly return false.
 *If any of the conditions for self-destruction are not met (failed attack or block), the selfDestruction method should correctly return fals
 */