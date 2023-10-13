/*Class invariants
 The invalidReqCount field should always be non-negative.
The Max_Invalid_Req field should always be non-negative.
The aimDist field should always be non-negative.
The strength field should always be non-negative.
The attackRange field should always be non-negative.
The targetsVanquished field should always be non-negative.
The provArtillery field should never be null.
The artillery array should never be null.
The row and column fields should always be non-negative.
The isActive and isAlive fields should always have a consistent state, such that if isActive is false, isAlive should also be false.
*/



using System;

namespace P5
{
    public class Turret : Fighter
	{
        // class variables and consts
		private int aimDist;
        private int invalidReqCount;
        private readonly int Max_Invalid_Req;
		private bool isRIP;

        // Precond:posRow, posCol may not be negative,turStrength, atkRange has to be positive
        //               ArtilleryProv could not be null
        // Postcond: infantry class created
        public Turret(int posRow, int posCol, int turStrength, int atkRange, IArtilleryProvider artilleryProv)
			:base(posRow, posCol, turStrength, atkRange, artilleryProv)
		{
			aimDist = atkRange;
			isRIP = false;
            Max_Invalid_Req = invalidReqBoundCal(turStrength);
		}

        //Precond: N/A
        // Postcond: returns RIP status
        public bool getIsRIP()
        {
            return isRIP;
        }

        //Precond: N/A
        //Postcond: finds the invalid request bound
        private int invalidReqBoundCal(int turStrength) 
        {
            return (turStrength) * 2 % 10;
        }

        //Precond:N/A
        //Postond:sets both status to false to trigger RIP
        private void RIPstatus()
        {
            isActive = false;
            isAlive = false;
        }

        //Precond: checks for invalid requests if exceeded
        //Postcond: become RIP and change all status to false, otherwise nothing happens
        private void InvalidReqBoundCheck()
        {
            invalidReqCount++;
            if (invalidReqCount > Max_Invalid_Req)
            {
                isRIP = true;
                RIPstatus();
            }
        }

        //Precond: N/A
        // Postcond: invalid request, turret not allowed to move
        public override bool Move(int x, int y)
        {
            if (strength <= MIN_STRENGTH)
            {
                isActive = false;
                isAlive = false;
            }

            InvalidReqBoundCheck();
            return false;
        }


        //Precond:checks for invalid distance
        //Postcond:returns false if it is invalid, returns true if distance are valid
        public override bool Shift(int p)
        {
            if (strength <= MIN_STRENGTH)
            {
                isActive = false;
                isAlive = false;
            }

            // Invalid distance
            if (p < 0 || p > row){ 
                InvalidReqBoundCheck();
                return false;
            }else
            {
                aimDist = p;
                revive();           
                return true;
            }
        }


        //Precond: cehcks for status of RIP /active/ alive
        //Postcond: calls the fighter target class
        public override bool Target(int x, int y, int q)
        {
            if (strength <= MIN_STRENGTH)
            {
                isActive = false;
                isAlive = false;
            }


            if (isRIP == false && getIsActive() && getIsAlive())
            {
                return base.Target(x, y, q);
            }
            return false;

        }

        //Precond:if it is not fully dead
        //Postcond: the strength will be set to 100, regardless of initial state (revive boost)
        private void revive()
        {
            if (isRIP == false)
            {
                strength = 100;
            }
        }

    }
}

/*
*The row and column values of a Turret object must not be negative.
(The strength and attackRange values of a Turret object must be positive.
The artilleryProv parameter of the Turret constructor must not be null.
The aimDist value of a Turret object must be less than or equal to its attackRange value.
The Max_Invalid_Req value of a Turret object must be calculated based on its strength value.
The invalidReqCount value of a Turret object must be less than or equal to its Max_Invalid_Req value.
If the invalidReqCount value of a Turret object exceeds its Max_Invalid_Req value, then the object becomes RIP and all its status (isActive and isAlive) are set to false.
If a Turret object is RIP, then its isActive and isAlive status are false.
If a Turret object is not RIP, then its strength value can be revived to 100.
The Target method of a Turret object can only be called if the object is not RIP and its isActive and isAlive status are true. Otherwise, the method returns false.
The Move method of a Turret object does not change any status of the object.
The Shift method of a Turret object can only be called if the given distance value is non-negative and less than or equal to the object's row value. Otherwise, the method returns false.

 */