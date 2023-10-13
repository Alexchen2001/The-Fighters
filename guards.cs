using System;




/*Class Invariant
 * The shield inventory must have at least one shield.
 * Shields in the shield inventory must have a positive durability.
 * The number of shields in down mode must be below or equal to the down mode limit.
 * The number of usable shields must be above the dead state limit for the guard to be alive.
 *The index used for blocking must be non-negative and within the range of the shield inventory.
 */
namespace P5
{
   
    public interface IShieldProvider
    {
        int[] retrieveShieldInv();
    }

    // Class that provides shields for the guards
    public class shieldProvider : IShieldProvider
    {
        private readonly int[] shieldInv;

        public shieldProvider(int[] shieldP)
        {
            if(shieldP.Length <= 0)
            {
                throw new ArgumentException("The shield Inventory Must have at least one shield");
            }
            for(int i = 0; i < shieldP.Length; i++)
            {
                if (shieldP[i] <= 0)
                {
                    throw new ArgumentException("Shields (value below validity no negative or 0) invalid durability");
                }
            }
            shieldInv = shieldP;

        }

        public int[] retrieveShieldInv()
        {
            return shieldInv;
        }
    }

    
    public class Guards: IGuard
    {
        private readonly int DOWN_MODE_LIMIT;
        private readonly int DEAD_STATE_LIMIT;

        protected int[] shieldDurability;
        protected bool[] shieldStatus;
        protected bool inUpMode;
        protected bool isAlive;
        protected IShieldProvider provShield;


        // Pre-cond: the provided array of shield is valid
        //Post-cond: guard object created with given array of shields
        public Guards(IShieldProvider proviShield)
        {
            provShield = proviShield;
            shieldDurability = proviShield.retrieveShieldInv();
            shieldStatus = new bool[shieldDurability.Length];
            inUpMode = true;
            isAlive = true;
            DOWN_MODE_LIMIT = (shieldDurability.Length/3) * 2 ;
            DEAD_STATE_LIMIT = shieldDurability.Length / 2; 
            for (int i = 0; i < shieldDurability.Length; i++)
            {
                shieldStatus[i] = true;
            }
        }

        //Pre-cond: N/A
        //Post-cond: if the usable shields went below the dead limit, then it is no longer active
        //           otherwise  it is still alive.
        private void checkIsAlive()
        {
            int viableShieldsCount = 0;
            bool shieldViable;
            for (int i = 0; i < shieldDurability.Length; i++)
            {
                if (shieldStatus[i] && shieldDurability[i] > 0)
                {
                    viableShieldsCount++;
                }
            }

            shieldViable = viableShieldsCount > DEAD_STATE_LIMIT;
            if (!shieldViable)
            {
                isAlive = false;
            }
        }


        // Pre-cond:N/A
        // Post-cond: if limit goes below or equal to the down mode threshhold, then it would go to down mode
        //            otherwise stay in up mode.
        private void downModeChange() {
            int viableShieldsCount = 0;
            bool shieldViable;
            for (int i = 0; i < shieldDurability.Length; i++)
            {
                if (shieldStatus[i])
                {
                    viableShieldsCount++;
                }
            }

            shieldViable = viableShieldsCount > DOWN_MODE_LIMIT;
            if (!shieldViable)
            {
                inUpMode = false;
            }
        }

        //Pre-cond: x is a valid value (non negative) also within the range of the
        //            the guard object shield inventory, the guard object is still alive
        //Post-cond:in up mode then with valid shield then derement, in down mode then the shields is completely used
        //            otherwise block fails returns false.
        public virtual bool Block(int x)
        {
            checkIsAlive();
            downModeChange();

            if(x < 0)
            {
                throw new ArgumentException("Invalid index for block, value are not allowed to be negative.");
            }

            if (!isAlive)
            {
                 return false;
            }
            
            if (x >= 0 && x < shieldDurability.Length && isAlive)
            {
                if (inUpMode && shieldStatus[x] && shieldDurability[x] > 0)
                {
                    shieldDurability[x]--;

                    if (shieldDurability[x] <= 0)
                    {
                        shieldStatus[x] = false;
                    }
                    return true;
                }
                else if ((!inUpMode) && shieldStatus[x] )
                {
                    shieldStatus[x] = false;
                    shieldDurability[x] = 0;
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
 * The shield inventory array (shieldDurability) and shield status array (shieldStatus) must have the same length.
 * The shield inventory array (shieldDurability) and shield status array (shieldStatus) must be initialized with values for all shields.
 * The shield inventory array (shieldDurability) and shield status array (shieldStatus) must be updated consistently when shields are used or their status changes.
 * The inUpMode flag should reflect the current mode based on the shield inventory status.
 * The isAlive flag should accurately represent the guard's state based on the shield inventory status.
 * The down mode limit (DOWN_MODE_LIMIT) should be calculated correctly based on the shield inventory length.
 * The dead state limit (DEAD_STATE_LIMIT) should be calculated correctly based on the shield inventory length.
 * The Block method should correctly handle edge cases and validate the input index (x).
 * The Block method should update the shield inventory and status arrays appropriately based on the guard's mode and shield durability.
 * The Block method should return the correct result (true or false) based on the success or failure of the block operation.
 * The checkIsAlive method should accurately determine if the guard is alive based on the shield inventory status.
 * The downModeChange method should accurately determine if the guard is in the down mode based on the shield inventory status.
 * 
 * 
 */