namespace TjommeMetSomme.Entities
{
    public class Constants
    {
        public static class Administator
        {
            public static class Role
            {
                public const int ID = 1;
                public const string NAME = "Administrator";
                public const string NORMALIZED_NAME = "Administrator";
            }

            public static class User
            {
                public const int ID = 1;
                public const string EMAIL = "administrator@tjommemetsomme.co.za";
                public const string USERNAME = "administrator";
                public const string FIRST_NAME = "admin";
                public const string LAST_NAME = "istrator";
                public const string PASSWORD = "@Test1234";
            }
        }

        public static class Manager
        {
            public static class Role
            {
                public const int ID = 2;
                public const string NAME = "Manager";
                public const string NORMALIZED_NAME = "Manager";
            }

            public static class User
            {
                public const int ID = 2;
                public const string EMAIL = "manager@tjommemetsomme.co.za";
                public const string USERNAME = "manager";
                public const string FIRST_NAME = "man";
                public const string LAST_NAME = "ager";
                public const string PASSWORD = "@Test1234";
            }
        }

        public static class Parent
        {
            public static class Role
            {
                public const int ID = 3;
                public const string NAME = "Parent";
                public const string NORMALIZED_NAME = "Parent";
            }
        }

        public static class Student
        {
            public static class Role
            {
                public const int ID = 4;
                public const string NAME = "Student";
                public const string NORMALIZED_NAME = "Student";
            }
        }

        public static class Tutor
        {
            public static class Role
            {
                public const int ID = 5;
                public const string NAME = "Tutor";
                public const string NORMALIZED_NAME = "Tutor";
            }
        }
    }
}