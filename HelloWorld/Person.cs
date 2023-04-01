using HelloWorld.Utils;
using System;

namespace HelloWorld
{
    public class Person
    {

        private string _name = "";


        public string Name
        {
            get
            {

                return _name;

            }

            set
            {

                _name = value == "Agatai" ? "Baska at" : value;
            }
        }


        private string _email = "";


        public string Email
        {
            set
            {
                if (EmailHelper.IsValidEmail(value))
                {
                    _email = value;
                }

                else
                {
                    Console.WriteLine("Invalid email");
                }
            }
            get
            {
                return _email;
            }
        }
    }
}
