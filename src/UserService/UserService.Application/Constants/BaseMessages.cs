namespace UserService.Application.Constants;

public class BaseMessages
{
    public struct UserCreateMessages
    {
        public const string UserCreateSucceess = "Kullanıcınız onaylandıktan sonra size mail ile bildirilecektir.";
        public const string UserCreateFail= "Kullanıcı kayıt işleminde hata oluştu.";
    }

    public struct UserUpdateMessages
    {
        public const string UserUpdateSucceess = "Kullanıcınız güncellenmiştir.";
        public const string UserUpdateFail = "Kullanıcı güncelleme işleminde hata oluştu.";
        public const string UserNotFount = "Böyle bir kullanıcı bulunmamaktadır.";
    }
}