// See https://aka.ms/new-console-template for more information

using TchapsTech.Customers;

Console.WriteLine("Customer Management!");

bool endApp = false;

while (!endApp)
{
    Console.WriteLine("\n ************************************************************************* ");
    Console.Write("\n What do you want to do ? \t Add: 1 \t Delete: 2  \t List: 3  \t Exit: 0 . |  ");

    var choice = Console.ReadLine();

    UserController userController = new UserController();

    switch (choice)
    {
        case "1":
            userController.AddUser();
            break;
        case "2":
            userController.DeleteUser();
            break;
        case "3":
            userController.ListUsers();
            break;
        case "0":
        default:
            endApp = true;
            break;
    }
}
