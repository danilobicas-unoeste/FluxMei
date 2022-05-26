using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LivroCaixa.Models
{
    [FirestoreData]
    public class Usuarios: IFirebaseEntity
    {
        [FirestoreProperty]
        public string IdMei { get; set; }
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Nome { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public DateTime Cadastro { get; set; }       
    }
}