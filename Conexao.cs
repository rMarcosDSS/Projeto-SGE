using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projeto_SGE_Testes
{
    internal class Conexao
    {
        public static string strConn = ("DATABASE=projetosge ; Data Source=localhost; user Id=root; password=''; Port=3306; convert zero datetime=true");

        public static string niveluser;
    }
}
