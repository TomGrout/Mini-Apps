using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

/* c3034731
Coursework for CompSci Year 1 - p4cs 

 References:
 https://www.physicsforums.com/threads/converting-integer-into-array-of-single-digits-in-c.558588/
 stackoverflow.com
 geeksforgeeks.com
 codereview.stackexchange.com

 */

namespace cw
{
    class Exercises
    {
        public static void TrinaryConverter()
        {
            Console.WriteLine("\nEnter a trinary number to be converted: ");          //prompt user input
            string trinaryInput = Console.ReadLine();        //take input from user
            List<int> triNum = new();                 //create a list to be used as trinary number later on

            if (trinaryInput != null)
            {
               foreach (char c in trinaryInput)
               {

                    int b = c - 48;         //convert each character in our new trinary number to an integer using ASCII values

                    if (b > 2 || b < 0)     //validate each value to be 0, 1 or 2
                    {
                        Console.WriteLine("ERROR! Enter only 0, 1 or 2");
                        TrinaryConverter();              //rerun method upon failed input
                    }
                    else
                    {
                        triNum.Add(b);      //on successful validation, add each character to integer list created earlier
                    }
               }
            }

            else if(trinaryInput == "q")
            {
                return;
            }

            else
            {
                Console.WriteLine("ERROR!");
                TrinaryConverter();         //rerun method upon failed input
            }
            triNum.Reverse();           //reverse the order of the trinary number, ready for mathematical operation

            int counter = 0;
            double decNum = 0;          //initialise counter and final value variables

            foreach (int i in triNum)
            {
                double tempNum = i * (Math.Pow(3, counter));            //for each digit in the trinary number, multiply it by 3^counter 
                counter++;
                decNum += tempNum;          //add each multiplied digit to the final value

            }
            Console.WriteLine("That is {0} in decimal!", decNum);           //when all digits have been converted and added to the final value, display the answer!

        }//end of trinaryConverter

        public static void ISBNverifier()
        {
            int ISBNtotal = 0;
            bool ISBNvalid = true;          //initiate variables for validation and final result

            Console.WriteLine("Enter isbn num: ");
            string ISBNinput = Console.ReadLine();          //take user input

            ISBNinput = ISBNinput.Replace("-", "");         //remove dashes from input
            int ISBNcounter = ISBNinput.Length;             //counter for multiplication
            string calculationOutput = "";                  //string to hold workings out, e.g: (2x10=20, 4x9=36)

            if ((ISBNcounter > 10) || (ISBNcounter < 10))
            {
                Console.WriteLine("Error - ISBN number must be 9 digits plus one check character.");        //show error message if length of input is not 10
                return;
            }

            for (int i = 0; i < ISBNinput.Length - 1; i++)          //iterate over items in ISBN number
            {
                var c = ISBNinput[i];
                int b = c - 48;             //change each character in input to ASCII number equivalent

                if (b < 0 || b > 9)
                {
                    Console.WriteLine("ERROR!");            //show error message if any non check digit is below 0 or above 9
                    ISBNvalid = false;
                    break;
                }
                else
                {
                    calculationOutput += (ISBNcounter + " * " + b + " = " + ISBNcounter*b + ", ");      //add calculation to string to be output as a whole later
                    ISBNtotal += (b * ISBNcounter);         //calculate descending counter value (from 10 to 1) multiplied by digit of input
                    ISBNcounter--;                          //reduce counter for next iteration
                }
            }

            if (ISBNinput[9] == 'X')                //check final value or 'check digit' in ISBN value
            {
                ISBNtotal += 10;                    //if check digit is X, take it to be worth 10
            }
            else if ((ISBNinput[9] - 48 < 10) && (ISBNinput[9] - 48 > -1))
            {
                ISBNtotal += ISBNinput[9];          //otherwise, if the digit is between 0-9, add it on as normal
            }
            else
            {
                Console.WriteLine("Error - check digit must be 0-9 or X");      //finally if the check digit is erroneous return an error
                ISBNvalid = false;
                return;
            }

            if (ISBNvalid)
            {
                Console.Write(calculationOutput + "\n");        //print workings out
                Console.WriteLine("{0} % 11 is {1}.\nTherefore, it is a {2} ISBN number.", ISBNtotal, ((ISBNtotal % 11).ToString()), (ISBNtotal % 11 == 0 ? "valid" : "invalid"));
            }   //if valid, display result and final calculation
        }//end of ISBNverifier


        public static void AddNewStudent(Dictionary<string, int> school)            //function to create a new student for the school roster
        {

            Console.WriteLine("What is the first name of the student? (must be unique): ");         //take user input for student name
            string addStudentNameInput = Console.ReadLine();

            Console.WriteLine("Which form would you like to add {0} to? ", addStudentNameInput);        
            int addStudentFormInput = Convert.ToInt32(Console.ReadLine());                          //take user input for student form

            if (addStudentFormInput < 1)
            {
                if (school.ContainsKey(addStudentNameInput))
                {
                    Console.WriteLine("ERROR - Name already used");         //if the student already exists, return an error
                }
                else
                {
                    school.Add(addStudentNameInput, addStudentFormInput);       //if the student doesnt exist, add them to the desired form
                    Console.WriteLine("{0} added to form {1}", addStudentNameInput, addStudentFormInput);       //tell user student has been added
                }
            }
            else
            {
                Console.WriteLine("Please enter a value greater than 0");
            }

            return;     //return to Main
        }//end of addNewStudent

        public static void FindForm(Dictionary<string, int> school)         //function to find a desired form in school roster
        {
            Console.WriteLine("Which form are you looking for?");
            string targetFormString = Console.ReadLine();               //take user input for form

            try
            {
                int targetForm = Int32.Parse(targetFormString);         //convert string input to integer, to match with data type of "form" value
                //Console.WriteLine(targetForm);
                if (school.ContainsValue(targetForm))     
                {
                    foreach (var item in school)            //if the desired form is in the school roster, search through items in roster
                    {
                        if (item.Value == targetForm)
                        {
                            Console.WriteLine(item.Key);            //when a name corresponding to the correct form has been found, display name to the user
                        }
                    }                                               //stop when all corresponding names have been displayed
                }
                else
                {
                    Console.WriteLine("Sorry, there exists no such form!");     //if there are no students in the desired form, display an error message
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nIncorrect data type entered. Please use a single integer for the desired form.");      
                FindForm(school);           //if the tryparse method fails, display an error message and re-run the function
            }

            return;
        }//end of findForm

        public static void ShowAllFormsSorted(Dictionary<string, int> school)           //function to show sorted forms from school roster
        {

            var sortedDict = from entry in school orderby entry.Value ascending select entry;   //use system linq to sort items alphabetically

            foreach (var item in sortedDict)
            {
                Console.WriteLine(item);
            }
        }//end of showAllFormsSorted

        public static void SchoolRoster()
        {
            Dictionary<string, int> school = new Dictionary<string, int>()          //initialise a roster to be used/edited by the user
            {
                {"Matt", 1 },
                {"Joe", 1 },
                {"Bill", 1 },
                {"Aaron", 2 },
                {"Anne", 2 },
                {"Harshpreet", 2 },
                {"Wilfred", 3 },
                {"Kiernan", 3 },
                {"Abdul", 3 },
                {"Jamie", 3 }           //this can be empty but it was useful in development to have some values predefined
            };

            Console.WriteLine("\nChoose one of the following options:\n----------------------------" +
            "\na) Add new student\nb) List students in a specific form\nc) List all students\nq) Quit");        //ask user to input a, b, c or q
                                                                                                                // based on what they want to do
            string selectAction = Console.ReadLine();

            switch (selectAction)                  //switch case determines which function to be ran, corresponding to the user's choice
            {
                case "a": AddNewStudent(school); break;

                case "b": FindForm(school); break;

                case "c": ShowAllFormsSorted(school); break;

                case "q": System.Environment.Exit(0); break;        //quit program
            }
        }//end of schoolRoster

        public static void Main(string[] args)
        {
            bool runProgram = true;         //non recursive way of being able to run or stop running the program when needed

            while (runProgram)
            {
                Console.WriteLine("\nChoose one of the following options:\n----------------------------" +
                    "\na) Trinary Converter\nb) School Roster\nc) ISBN verifier\nq) Quit");                     //take user input for menu

                string selectAction = Console.ReadLine();

                switch (selectAction)           //based on previous input from user, choose a function to be ran which corresponds with their choice
                {
                    case "a":
                        TrinaryConverter();     //trinary converter
                        break;

                    case "b":
                        SchoolRoster();         //menu system for school roster
                        break;

                    case "c":
                        ISBNverifier();         //ISBN verifier
                        break;

                    case "q":
                        runProgram = false;     //exit program by using boolean expression
                        break;

                    default:
                        Console.WriteLine("Please enter a value a, b, c or q, corresponding to the desired action.\n");
                        break;                  //if the user input has no corresponding function, they will be asked to retry, 
                }                               // and the menu will rerun as per the bool variable runProgram
            }
        }//end of Main
    }//end of Exercises
}