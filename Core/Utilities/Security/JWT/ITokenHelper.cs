using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
        IIdentity ValidateToken(string jwt);
    }
}
