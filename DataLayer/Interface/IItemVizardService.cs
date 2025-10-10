using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemVizardService
    {
        public List<ItemVizard> GetAllItems(ItemVizardInput vizardInput);
        public Int32 Insert(ItemVizardStore vizardStore);
        public List<ItemPriceWizard> GetAllItemPriceWizard(ItemPriceWizardInput pricewizardinput);
    }
}
