using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Installer_checklist
{
    static class UserSecurity
    {
        public static void Administrator()
        {
            string role = string.Empty;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                role = "User is an Administrator";
            }
            else
                role = "User is not an Administrator";

            Console.WriteLine(role);
        }

        //Check if user is a Domain User or Local User
        public static bool DoesUserExist()
        {
            //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //user name of the person who started the current thread.
            string userName = Environment.UserName;

            bool exists = false;
            try
            {
                using (var domainContext = new PrincipalContext(ContextType.Domain, "DOMAIN"))
                {
                    using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userName))
                    {
                        exists = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception could occur if machine is not on a domain
            }
            using (var domainContext = new PrincipalContext(ContextType.Machine))
            {
                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userName))
                {
                    exists = true;
                }
            }
            return exists;
        }

        public static void isCurrentUserLocalUser()
        {
            string user = string.Empty;
            if (Environment.MachineName == Environment.UserDomainName)
            {
                user = "Current User is Local User";
            }
            else
                user = "Current User is not Local User";

            Console.WriteLine(user);
        }
    }
}
