using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace EmployeeManagement.Application.Services
{
    public class PasswordHasherService : IPasswordHasher<string>
    {
        public string HashPassword(string user, string password)
        {
            // Generar un salt único para cada contraseña
            byte[] salt = GenerateSalt();

            // Hash de la contraseña usando PBKDF2
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Concatenar el salt al hash para su posterior verificación
            return $"{Convert.ToBase64String(salt)}:{hashedPassword}";
        }

        public PasswordVerificationResult VerifyHashedPassword(string user, string hashedPassword, string providedPassword)
        {
            try
            {
                // Separar el salt y el hash de la contraseña almacenada
                string[] parts = hashedPassword.Split(':');
                byte[] salt = Convert.FromBase64String(parts[0]);
                string storedPasswordHash = parts[1];

                // Generar el hash de la contraseña proporcionada
                string hashedPasswordProvided = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: providedPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                // Comparar los hashes
                if (hashedPasswordProvided == storedPasswordHash)
                {
                    return PasswordVerificationResult.Success;
                }
                else
                {
                    return PasswordVerificationResult.Failed;
                }
            }
            catch
            {
                return PasswordVerificationResult.Failed;
            }
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
