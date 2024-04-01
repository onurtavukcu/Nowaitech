namespace NowaitechDomain.Commands
{
    public static class myextension
    {
        public static string GetFirstCar(this string value) // this value'nin bir string nesnesi olduğunu belirtir
                                                            // ve bu extension string kütüphanesindeymiş gibi çalışır this bu işe yarar.
        {
            if (value.Length < 1)
            {
                return null;
            }
            else
            {
                return value.Substring(0,1);    
            }
        }
    }
}
