using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IBoxUnpackService
    {
        BoxUnpackResponse UnpackBox(BoxUnpack model);
    }
}
