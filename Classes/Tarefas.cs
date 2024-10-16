using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeTarefas.Classes
{
    public class Tarefas
    {
        public string Descricao { get; set; }
        public string Prioridade { get; set; }
        public DateTime DataDescricao { get; set; }

        public override string ToString()
        {
            return $"{Descricao} - {Prioridade} - {DataDescricao:dd/MM/yyyy}";
        }


    }
}
