using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Infra.Data.Repositories
{
    public class LoginRepository : GenericoRepository<Login>, ILoginRepository
    {
        private readonly DBContexto _context;

        public LoginRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Login>> PesquisarPorEmailAsync(string Email)
        {
            return await _context.Login.
                Where(x => x.Email == Email).ToListAsync();
        }

        public async Task<IEnumerable<Login>> PesquisarPorEmailSenhaAsync(string Email, string Senha)
        {
            return await _context.Login.
                Where(x => x.Email == Email && x.Password == Senha).
                ToListAsync();
        }


        public string Criptografar(string Texto)
        {
            return Encrypt(Texto);
        }

        public string DesCriptografar(string Texto)
        {
            return Decrypt(Texto);
        }


        private string Encrypt(string clearText)
        {
            string encryptionKey = "_Y(8)51LcpW/gq7Ob1JKBEFW#";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }


        private string Decrypt(string cipherText)
        {
            //string encryptionKey = "MAKV2SPBNI99212";
            string encryptionKey = "_Y(8)51LcpW/gq7Ob1JKBEFW#";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }

    }



}
