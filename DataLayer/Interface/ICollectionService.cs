using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ICollectionService
    {
        public CollectionResponse Insert(Collection collection);
        public CollectionResponse NextCollectionNo();
        public CollectionResponse Update(Collection collection);
        public MasterResponse MasterData();
        public List<Collection> GetCollection(CollectionInput collectionInput);
        public CollectionDownloadOutput DownloadCollection(CollectionDownloadInput vInput);
        public PendingCollectionResponse GetPendingCollection();
    }
}
