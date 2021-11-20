using System;
using System.Collections.Generic;

namespace FluxMei.Models
{
    public partial class Mei
    {
        public Mei()
        {
            Movimento = new HashSet<Movimento>();
        }
        public string IdMei { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string NomeEmpresa { get; set; }
        public string Logradouto { get; set; }
        public string Cnpj { get; set; }
        public string NomeProprietario { get; set; }
        public string Telefone { get; set; }

        public ICollection<Movimento> Movimento { get; set; }
    }
}
