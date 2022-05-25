using Microsoft.Owin;
using Owin;
using SimpleInjector;

[assembly: OwinStartupAttribute(typeof(LivroCaixa.Startup))]
namespace LivroCaixa
{
    public class FirebaseSettings
    {
        public string type => "service_account";
        public string project_id => "fluxodecaixa-9ccb8";
        public string private_key_id => "4843de4d8709a9f2792be3ae834a900802f4cded";
        public string private_key => "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQClH1l3oUJi4IOl\nI9fp8USSfQCgzSI7vXlcgQ86lrQf55ZzMV1qDfGYb5c43lUga43khDfjSXopzAi4\n4RmgU0AOcvue5Y6OYOL0z5ujU6aRyASZD62H1ja8Bpzvt/aelhPJfTO6QAyE0WrJ\nWMWIUQSi0huXTYCCasSz/uqBT3j1HPrkPojUs6xLO+ZyEVHYylo8FDLv4Sas/53J\n91p8vSrgaVrOt+sSC3KsilzuRPWhhF3fJLscCoNTeWsISDRr2jFvbAb46mQE5de3\nXADoearYq7nj5TsaFbwC3Bn4iToA9u1ToqvzDv5rSg16RBYgn/T5+RDfRVQjNK3V\nSTDD9YAlAgMBAAECggEAAy5P7GiMXVhC4PcITUbrEcFbtj1NaAY15MzNrMzDaovt\nD4oHaSgcm9mmeZ9611WLFbhgVT4QnuaB0oHvmvGPndekNkLo1KBULvAi6OoPHubS\nA9rT9nZvdn7QHiQWmaZ9ehex2+go9ZQNMRTHNWGOK87NXkRNcfW5lVgXT2dnBp3A\n02PLEAy88IUi41axoUmmtGECvNT+CvXPkhAIhadYZgtDMHSjq0fveeA7AOVVhi8c\nGF3V9Rg8fR12M0Yjrw+F3GvKe590WPT7JH3lda3T6EpqMW8lwCrc9Spw6qEYYHuM\nXfSIzkKUbGEeTikk121Ex76UOUYBw+QGdabfrngyTwKBgQDne+vccY0Vw/ROnWqy\nfaAK48d6qBTsUzQ6lznoYPeIjPnI8+KyT1P5mrcclYErArzkwKLu5P0MO4lNNYgf\ne/YY9N6KWhwG6FqeTcRs/F0O1xWTaMYVQ1J/9SF+pwBnbLt4tJRCb+RASVOOvwIJ\nDEcbTZE4JG8Dvq7UqRtd5NvuGwKBgQC2nDVh5nx21/e4LYzGfrHdYXtvamzZVXbt\nNdynziMZ++gsBNdtS3ckWVynbkqmudJ3tF6nBTuUOLIbCbvu+mSrTBZGs0x+fEmB\ngOctAAbLOmYYuRnWl61pRBepmYQoVf8PVfoU/31V0IqVLr9DNJDGAHu5iGbIDI/q\neUQplqIuvwKBgQCI5cqZNjY/1wFWtNXy9UR+7DJ+oYSuNo6+z8yZVy9M/EKEy2cY\nGL4K8aq1Jt2a+CWKCzrQmW/lmhgbJzt3kzH5lGc/3waQNz3QPSGbaqwGKMFDykTL\nMmNJ9Uh3xIACqlU8j17W46FTO4pE80Va4H308ayeTT2yM0Tl05SxtLU0XQKBgH4C\nBpgautIrLwYJcHXcIRIZXVrBfoDuB5WiMDQe+3vHpysQ4DLL+1e6zkO+yIaJ/WCa\ntyHba4RZJxWfPu6mG+dUJp78xJEXZIWXlG4p8YY1MxRmTh0VJxVXZlii1V4cEVfc\nxSfOMQeRUr+ktVhDoo7u8HFTXVE30etDaLSl/B3zAoGAWxkTMlPvhN1GI3Q2Ltkm\nI9/dchiMt7mgd353PRa+k+uuSSzo23uqokKmQ3RaDm2UFVkd4DjjpoKdtcPeNOso\nB133uu3MtCGaakQuiz3IwUDb+/QU7omcoJY+Ce3zzgRnxab4ChFikHyhN7mcAiMn\nzJUeaf++hhBsgk1mRv9GwXs=\n-----END PRIVATE KEY-----\n";
        public string client_email => "fluxodecaixa-9ccb8@appspot.gserviceaccount.com";
        public string client_id => "108326616636694101106";
        public string auth_uri => "https://accounts.google.com/o/oauth2/auth";
        public string token_uri => "https://oauth2.googleapis.com/token";
        public string auth_provider_x509_cert_url => "https://www.googleapis.com/oauth2/v1/certs";
        public string client_x509_cert_url => "https://www.googleapis.com/robot/v1/metadata/x509/fluxodecaixa-9ccb8%40appspot.gserviceaccount.com";

        //[JsonPropertyName("project_id")]
        //public string ProjectId => "that-rug-really-tied-the-room-together-72daa";

        //[JsonPropertyName("private_key_id")]
        //public string PrivateKeyId => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        // ... and so on
    }

    public partial class Startup
    {               
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
