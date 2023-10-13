/*Class Invariants

The Infantry object must have non-negative values for its row and column positions.
The Infantry object must have a positive strength and attack range.
The Infantry object must be initialized with a non-null IArtilleryProvider object.
The sum of the object's artillery array must be greater than or equal to 1.
The object's invalid request count must be less than or equal to the maximum invalid request bound, which is calculated based on the object's initial strength value.

 */


using System;

namespace P5
{
    public class Infantry : Fighter
	{

        // clas variables and constants
        private readonly int Max_Invalid_Req;
        private const int MIN_ARTILLERY = 1;
        //private readonly int[] initArtillery;
        //private readonly int initStrength;
        //private readonly int initAtkRange;


        private int prevRow;
        private int prevCol;
        private int invalidReqCount;


        // Precond:infRow, infCol may not be negative,artStrength, atkRange has to be positive
        //               ArtilleryProvider could not be null
        // Postcond: infantry class created
        public Infantry(int infRow, int infCol, int artStrength, int atkRange, IArtilleryProvider artilleryProvider)
            : base(infRow, infCol, artStrength, atkRange, artilleryProvider)
        {
            prevRow = -1;
            prevCol = -1;
            Max_Invalid_Req = invalidReqBoundCal(artStrength);
            //initStrength = artStrength;
            //initAtkRange = atkRange;
        }

        //Precond: valid InfStrength
        //Postcond: returns a num calculated by self-created formula
        private int invalidReqBoundCal(int InfStrength)
        {
            return (InfStrength) * 2 % 10;
        }

        //Precond: N/A
        //Postcond: returns true/false if strength has went below minimum
        private bool ActiveQualCheck()
        {
            return strength > MIN_STRENGTH;
        }

        //Precond:N/A
        //Postcond: returns true/false if artillery went below minimum
        private bool AliveQualCheck()
        {
            return artillery.Sum() > MIN_ARTILLERY;
        }

        //Precond:N/A
        //Postcond: sets to inactive if it exceeds bound, increments the invalid request everytime.
        //Helper function of doing the invalid bound
        private void invalidReqBoundCheck()
        {
            invalidReqCount++;
            if (invalidReqCount > Max_Invalid_Req)
            {
                isActive = false;
            }
        }

        //Precond: has to be active and alive, and the the position does not equal to the former coordinates.
        //Postcond: sets to new place, returns true, otherwise runs invalid bound and returns false.
        public override bool Move(int x, int y)
        {
            if (strength <= MIN_STRENGTH)
            {
                isActive = false;
                isAlive = false;
            }

            if (getIsActive() == true && getIsAlive() == true)
            {
                if (prevRow != x && prevCol != y)
                {
                    prevRow = row;
                    prevCol = column;

                    row = x;
                    column = y;

                    return true;
                }
            }

            invalidReqBoundCheck();
            return false;
           
        }

        //Precond: object has to be active and alive
        //Postcond: invalid bound check returns false if precond not satisfy, otherwise the range is assigned.
        public override bool Shift(int p)
        {
            if (strength <= MIN_STRENGTH)
            {
                isActive = false;
                isAlive = false;
            }


            if (getIsActive() == false || getIsAlive() == false)
            {
                invalidReqBoundCheck();
                return false;
            }
            attackRange = p;
            return true;
        }

        //Precond: object have to be inactive, but has to be alive
        //Postcond: objects reset to initial  range otherwise return false
        public bool reset()
        {
            if (getIsActive() == false && getIsAlive() == true)
            {
                strength = initStrength;
                attackRange = initAtkRange;
                artillery = initArtillery;
                isActive = ActiveQualCheck();
                isAlive = AliveQualCheck();
                invalidReqCount = 0;
                return true;
            }
            return false;

        }

       

    }
}

/* Implementation Invariants
The Infantry class must always be created with non-negative infRow and infCol and positive artStrength and atkRange values. The rtilleryProvider parameter cannot be null.
The invalidReqBoundCal method must take a valid infantry strength value and return a non-negative integer calculated by a self-created formula.
The ActiveQualCheck method must return true if the infantry's strength is greater than or equal to 0, and false otherwise.
The AliveQualCheck method must return true if the sum of the infantry's artillery is greater than or equal to 1, and false otherwise.
The invalidReqBoundCheck method must increment the invalidReqCount variable, and set the infantry's isActive field to false if invalidReqCount is greater than Max_Invalid_Req.
The Move method must return true if the infantry is active, alive, and its position is not equal to the previous coordinates, and false otherwise. If the method returns false, the invalidReqBoundCheck method must be called.
The Shift method must return true if the infantry is active, alive, and the attackRange value is assigned a valid value p, and false otherwise. If the method returns false, the invalidReqBoundCheck method must be called.
The reset method must return true if the infantry is inactive and alive, and reset the infantry's properties to their initial values. Otherwise, the method must return false.
*/