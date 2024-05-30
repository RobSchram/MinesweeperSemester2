using LogicLayer.interfaces;
using LogicLayer;
using LogicLayer.Dto;

public class FieldDataAccess : IFieldDao
{
    private readonly IFieldDao _fieldDao;
    private readonly IClearData clearData;
    private readonly IStoreData storeData;

    public FieldDataAccess(IClearData clearData, IStoreData storeData, IFieldDao fieldDao)
    {
        this.clearData = clearData;
        this.storeData = storeData;
        _fieldDao = fieldDao;
    }

    public void StoreField(Cell[,] field)
    {
        clearData.ClearField();
        storeData.StoreField(field);
    }
    public FieldDto GetField()
    {
        return _fieldDao.GetField();
    }
}