using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string PasswordsNotMatch = "Şifreler uyuşmuyor.";
        public static string UserExistsWithSameEmail = "Bu e-mail ile bir kullanıcı mevcut.";
        public static string UserExistsWithSameGsm = "Bu telefon numarası ile bir kullanıcı mevcut.";
        public static string LoginSuccessfull = "Sayın {0} hoşgeldiniz.";
        public static string PasswordInvalid = "Şifre eksik veya hatalı.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string VerificationCodeWrong = "Verification code is wrong";
        public static string AuthorizationFailed = "Authorization is failed.";
        public static string UsernameExists = "This username already exists";
        public static string RegisteredSuccessfully = "User registered successfully.";
        public static string CardNotFound = "Billing card not found";
        public static string AddressNotfound = "Address not found.";
    }
}
