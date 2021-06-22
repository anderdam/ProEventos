using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string LocalName { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} deve ter de 3 a 50 caracteres")]
        public string Tema { get; set; }

        [Display(Name = "Qtd Pessoas")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(10, 120000, ErrorMessage = "O {0} precisa ter de 10 a 120.000 pessoas.")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida (gif, jpg, jpeg, bmp ou png).")]
        public string ImagemURL { get; set; }

        [Display(Name = "telefone")]
        [Required(ErrorMessage = "O {0} é obrigatório.")]
        [Phone(ErrorMessage = " O {0} não pode ter letras.")]
        public string Telefone { get; set; }

        [Display(Name = "e-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O {0} precisa ser válido.")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}