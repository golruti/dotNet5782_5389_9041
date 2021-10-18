using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            int choice = 0;

            Console.Write("For insert options press 1/n" +
                "  For update options press 2/n" +
                " For display options press 3/n " +
                "To view the list options, press 4/n");
            choice = Console.Read();


            switch (choice)
            {

                
                case 1:
                    Console.Write("To add a base station to the list of stations Press 1/n" +
                        " To add a skimmer to the list of existing skimmers Press 2/n" +
                        " To receive a new customer To the list of customers Press 3/n " +
                        "To receive a package For delivery Press 4/n");
                    choice = Console.Read();

                    switch (choice)
                    {

                        case 1:
                            // code block
                            break;
                        case 2:
                            // code block
                            break;
                        default:
                            // code block
                            break;
                    }
                    break;


                case 2:
                    // code block
                    break;
                case 3:
                    // code block
                    break;
                case 4:
                    // code block
                    break;
                default:
                    // code block
                    break;
            }





        }
    }
}
