using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class TokenIssuer
    {
        private Dictionary<string, string> _audienceKey = new Dictionary<string, string>();

        public void ShareKeyOutofBand(string audience, string key)
        {
            if (!_audienceKey.ContainsKey(audience))
            {
                _audienceKey.Add(audience, key);
            }
            else
            {
                _audienceKey[audience] = key;
            }
        }

        //public void GetToken(string audience, string credentials)
        //{
        //    throw NotImplementedException;
        //}
    }
}
