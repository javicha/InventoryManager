namespace Inventory.Application.Common.Enums
{
    /// <summary>
    /// Type of product. To simplify the model, we represent it as an enum instead of an entity
    /// </summary>
    public enum ProductTypeEnum
    {
        Primer = 0,
        PrimerKit = 1,
        ProbeKit = 2,
        BarcodeKit = 3
    }
}
