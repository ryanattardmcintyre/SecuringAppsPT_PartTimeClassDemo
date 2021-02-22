using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace ShoppingCart.Presentation.Utilities
{
    public class Encryption
    {
        public static string Hash(string str)
        {
            SHA512 myAlg = SHA512.Create();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);

            byte[] digest = myAlg.ComputeHash(data);

            return Convert.ToBase64String(digest);
        }

        static string password = "alskdjflaskdjfals";
        static string salt = "alsdkjfalsdkfjalsdkasdfasdfasdffj";

        public static string SymmetricEncrypt(string str)
        {
            Rfc2898DeriveBytes myKeyGenerator = new Rfc2898DeriveBytes(password,
                 System.Text.Encoding.UTF8.GetBytes(salt));

            //key , iv

            Rijndael myAlg = Rijndael.Create();

            byte[] key = myKeyGenerator.GetBytes(myAlg.KeySize / 8);
            byte[] iv = myKeyGenerator.GetBytes(myAlg.BlockSize / 8);

            MemoryStream msIn = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(str)); //converting str >>> bytes
            msIn.Position = 0;


            CryptoStream cs = new CryptoStream(
                msIn,
                myAlg.CreateEncryptor(key, iv),
                 CryptoStreamMode.Read
                );

            MemoryStream msOut = new MemoryStream(); //this will be for my cipher
            cs.CopyTo(msOut);
            msOut.Position = 0; //pointer within the stream has been reset to position 0

            string output = Convert.ToBase64String(msOut.ToArray()); //converting bytes >> str
            return output;
        }

        public static string SymmetricDecrypt(string cipher)
        {
            Rfc2898DeriveBytes myKeyGenerator = new Rfc2898DeriveBytes(password,
                    System.Text.Encoding.UTF8.GetBytes(salt));

            //key , iv

            Rijndael myAlg = Rijndael.Create();

            byte[] key = myKeyGenerator.GetBytes(myAlg.KeySize / 8);
            byte[] iv = myKeyGenerator.GetBytes(myAlg.BlockSize / 8);

            MemoryStream msIn = new MemoryStream(Convert.FromBase64String(cipher)); //converting str (containing encrypted data) >>> bytes
            msIn.Position = 0;


            CryptoStream cs = new CryptoStream(
                msIn,
                myAlg.CreateDecryptor(key, iv),
                 CryptoStreamMode.Read
                );

            MemoryStream msOut = new MemoryStream(); //this will be for my cipher
            cs.CopyTo(msOut);
            msOut.Position = 0; //pointer within the stream has been reset to position 0

            string output = System.Text.Encoding.UTF8.GetString(msOut.ToArray()); //converting bytes >> str
            return output;


        }


        public static AsymmetricKeys GenerateAsymmetricKey()
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();

            AsymmetricKeys keys = new AsymmetricKeys()
            {
                PublicKey = myAlg.ToXmlString(false),
                PrivateKey = myAlg.ToXmlString(true)
            };
            return keys;
        }

        public static MemoryStream AsymmetricEncrypt(MemoryStream input, string publicKey)
        {
            input.Position = 0;
            //notes:
            // asymmetric uses two keys
            // asymmetric is slower
            // asymmetric is not used to encrypt/decrypt large data
            
            //encrypt with the public key
            //decrypt with the private key

            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            myAlg.FromXmlString(publicKey);

            byte[] cipher = myAlg.Encrypt(input.ToArray(), true);
            MemoryStream msOut = new MemoryStream(cipher);
            msOut.Position = 0;
            return msOut;
        }
        public static MemoryStream AsymmetricDecrypt(MemoryStream cipherInput, string privateKey)
        {
            cipherInput.Position = 0;
            //notes:
            // asymmetric uses two keys
            // asymmetric is slower
            // asymmetric is not used to encrypt/decrypt large data

            //encrypt with the public key
            //decrypt with the private key

            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            myAlg.FromXmlString(privateKey);

            byte[] originalData = myAlg.Decrypt(cipherInput.ToArray(), true);
            MemoryStream msOut = new MemoryStream(originalData);
            msOut.Position = 0;
            return msOut;
        }

        //drawback of this method is that you have to save the key and iv somwhere because you 'll need them for the decrypt
        public static SymmetricKeys GenerateSymmetricKeys()
        {
            Rijndael myAlg = Rijndael.Create();
            myAlg.GenerateKey(); myAlg.GenerateIV();

            SymmetricKeys keys = new SymmetricKeys()
            {
                Key = myAlg.Key,
                Iv = myAlg.IV
            };
            return keys;
        }

        public static void Encryptfile(MemoryStream input, string publicKey)
        {
            //1. Generate the symmetric keys (recommended you can use the geneatesymmetrickeys())
            //2. symmetrically encrypt the input
            //3. asymmetrically encrypt the symmetric keys generated in 1.
            //4. create a new FileStream in which you will place
                //4.i  the encrypted key, the encrypted iv (from no. 3)
                //4.ii the encrypted input (from no. 2)
            //5. save everything altogether in 1 FileStream (in a chosen folder) created in 4.
            //6. save in a physical file
        }

        public static MemoryStream Decryptfile(MemoryStream input, string privateKey)
        {
            //1. Opening the input (if it is a FileSteram it might be even better)
            //2. Read the first 128 bytes (which should be the encrypted symmetric key)
            //3. Read the next (without resetting the filesteram position) 128 bytes (which should be the encrypted IV)
            byte[] encKey =new byte[128];
            input.Read(encKey, 0, 128);
            byte[] encIv = new byte[128];
            input.Read(encIv, 0, 128);
            //4. Asymmetrically decrypt both the encrypted key (from no.2) and the encrypted iv (from no 3.)
            //5. Read the rest of the bytes (from 256 till end of file)
            MemoryStream cipher = new MemoryStream();
            input.CopyTo(cipher);

            //6. Symmetrically decrypt the cipher (the actual file content)

            //7 you can return back the decrypted file content and then you allow the teacher/student to download it. 
            //recommended if you are going to save it in a folder be careful, because you will be leaving a decrypted copy in a folder somewhere
            //compromising all the hard work that you have been working on.
            return null;

        }



    }


    public class  AsymmetricKeys
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }


    public class SymmetricKeys
    { public byte[] Key { get; set; } 
        public byte [] Iv { get; set; }
    }
}
