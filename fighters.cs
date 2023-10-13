
/*
 The row and column position of a Fighter object must be non-negative integers.
The strength and attackRange of a Fighter object must be positive integers.
The artillery array of a Fighter object must not be null.
The targetsVanquished counter of a Fighter object must be a non-negative integer.
The provArtillery field of a Fighter object must be initialized with a non-null IArtilleryProvider object.
The isActive and isAlive fields of a Fighter object must always be in a consistent state.
The Move method of a Fighter object must only move the object to a legal position.
The Shift method of a Fighter object must only set the attackRange to a legal value.
The Target method of a Fighter object must only vanquish the target if the object is alive, active, the distance is less than or equal to the attackRange, and the strength is greater than or equal to the target's strength.

 */



using System;

namespace P5{

    // Interface that declares a method to retrieve artillery for the fighters
    public interface IArtilleryProvider
    {
        int[] retrieveArtillery();
    }

    // Class that provides artillery for the fighters
    public class ArtilleryProvider : IArtilleryProvider
    {
        private readonly int[] artilleryInv;

        public ArtilleryProvider(int[] artilleryP)
        {
            if (artilleryP.Length <= 0)
            {
                throw new ArgumentException("The artillery Inventory Must have at least one shield");
            }
            for (int i = 0; i < artilleryP.Length; i++)
            {
                if (artilleryP[i] <= 0)
                {
                    throw new ArgumentException("Shields (value below validity no negative or 0) invalid durability");
                }
            }
            artilleryInv = artilleryP;
           
        }
        
        public int[] retrieveArtillery()
        {
            return artilleryInv;
        }
    }

    
    public abstract class Fighter
    {
        // class variables
        protected readonly int MIN_STRENGTH;

        protected int row;
        protected int column;
        protected int strength;
        protected int attackRange;
        protected int[] artillery;
        protected int targetsVanquished;
        protected IArtilleryProvider provArtillery;

        protected readonly int[] initArtillery;
        protected readonly int initStrength;
        protected readonly int initAtkRange;

        protected bool isAlive;
        protected bool isActive;



        // PreCondition: posRow, posCol may not be negative,roleStrength, atkRange has to be positive
        //               Artillery could not be null
        // Post condition Fighter object created with given specs
        public Fighter(int posRow, int posCol, int roleStrength, int atkRange, IArtilleryProvider proviArtillery)
        {
            row = posRow;
            column = posCol;
            strength = roleStrength;
            attackRange = atkRange;
            provArtillery = proviArtillery;
            artillery = provArtillery.retrieveArtillery();
            targetsVanquished = 0;
            isAlive = true;
            isActive = true;

            initStrength = roleStrength;
            initAtkRange = atkRange;
            initArtillery = provArtillery.retrieveArtillery();

            MIN_STRENGTH = artillery[artillery.Length - 1] % 2;

        }

        //Precond: N/A
        // Postcond: return true/false for is Active
        public bool getIsActive(){
            return isActive;
        }

        //Precond: N/A
        // Postcond: return true/false for is Alive
        public bool getIsAlive() {
            return isAlive;
        }

        //Precond: N/A
        // Postcond: return row position
        public int getRow()
        {
            return row;

        }

        //Precond: N/A
        //Postcond: return column position
        public int getCol()
        {
            return column;
        }

        //Precond: x, y are in the boundaries
        //Postcond: objects moves position if legal
        public abstract bool Move(int x, int y);

        //precond: p is a non-negative integer
        // postCond:set the atkRange, if Legal
        public abstract bool Shift(int p);

        //precond: fighter must be alive and active, x,y must be positive, and q must be valid
        //Postcond: true if the target is vanquiahsed and increments amount, false if it did not vanquish.
        public virtual bool Target(int x, int y, int q){


            if (strength <= MIN_STRENGTH)
            {
                isActive = false;
                isAlive = false;
            }


            if (isAlive == false || isActive == false)
            {
                return false;
            }

            int distance = Math.Abs(column - y) + Math.Abs(row - x);
            for(int i = 0; i < artillery.Length; i++)
            {
             int artilleryVal = artillery[i];
             if (artilleryVal <= strength && distance <= attackRange)
             {
                targetsVanquished++;
                return true;
              }
            }

            return false;
        }

        //Preond: N/A
        // Postcond: returns amount of target vanquished
        public int Sum()
        {
            return targetsVanquished;
        }

    }
    

}



/*
 Implementation invariants

The position of the fighter should always be non-negative.
The strength and attack range of the fighter should always be positive.
The artillery provided to the fighter should not be null.
The fighter should always start alive and active.
The isActive and isAlive variables should only be changed through the methods provided in the Fighter class.
The Move and Shift methods should only be called when the fighter is alive and active, and the new positions or attack range values are legal.
The Target method should only be called when the fighter is alive and active, the target position is positive, and the artillery is valid.
The Sum method should always return the total number of targets vanquished by the fighter.
 */