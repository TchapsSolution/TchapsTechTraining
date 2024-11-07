namespace TchapsTech.Customers
{
    public class UserController
    {
        public void ListUsers()
        {
            var users = DataAccess.GetUsers();

            DisplayUsers(users);
        }

        public void AddUser()
        {
            Console.WriteLine("\n Adding new user. Enter the user information in the following format: ");
            Console.WriteLine("FirstName, \t LastName, \t Email, \t PhoneNumber\n");

            var userModel = new List<string>();

            while (userModel.Count != 4)
            {
                var dataRaw = Console.ReadLine();

                userModel = dataRaw.Split(",").ToList();

                if (userModel.Count != 4)
                {
                    Console.WriteLine("\n User data invalid. Please enter the user information in the following format: ");
                    Console.WriteLine("FirstName, \t LastName, \t Email, \t PhoneNumber \n");
                }
            }

            var user = new UserModel
            {
                FirstName = userModel[0],
                LastName = userModel[1],
                Email = userModel[2],
                PhoneNumber = userModel[3]
            };

            DataAccess.Insert(user);
        }


        public void DeleteUser()
        {
            Console.WriteLine("\n Deleting an existing user. Please specify the UserId from the table ");
            Console.Write(" UserId: ");

            int userId = 0;
            List<UserModel> users = new();

            while (userId < 1)
            {
                var dataRaw = Console.ReadLine();

                int.TryParse(dataRaw, out userId);

                if (userId < 1)
                {
                    Console.Write("\n UserId is invalid. Please enter a valid number . UserId: ");
                    continue;
                }

                users = DataAccess.GetUser(userId);
                if (users == null || users.Count == 0)
                {
                    Console.Write("\n The user with the Id '{0}' was not found. Please enter a valid UserId: ", userId);
                    userId = 0;
                }
            }

            Console.WriteLine("\n The following user will be deleted.");
            DisplayUsers(users);

            Console.Write("\n Are you sure you want to delete this user? \t Yes: Y , No: N \t : ");

            var confirm = Console.ReadLine();

            if (confirm.ToLower() == "y")
            {
                DataAccess.Delete(userId);
            }

        }


        static void DisplayUsers(List<UserModel> users)
        {
            Console.WriteLine();
            Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,20}|{3,30}|{4,20}|{5,22}|", "UserId", "FirstName", "LastName", "Email", "PhoneNumber", "CreationDate"));
            Console.WriteLine();

            foreach (var user in users)
            {
                Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,20}|{3,30}|{4,20}|{5,22}|", user.UserId, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.CreationDate));
            }
        }


    }
}
