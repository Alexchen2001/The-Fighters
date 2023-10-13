//Alexander Chen SUID: 4186272
// Platform: MAC OS, Visual Studio
// 6/2/2023

/* Design:
 * 
 * is a simple game where we have two types of fighters: turrets and infantry.
 * Both types of fighters have a common abstract class called Fighter which implements some common 
 * functionality such as movement, targetting and tracking the number of targets vanquished. Each fighter also has some 
 * unique functionality and restrictions specific to its type.he Fighter class takes in the position, strength, attack range
 * and an IArtilleryProvider object as inputs, which provides the fighter with artillery. The Move and Shift methods are used 
 * to move the fighter to a new position or change the attack range respectively. The Target method is used to check if a target can be 
 * vanquished and the Sum method is used to keep track of the total number of targets vanquished.
 * All functionality will majorly rely on the status of whether if it is alive or dead, active or inactive
 * The guard is a defensive object (character) that only does minimal functionality such as blocks, every guard object has its diffeent
 * features for blocing with certain restrictions or special way of blocking, but they share a similar trait is that 
 * they would only support blocing and DO NOT support excessive functionality that is not specified here. There are cross product  classes of
 * object that is available for example like turretGuard etc. There is one thing to note that other types of cross product objects are not curretnly
 * supported other than specified, every cross product other than inheriting from their parents class either from one of the objects from the 
 * fighter hiearchy or the guards hiearchy it will has its own unique funcitonality that utilizes the parents functinliaty to activate.
 * Note that each class has its own limitations in different data fields that it may possess, and anot every cross product have similar logic.
 * 
 * NOTE: this is all based on the assumption that the client is inputing valid values, whih would not trigger and error prone issues of the fucntionalities.
 * There are some classes that only uses isRIP as a idea Not necessarily meaning that is dead, but rather in a fake dead state.
 * All cases below are allowing to have valid blocks and invalid blocks also with valid and invalid  unique functionality from the cross product classes objects.
 * 
 * The guards objects below demonstrates valid blocks calls, as the range is arefully chosen to demonstrate the validity of the guard hiearchy objects
 * 
 * All input are assume that it will go through method dependency injection to create a safe injection.
 */

using System;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using P5;


namespace P5
{
    class P5
    {
        static void Main()
        {

            Random random = new Random();

            // creats a array of weapon 
            int[] ammo = new int[10];
            for (int i = 0; i < 10; i++)
            {
                ammo[i] = random.Next(0, 150);
            }

            // creates an array of shields
            int[] shields = new int[10];
            for (int i = 0; i < 10; i++)
            {
                shields[i] = random.Next(1, 150);
            }

            // Initializing artillery and shields
            IArtilleryProvider artilleryProvider = new ArtilleryProvider(ammo);
            IShieldProvider shieldProvider = new shieldProvider(shields);

            // creates random amt of objecrs for normal guards and composite guards object.
            int amtOfGuards = random.Next(10, 20);
            int amtOfCompGuards = random.Next(20, 40);

            //creates list of guards and list of composite guards.
            List<IGuard> diffGuards = new List<IGuard>();
            List<IGuard> compGuards = new List<IGuard>();
            List<int> targets = new List<int>();

            // Initialized different guards in a heterogeneous list
            for (int i = 0; i < amtOfGuards; i++)
            {

                if (random.NextDouble() < 0.3)
                {
                    diffGuards.Add(new Guards(shieldProvider));
                }
                else if (random.NextDouble() >=0.3 && random.NextDouble() <= 0.6)
                {
                    diffGuards.Add(new quirkyGuard(shieldProvider));
                }
                else
                {
                    diffGuards.Add(new skipGuards(shieldProvider));
                }
            }

            int numOfModeChanges = random.Next(10, 30);

            // exports the result of the guards block.
            for (int i = 0; i < numOfModeChanges; i++)
            {
                int guardIndex = random.Next(0, diffGuards.Count);

                int x = random.Next(0, 10);
  
                bool result;

                result = diffGuards[guardIndex].Block(x);
                // decides which guard it is by object
                if (diffGuards[guardIndex] is Guards)
                {
                    Console.WriteLine($"guard {guardIndex} :guard, Blocked? {result}");
                }
                else if (diffGuards[guardIndex] is skipGuards)
                {
                    Console.WriteLine($"guard {guardIndex} :skipGuard, Blocked? {result}");
                }
                else if (diffGuards[guardIndex] is quirkyGuard)
                {
                    Console.WriteLine($"guard {guardIndex} :quirkyGuard, Blocked? {result}");
                }
                       
            }


            // initialializes a list (heterogenous)  of composite guards
            for (int i = 0; i < amtOfCompGuards; i++)
            {

                int row = random.Next(0, 10);
                int col = random.Next(0, 10);
                if (random.NextDouble() < 0.16)
                {
                    compGuards.Add(new turretGuard(row, col, 100, 7,artilleryProvider,shieldProvider));
                }
                else if (random.NextDouble() >= 0.16 && random.NextDouble() < 0.33)
                {
                    compGuards.Add(new turretQuirkyGuard(row, col, 100, 5, artilleryProvider, shieldProvider));
                }
                else if(random.NextDouble() >= 0.33 && random.NextDouble() < 0.49)
                {
                    compGuards.Add(new turretSkipGuard(row, col, 100, 9, artilleryProvider, shieldProvider));
                }
                else if (random.NextDouble() >= 0.49 && random.NextDouble() < 0.65)
                {
                    compGuards.Add(new infantryGuards(row, col, 100, 5, artilleryProvider, shieldProvider));
                }
                else if (random.NextDouble() >= 0.65 && random.NextDouble() <= 0.81)
                {
                    compGuards.Add(new infantrySkipGuard(row, col, 100, 5, artilleryProvider, shieldProvider));
                }
                else
                {
                    compGuards.Add(new infantryQuirkyGuard(row, col, 100, 5, artilleryProvider, shieldProvider));
                }
            }

            int numOfModeChange = random.Next(50, 100);

            // exports composite objects unique function exports results
            // it should result true or false, as it is valid nums and invalid nums
            for (int i = 0; i < numOfModeChange; i++)
            {
                int compguardIndex = random.Next(0, diffGuards.Count);

                int posX = random.Next(0, 10);
                int posY = random.Next(0, 10);
                int dist = random.Next(0, 5);
                int oppoStrength = random.Next(0, 100);
                int shieldInd = random.Next(0, 10);

                bool result;
                if (compGuards[compguardIndex] is turretGuard)
                {
                    result =((turretGuard)compGuards[compguardIndex]).retaliation(posX, posY,oppoStrength,dist,shieldInd);
                    Console.WriteLine($"guard {compguardIndex} :turretGuard) Revenge Success? {result}");
                }
                else if (compGuards[compguardIndex] is turretSkipGuard)
                {
                    result = ((turretSkipGuard)compGuards[compguardIndex]).feignedTerror(posX, posY, shieldInd);
                    Console.WriteLine($"guard {compguardIndex} :turretSkipGuard Scared Success? {result}");
                }
                else if (compGuards[compguardIndex] is turretQuirkyGuard)
                {
                    result = ((turretQuirkyGuard)compGuards[compguardIndex]).selfDestruction(posX, posY, oppoStrength, shieldInd);
                    Console.WriteLine($"guard {compguardIndex} :turretQuirkyGuard explosion Success? {result}");
                }
                else if (compGuards[compguardIndex] is infantryGuards)
                {
                    result = ((infantryGuards)compGuards[compguardIndex]).resistance(shieldInd, posX, posY, oppoStrength);
                    Console.WriteLine($"guard {compguardIndex} :infantryGuards, resisted Success? {result}");
                }
                else if (compGuards[compguardIndex] is infantryQuirkyGuard)
                {
                    result = ((infantryQuirkyGuard)compGuards[compguardIndex]).strikes(posX, posY, oppoStrength, shieldInd);
                    Console.WriteLine($"guard {compguardIndex} :infantryQuirkyGuard, strikes Success? {result}");
                }
                else if (compGuards[compguardIndex] is infantrySkipGuard)
                {
                    result = ((infantrySkipGuard)compGuards[compguardIndex]).dodgeAtk(posX, posY, oppoStrength, shieldInd);
                    Console.WriteLine($"guard {compguardIndex} :infantrySkipGuard, dodgeAtk Success? {result}");
                }



            }

        }
    }
}



/* Revision history
 * 
 * Fighter dependency injection
 * minimum strength for fighter hierarchy
 * Added guard classes
 * Added cross product classes
 * Adjustments to minor details of fighter hiearchy functions adding error handleing
 */