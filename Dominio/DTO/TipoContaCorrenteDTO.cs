using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class TipoContaCorrenteDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Descrição do Operação")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Descricao { get; set; } = String.Empty;


        public TipoContaCorrenteDTO() { }




        public static TipoContaCorrente ToEntidade(TipoContaCorrenteDTO tipoCCDTO)
        {
            return new TipoContaCorrente
            {
                Id = tipoCCDTO.Id,
                Descricao = tipoCCDTO.Descricao

            };

        }

        public static IEnumerable<TipoContaCorrenteDTO> ToDTO(IEnumerable<TipoContaCorrente> tipoCC)
        {
            List<TipoContaCorrenteDTO> tipoContaCorrenteDTO = new();

            foreach (var item in tipoCC)
            {
                tipoContaCorrenteDTO.Add(new TipoContaCorrenteDTO
                {
                    Id = item.Id,
                    Descricao = item.Descricao
                });
            }
            return tipoContaCorrenteDTO;

        }

        public static TipoContaCorrenteDTO ToDTO(TipoContaCorrente tipoCCDTO)
        {
            return new TipoContaCorrenteDTO
            {
                Id = tipoCCDTO.Id,
                Descricao = tipoCCDTO.Descricao

            };
        }
    }
}
