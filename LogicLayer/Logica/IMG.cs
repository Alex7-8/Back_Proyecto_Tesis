using DataLayer.EntityModel;
using Firebase.Storage;
using Firebase.Auth;
using System.ComponentModel.DataAnnotations;
namespace LogicLayer
{
    public class IMG
    {


		


		private static readonly string ApiKey = "AIzaSyA6JTz6KrGgHK_5EtHxClJZjsiO797C38I";
        private static readonly string Bucket = "front-8ca8f.appspot.com";
        private static readonly string AuthEmail = "alex@gmail.com";
        private static readonly string AuthPassword = "12345678";

        public static StreamContent ConvertBase64ToStream(string imageFromRequest)
        {
            byte[] imageStringToBase64 = Convert.FromBase64String(imageFromRequest);
            StreamContent streamContent = new(new MemoryStream(imageStringToBase64));
            return streamContent;
        }

        public static async Task<string> UploadImage(Stream stream, IMGEntity imageDTO)
        {
            string imageFromFirebaseStorage = "";
            FirebaseAuthProvider firebaseConfiguration = new(new FirebaseConfig(ApiKey));

            FirebaseAuthLink authConfiguration = await firebaseConfiguration
                .SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            CancellationTokenSource cancellationToken = new();

            FirebaseStorageTask storageManager = new FirebaseStorage(Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authConfiguration.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child(imageDTO.FolderName)
                .Child(imageDTO.ImageName)
                .PutAsync(stream, cancellationToken.Token);

            try
            {
                imageFromFirebaseStorage = await storageManager;
            }
            catch
            {
            }
            return imageFromFirebaseStorage;
        }
    }
}