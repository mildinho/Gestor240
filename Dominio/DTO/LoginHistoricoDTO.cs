using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Dominio.DTO
{
    public class LoginHistoricoDTO
    {

        public DateTime Data { get; set; }
        public string IP { get; set; }
        public string EMail { get; set; }

        public LoginHistoricoDTO() { }




        public static LoginHistorico ToEntidade(LoginHistoricoDTO login)
        {
            return new LoginHistorico
            {
                Data = login.Data,
                EMail = login.EMail,
                IP = login.IP,

            };

        }

        public static IEnumerable<LoginHistoricoDTO> ToDTO(IEnumerable<LoginHistorico> login)
        {
            List<LoginHistoricoDTO> loginDTO = new();

            foreach (var item in login)
            {
                loginDTO.Add(new LoginHistoricoDTO
                {
                    Data = item.Data,
                    EMail = item.EMail,
                    IP = item.IP,
                });
            }
            return loginDTO;

        }

        public static LoginHistoricoDTO ToDTO(LoginHistorico login)
        {
            return new LoginHistoricoDTO
            {
                Data = login.Data,
                EMail = login.EMail,
                IP = login.IP,
            };
        }
    }
}
