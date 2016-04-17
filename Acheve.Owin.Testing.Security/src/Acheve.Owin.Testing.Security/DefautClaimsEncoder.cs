﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Acheve.Owin.Testing.Security
{
    internal static class DefautClaimsEncoder
    {
        public static string Encode(IEnumerable<Claim> claims)
        {
            var sourceString = string.Join("&", claims.Select(c => $"{c.Type}={c.Value}"));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(sourceString));
        }

        public static IEnumerable<Claim> Decode(string encodedValue)
        {
            var decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(encodedValue));
            return decodedString.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x =>
                {
                    var values = x.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    return new Claim(values[0], values[1]);
                });
        }
    }
}
