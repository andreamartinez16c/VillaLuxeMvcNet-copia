using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using VillaLuxeMvcNet.Models;

namespace VillaLuxeMvcNet.Services
{
    public class ServiceAWSCache
    {
        private IDistributedCache cache;

        public ServiceAWSCache(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<List<Villa>> GetVillasFavoritasAsync()
        {

            string jsonVillas =
                await this.cache.GetStringAsync("villasfavoritas");
            if (jsonVillas == null)
            {
                return null;
            }
            else
            {
                List<Villa> villas = JsonConvert.DeserializeObject<List<Villa>>(jsonVillas);
                return villas;
            }
        }

        public async Task AddVillaFavoritaAsync(Villa villa)
        {
            List<Villa> villas = await this.GetVillasFavoritasAsync();
            //SI NO EXISTEN VILLAS FAVORITAS TODAVIA, CREAMOS 
            //LA COLECCION
            if (villas == null)
            {
                villas = new List<Villa>();
            }
            //AÑADIMOS LA NUEVA VILLA A LA COLECCION
            villas.Add(villa);
            //SERIALIZAMOS A JSON LA COLECCION
            string jsonVillas = JsonConvert.SerializeObject(villas);
            DistributedCacheEntryOptions options =
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                };
            //ALMACENAMOS LA COLECCION DENTRO DE CACHE REDIS
            //INDICAREMOS QUE LOS DATOS DURARAN 30 MINUTOS
            await this.cache.SetStringAsync("villasfavoritas", jsonVillas, options);
        }

        public async Task DeleteVillaFavoritaAsync(int idvilla)
        {
            List<Villa> villas = await this.GetVillasFavoritasAsync();
            if (villas != null)
            {
                Villa villaEliminar = villas.FirstOrDefault(x => x.IdVilla == idvilla);
                villas.Remove(villaEliminar);

                if (villas.Count == 0)
                {
                    await this.cache.RemoveAsync("villasfavoritas");
                }
                else
                {
                    string jsonVillas = JsonConvert.SerializeObject(villas);
                    DistributedCacheEntryOptions options =
                        new DistributedCacheEntryOptions
                        {
                            SlidingExpiration = TimeSpan.FromMinutes(30)
                        };
                    await this.cache.SetStringAsync("villasfavoritas", jsonVillas, options);
                }
            }
        }
    }
}
