namespace UserManagement.Application.Constants;

public class BaseMessages
{
    public struct AcceptRegistrationMessages
    {
        public const string AcceptRegistrationSucceess = "Kullanıcı kayıt işlemi onaylanmıştır.";
        public const string AcceptRegistrationFail = "Bu kullanıcının kaydı daha önce onaylanmıştır.";
        public const string UserNotFount = "Böyle bir kullanıcı bulunmamaktadır.";
    }
    public struct DeclineRegistrationMessages
    {
        public const string Succeess = "Kullanıcı kayıt işlemi onaylanmamıştır.";
        public const string UserNotFount = "Böyle bir kullanıcı bulunmamaktadır.";
        public const string Fail = "Kullanıcı kayıt işlemi onaylanmama işleminde hata oluştu.";
    }
    public struct UserUpdateMessages
    {
        public const string UserUpdateSucceess = "Kullanıcınız güncellenmiştir.";
        public const string UserUpdateFail = "Kullanıcı güncelleme işleminde hata oluştu.";
        public const string UserNotFount = "Böyle bir kullanıcı bulunmamaktadır.";
    }

    public struct UserRegisterMessages
    {
        public const string UserRegisterSucceess = "Kullanıcı eklenmiştir.";
        public const string UserRegisterFail = "Kullanıcı eklenirken hata oluştu.";
        public const string UserNotFount = "Böyle bir kullanıcı bulunmamaktadır.";
    }
}