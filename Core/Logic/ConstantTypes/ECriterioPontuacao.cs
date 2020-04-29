using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic.ConstantTypes
{
    public enum ECriterioPontuacao
    { 
        Renda_total_da_família_até_900_reais = 1,
        Renda_total_da_família_de_901_à_1500_reais = 2,
        Renda_total_da_família_de_1501_à_2000_reais = 3,
        Pretendente_com_idade_igual_ou_acima_de_45_anos = 4,
        Pretendente_com_idade_de_30_à_44_anos = 5,
        Pretendente_com_idade_abaixo_de_30_anos = 6,
        Famílias_com_3_ou_mais_dependentes_lembrando_que_dependentes_maiores_de_18_anos_não_contam = 7, //)
        Famílias_com_1_ou_2_dependentes__lembrando_que_dependentes_maiores_de_18_anos_não_contam = 8 //)
    }
}