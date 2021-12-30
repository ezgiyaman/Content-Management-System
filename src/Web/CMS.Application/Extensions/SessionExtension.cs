using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Extensions
{
    // Session kabul ettiği 2 tip data bulunmaktadır.Bunlardan 1. XML diğeri ise Json'dır.
    //İlgili ürünü sepete atmak istediğimizde yani Session'da tutmak istediğimizde "SetJson()" fonksiyonumuz çalışacaktır.Bu fonksiyon product tipindeki ürünümüzü Json tipini dönüştürecektir yani serialize edecektir.Sepetten yani session'dan bu ürünü alıp uygulama tarafındaki işleme sokacağım zaman ise uygulamanın anlayacağı tipe yani product tipine dönüştürmem gerecektir bunada GetJson() fonksiyonuyla yapacağım.
    public static class SessionExtension
    {
        //Sepette gönderilen ürünler Session üzerinde saklanacaktır.Session browser'da geçiş olarak bizim ömrünü belirleyeceğimiz bir depolama alanıdır.
        public static void SetJson(this ISession session, string key, object value)
        {
            //burada JsonConvert sınıfının içerisinde gömülü olarak bulunan SerializeObject fonksiyonu ile product tipindeki ürünümü Json tipindeki ürünümü Json tipine serialize ediyorum.
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            //SessionData null ise bize default olarak method gelen tipi döndürecek ,null değilse json tipindeki ürünü deserialize ederek product tipine dönüştürecek.
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
