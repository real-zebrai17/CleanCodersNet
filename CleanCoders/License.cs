using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCoders
{
    public class License : Entity
    {
        public enum LicenseType {  VIEWING, DOWNLOADING }

        public License(LicenseType type, User user, Codecast codeCast)
        {
            Type = type;
            User = user;
            CodeCast = codeCast;
        }

        public User User { get; }

        public Codecast CodeCast { get; }

        public LicenseType Type { get; }
    }
}
