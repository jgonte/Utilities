namespace Utilities.Validation
{
    public static class RegularExpressions
    {
        public static string Email => @"^(([^<>()\[\]\.,;:\s@\""]+(\.[^<>()\[\]\.,;:\s@\""]+)*)|(\"".+\""))@(([^<>()\.,;\s@\""]+\.{0,1})+([^<>()\.,;:\s@\""]{2,}|[\d\.]+))$";
    }
}
