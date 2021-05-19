using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.Services.IService;

namespace TestApp.Services
{
    public class OrganizationService
    {
        IGridFSBucket gridFS;
        IMongoCollection<Organization> Organizations;

        public OrganizationService(IDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            gridFS = new GridFSBucket(database);
            Organizations = database.GetCollection<Organization>("Organizations");
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await Organizations.Find(items => true).ToListAsync();
        }

        public async Task<Organization> GetOrganization(string id)
        {
            return await Organizations.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task Create(Organization p)
        {
            await Organizations.InsertOneAsync(p);
        }

        public async Task Update(Organization p)
        {
            await Organizations.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(p.Id)), p);
        }

        public async Task Remove(string id)
        {
            await Organizations.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<byte[]> GetImage(string id)
        {
            return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        }

        // Заранее прошу прощения за этого кадавра.
        public async Task StoreImage(string id, Stream imageStream, string imageName)
        {
            Organization p = await GetOrganization(id);


            if (p.HasPhotoImage())
            {
                // если ранее уже была прикреплена картинка, удаляем ее
                await gridFS.DeleteAsync(new ObjectId(p.PhotoImgUrl));
            }
            else if (!p.HasPhotoImage())
            {
                // сохраняем изображение
                ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);

                // обновляем данные по документу
                p.PhotoImgUrl = imageId.ToString();
                var filter = Builders<Organization>.Filter.Eq("_id", new ObjectId(p.Id));
                var update = Builders<Organization>.Update.Set("PhotoImgUrl", p.PhotoImgUrl);

                await Organizations.UpdateOneAsync(filter, update);
            }

            if (p.HasPhoneImage())
            {
                // если ранее уже была прикреплена картинка, удаляем ее
                await gridFS.DeleteAsync(new ObjectId(p.PhoneNumberImgUrl));
            }
            else if (!p.HasPhoneImage())
            {
                // сохраняем изображение
                ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);

                // обновляем данные по документу
                p.PhoneNumberImgUrl = imageId.ToString();
                var filter = Builders<Organization>.Filter.Eq("_id", new ObjectId(p.Id));
                var update = Builders<Organization>.Update.Set("PhoneNumberImgUrl", p.PhoneNumberImgUrl);

                await Organizations.UpdateOneAsync(filter, update);
            }

            if (p.HasAddressImage())
            {
                // если ранее уже была прикреплена картинка, удаляем ее
                await gridFS.DeleteAsync(new ObjectId(p.AddressImgUrl));
            }
            else if (!p.HasAddressImage())
            {
                // сохраняем изображение
                ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);

                // обновляем данные по документу
                p.AddressImgUrl = imageId.ToString();
                var filter = Builders<Organization>.Filter.Eq("_id", new ObjectId(p.Id));
                var update = Builders<Organization>.Update.Set("AddressImgUrl", p.AddressImgUrl);

                await Organizations.UpdateOneAsync(filter, update);
            }
        }
    }
}
