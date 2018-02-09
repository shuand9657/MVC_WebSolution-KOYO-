using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DBModelClass
{
    public class AudienceModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }

    public class AudienceStore
    {
        public static ConcurrentDictionary<string, Audience> AudienceList = new ConcurrentDictionary<string, Audience>();
        
        static AudienceStore()
        {
            AudienceList.TryAdd("099153c2625149bc8ecb3e85e03f0022", new Audience
            {
                ClientId = "099153c2625149bc8ecb3e85e03f0022",
                Base64Secrect = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                Name = "http://www.koyosms.com"
            });
        }

        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");
            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secrect = TextEncodings.Base64Url.Encode(key);

            Audience newAudience = new Audience { ClientId = clientId, Base64Secrect = base64Secrect, Name = name };
            AudienceList.TryAdd(clientId,newAudience);

            return newAudience;
        }

        public static Audience FindAudience(string clientId)
        {
            Audience audience = null;
            if(AudienceList.TryGetValue(clientId,out audience)){
                return audience;
            }

            return null;
        }
    }
}
