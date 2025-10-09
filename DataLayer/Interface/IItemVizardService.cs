using RetailApi.Models;

namespace RetailApi.DAL.Interfaces
{
    public interface IItemVizardService
    {
        public List<ItemVizard> GetAllItems(ItemVizardInput vizardInput);
        public Int32 Insert(ItemVizardStore vizardStore);
        public List<ItemPriceWizard> GetAllItemPriceWizard(ItemPriceWizardInput pricewizardinput);
    }
}
