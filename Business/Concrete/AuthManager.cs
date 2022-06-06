using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserDal _userDal;
        private ITokenHelper _tokenHelper;
        public AuthManager(IUserDal userDal, ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _userDal = userDal;
        }

        public IDataResult<AccessToken> Login(UserForLoginDto userCredentials)
        {
            userCredentials.Gsm = GsmHelper.CorrectGsmWithCountryCode(userCredentials.Gsm,"TR");
            var user = _userDal.Get(u=>u.Gsm == userCredentials.Gsm);
            if (user == null) return new ErrorDataResult<AccessToken>(Messages.UserNotFound);

            var passwordCompare = HashingHelper.VerifyPasswordHash(userCredentials.Password, user.PasswordHash, user.PasswordSalt);
            if (!passwordCompare) return new ErrorDataResult<AccessToken>(Messages.PasswordInvalid);

            var token = CreateAccessToken(user);
            return new SuccessDataResult<AccessToken>(token,String.Format(Messages.LoginSuccessfull,$"{user.Name} {user.Surname}"));
        }

        public IResult Register(UserForRegisterDto userCredentials)
        {
            userCredentials.Gsm = GsmHelper.CorrectGsmWithCountryCode(userCredentials.Gsm, "TR");
            var businessRules = BusinessRules.Run(UserShouldNotExistsWithSameGsm(userCredentials.Gsm),UserShouldNotExistsWithSameEmail(userCredentials.Email),PasswordsMustMatch(userCredentials.Password,userCredentials.PasswordConfirm));
            if (businessRules != null) return businessRules;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userCredentials.Password, out passwordHash, out passwordSalt);
            var newUser = new User()
            {
                Email = userCredentials.Email,
                Gsm = userCredentials.Gsm,
                GsmApproved = true, // For now, it is simulation.
                Name = userCredentials.Name,
                Surname = userCredentials.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userDal.Add(newUser);
            return new SuccessResult(Messages.RegisteredSuccessfully);
        }

        private AccessToken CreateAccessToken(User user)
        {
            return _tokenHelper.CreateToken(user);
        }

        #region Business Rules
        private IResult UserShouldNotExistsWithSameGsm(string gsm)
        {
            var user = _userDal.Get(u => u.Gsm == gsm);
            if (user != null) return new ErrorResult(Messages.UserExistsWithSameGsm);
            return new SuccessResult();
        }
        private IResult UserShouldNotExistsWithSameEmail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);
            if (user != null) return new ErrorResult(Messages.UserExistsWithSameEmail);
            return new SuccessResult();
        }
        private IResult PasswordsMustMatch(string password, string passwordConfirm)
        {
            if(password == passwordConfirm) return new SuccessResult();
            return new ErrorResult(Messages.PasswordsNotMatch);
        }
        #endregion
    }
}
