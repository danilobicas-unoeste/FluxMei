using Firebase.Auth;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace LivroCaixa.Models
{
    public class FirestoreProvider
    {
        public string webApiKey = "AIzaSyDPO2Ft-G1H9Mlfdgo3xEVFaY1jDsOmZVY";
        string projectId;
        private readonly FirebaseAuthProvider _firebaseAuthProvider;
        private readonly FirestoreDb _fireStoreDb = null;

        public FirestoreProvider()
        {                   
            string arquivoApiKey = HostingEnvironment.MapPath("~/Resources/fluxodecaixa-9ccb8-4843de4d8709.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", arquivoApiKey);
            projectId = "fluxodecaixa-9ccb8";
            _fireStoreDb = FirestoreDb.Create(projectId);
            _firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
        }

        public async Task<Object> Login(string email, string senha)
        {
            try
            {
                var auth = await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, senha);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                return serializedContent;
                //Preferences.Set("FreshFirebaseToken", serializedContent);
            }
            catch
            {
                return null;
            }
        }

        public async Task AddOrUpdate<T>(T entity, CancellationToken ct) where T : IFirebaseEntity
        {
            var document = _fireStoreDb.Collection(typeof(T).Name).Document(entity.Id);
            await document.SetAsync(entity, cancellationToken: ct);
        }

        public async Task<T> Get<T>(string id, CancellationToken ct) where T : IFirebaseEntity
        {
            var document = _fireStoreDb.Collection(typeof(T).Name).Document(id);
            var snapshot = await document.GetSnapshotAsync(ct);
            return snapshot.ConvertTo<T>();
        }

        public async Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct) where T : IFirebaseEntity
        {
            var collection = _fireStoreDb.Collection(typeof(T).Name);
            var snapshot = await collection.GetSnapshotAsync(ct);
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct) where T : IFirebaseEntity
        {
            return await GetList<T>(_fireStoreDb.Collection(typeof(T).Name).WhereEqualTo(fieldPath, value), ct);
        }

        // just add here any method you need here WhereGreaterThan, WhereIn etc ...

        private static async Task<IReadOnlyCollection<T>> GetList<T>(Query query, CancellationToken ct) where T : IFirebaseEntity
        {
            var snapshot = await query.GetSnapshotAsync(ct);
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        public async Task<Usuarios> CadastrarNovoUsuario(string email, string password, string nome, string idmei)
        {
            Usuarios usuario;
            
            var auth = await _firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email,password);
            
            string token = auth.FirebaseToken;
            if (token != null)
            {
                usuario = new Usuarios();
                usuario.Email = email;
                usuario.Nome = nome;
                usuario.Id = auth.User.LocalId;
                usuario.IdMei = idmei;
                CollectionReference collectionReference = _fireStoreDb.Collection("Usuarios");
                await collectionReference.AddAsync(usuario);
                return usuario;
            }
            return null;
        }
    }
}