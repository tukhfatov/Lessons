namespace HelloWorld.Utils
{
    public static class EmailHelper
    {

        public static bool IsValidEmail (string email)
        {

            return email.Contains("@");
        }
    }
}
