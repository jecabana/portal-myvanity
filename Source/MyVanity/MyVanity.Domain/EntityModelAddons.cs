namespace MyVanity.Domain
{
    public partial class PersonDetails
    {
        public string FullName
        {
            get { return Utils.BuildFullName(FirstName, MiddleName, LastName); }
        }
    }

    
    public partial class Procedure
    {
        public string ShortDescription
        {
            get { return string.Format("{0}, {1}", Category.Name, Type.Name); }
        }
    }

    public partial class UserProcedure
    {
        public string ShortDescription
        {
            get { return string.Format("{0}, ({1})", Patient.Profile.FullName, Procedure.ShortDescription); }
        }
    }

    public static class Utils
    {
        public static string BuildFullName(string first, string middle,string last)
        {
            return !string.IsNullOrEmpty(middle) ? string.Format("{0} {1} {2}", first, middle, last) 
                                                 : string.Format("{0} {1}", first, last);
        }
    }
}
