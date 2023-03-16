/*
 * Program: Assignment 4 - Demonstrate the use of arrays and test plans
 *
 * Purpose: Create and record inventory information for a Car dealership
 *
 * Creator: Nishita Deswel & Shai Karmani
 *      Nov 22 2022 V 1.0
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Declare array of Brands
            string[] brandDeswal = new string[3];
            string[] carsInvArr = new string[20];

            //Variables
            string userEntry = " ";

            //for loop to get brands from user
            //Enter the 3 brands in your car inventory
            for (int i = 0; i < brandDeswal.Length; i++)
            {
                Console.WriteLine("Please enter a brand: ");
                brandDeswal[i] = Console.ReadLine().ToString();
                Console.WriteLine("Brand entered is {0}.", brandDeswal[i]);
            }

            while (userEntry != "E")
            {
                userEntry = DisplayMenu();
                if (userEntry == "A")
                {
                    AddCarDetails(carsInvArr, brandDeswal);
                }
                else if (userEntry == "B")
                {
                    EditCarDetails(carsInvArr);
                }
                else if (userEntry == "C")
                {
                    DisplayCars(carsInvArr);
                }
                else if (userEntry == "D")
                {
                    DeleteCar(carsInvArr);
                }
            }
        }

        private static string DisplayMenu()
        {
            string userEntry = "";
            Console.WriteLine("*************");
            Console.WriteLine("Which option would you like to pick ?");
            Console.WriteLine("***********");
            Console.WriteLine("Option One- Adding new car details (A)");
            Console.WriteLine("Option Two- Edit existing car details (B)");
            Console.WriteLine("Option Three- Display all Cars in store (Brand Name & Model (C)");
            Console.WriteLine("Option Four- Delete Car Information (D)");
            Console.WriteLine("Option Five- Exit the program (E)");
            Console.WriteLine("***********");
            Console.WriteLine("Enter: A , B , C , D, or E");
            userEntry = Console.ReadLine();
            return userEntry;
        }

        private static void AddCarDetails(string[] invArr, string[] brandArr)
        {
            string brand = "";
            string model = "";
            bool isValid = true;
            while (NumberOfCarsInInventory(invArr) <= 20)
            {
                Console.WriteLine("Enter one of the three brands: ");
                brand = Console.ReadLine();

                if (brand == "DONE")
                {
                    //Brings you back to main menu
                    break;
                }

                brand = BrandValidator(brandArr, brand);

                try
                {
                    Console.WriteLine("Enter the Model: \n" +
                    "SEDAN (1) \n" +
                    "HATCHBACK (2) \n" +
                    "SUV (3) \n" +
                    "PICKUP TRUCK (4)");
                    model = Console.ReadLine();
                    //Model must be validated, so I will call the method that validates it
                    isValid = ModelValidator(model);
                    //As per the rubric, an exception must be thrown if the user enters the wrong input
                    if (!isValid)
                    {
                        throw new Exception("You have entered an incorrect model input");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please re-enter model: ");
                    model = Console.ReadLine();
                }

                //Time to concatenate in format Brand-Model (e.g Ford-1)
                string carDetails = "";

                carDetails = brand + "-" + model;
                AddCarToInventory(invArr, carDetails);
                Console.WriteLine(carDetails + " is added to inventory");

                brand = "";
                model = "";
            }
        }

        private static void DisplayCars(string[] invArr)
        {
            foreach (var i in invArr)
            {
                if (string.IsNullOrEmpty(i))
                {
                    Console.WriteLine("NONE");
                }
                Console.WriteLine(i);
            }
        }

        private static void DeleteCar(string[] invArr)
        {
            Console.WriteLine("Which car would you like to delete from your inventory? (enter in the form ford-2)");
            string deleteRecord = Console.ReadLine();
            bool exists = invArr.Contains(deleteRecord);
            if (!exists)
            {
                Console.WriteLine("This entry does not exist");
            }
            else if (exists)
            {
                Console.WriteLine("Would you like to delete this record? Y / N");
                string deleteOrNo = Console.ReadLine();
                if (deleteOrNo == "Y")
                {
                    var index = Array.FindIndex(invArr, i => i.Contains(deleteRecord));
                    invArr[index] = "NONE";
                }
            }
        }

        private static void EditCarDetails(string[] invArr)
        {
            Console.WriteLine("Please enter the car record you would like to edit, in the form (Ford-2): ");
            string alterRecord = Console.ReadLine();
            bool exists = invArr.Contains(alterRecord);
            if (!exists)
            {
                Console.WriteLine("Brand record not found for that entry");
            }
            else
            {
                var index = Array.FindIndex(invArr, i => i.Contains(alterRecord));
                Console.WriteLine("What would you like to change this to? ");
                string alterTo = Console.ReadLine();
                invArr[index] = alterTo;
                Console.WriteLine("You have successfully changed " + alterRecord + " to " + alterTo);
            }
        }

        private static void AddCarToInventory(string[] invArr, string carDetails)
        {
            for (int i = 0; i < invArr.ToArray().Length; i++)
            {
                if (string.IsNullOrEmpty(invArr[i]) || invArr[i] == "NONE")
                {
                    invArr[i] = carDetails;
                    break;
                }
            }
        }

        private static string BrandValidator(string[] brandDeswal, string brand)
        {
            while (!brandDeswal.Contains(brand))
            {
                Console.WriteLine("This is not one of the brands you entered in the start. Re-enter: ");
                brand = Console.ReadLine();
            }
            return brand;
        }

        private static bool ModelValidator(string model)
        {
            bool isValid = false;
            int val = Convert.ToInt32(model);
            if (model == "1" || model == "2" || model == "3" || model == "4")
            {
                isValid = true;
            }
            return isValid;
        }

        private static int NumberOfCarsInInventory(string[] invArr)
        {
            int carCount = 0;
            //i is the counter
            for (int i = 0; i < invArr.Length; i++)
            {
                if (string.IsNullOrEmpty(invArr[i]) && invArr[i] == "NONE")
                {
                    carCount = carCount + 1;
                }
            }
            return carCount;
        }
    }
}