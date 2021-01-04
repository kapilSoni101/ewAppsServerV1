using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.CommonService {
    public static class TestCryptoHelper {

        public static void CheckEncryption(string raw) {
            try {
                // Either TripleDES or AES
                CryptoHelper.EncryptionAlgorithm algorithm = CryptoHelper.EncryptionAlgorithm.TripleDES;
                Console.WriteLine($"Encryption Algorithm: {algorithm}");

                // Encrypt string  
                string ciphertext = new CryptoHelper().Encrypt(raw, algorithm);
                // Print encrypted string  
                Console.WriteLine($"Encrypted data: {ciphertext}");
                // Decrypt the encrypted string.  
                string decrypted = new CryptoHelper().Decrypt(ciphertext, algorithm);
                // Print decrypted string. It should be same as raw data  
                Console.WriteLine($"Decrypted data: {decrypted}");
            }
            catch (Exception exp) {
                Console.WriteLine(exp.Message);
            }
            Console.ReadKey();
        }
    }
}
