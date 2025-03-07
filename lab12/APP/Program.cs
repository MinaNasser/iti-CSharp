using Models;
using POS;

namespace APP

{
    public class Program
    {
        static int Main()
        {
            UserInterface userInterface = new UserInterface();
            userInterface.ShowMainMenu();

            Customer customer = new Customer();
            return 0;

        }
    }
}