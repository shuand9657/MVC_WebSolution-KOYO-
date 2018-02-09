//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BzModelClass
//{
//    public class AuthToken : OAuthBearerAuthenticationProvider
//    {
//        public override Task RequestToken(OAuthRequestTokenContext context)
//        {
//            var value = context.Request.Query.Get("access_token");
//            if (!string.IsNullOrEmpty(value))
//            {
//                context.Token = value;
//            }

//            return Task.FromResult(0);
//        }
//    }
//}
